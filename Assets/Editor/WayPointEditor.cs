using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PM {

    [CustomEditor(typeof(WayPoint))]
    public class WayPointEditor : Editor
    {

        Vector3 mIndicatorPosition;
        private void OnEnable()
        {
            //Undo.ClearAll();
            SceneContext.InWayPointEdit = true;
            //Debug.Log("waypoint editor enable");
        }

        private void OnDisable()
        {
            SceneContext.InWayPointEdit = false;
            //Undo.ClearAll();
            //Debug.Log("waypoint editor disable");
        }

        private void OnSceneGUI()
        {
            SceneView.currentDrawingSceneView.in2DMode = true;

            Handles.BeginGUI();
            Handles.EndGUI();

            mIndicatorPosition = GetIndicatorPosition(Event.current.mousePosition);
            if (Tools.current == Tool.View) {
                OnDrawIndicator();
                HandleMouseEvent();
            }
        }

        void OnDrawIndicator()
        {
            WayPoint wp = (WayPoint)target;

            Handles.DrawLine(wp.transform.position,mIndicatorPosition);

            //repaint scene view,so we can display indicator line immediatly
            SceneView.currentDrawingSceneView.Repaint();
        }

        bool HandleMouseEvent() {

            WayPoint curP = (WayPoint)target;
            if (Event.current.type == EventType.MouseDown)
            {
                do
                {
                    WayPoint selWp = GetTouchedWayPoint(new Vector2(mIndicatorPosition.x, mIndicatorPosition.y));
                    if (selWp == curP) {
                        break;
                    }
                    if (selWp == null)
                    {
                        selWp = NewWayPoint();
                        selWp.name = "wp" + selWp.gameObject.GetHashCode();
                        selWp.transform.parent = curP.transform.parent;
                        selWp.transform.position = mIndicatorPosition;
                    }
         
                    float cost = WayPoint.Connection.calCost(curP, selWp);
                    curP.addConnection(selWp, cost);
                    selWp.addConnection(curP, cost);

                    Selection.activeGameObject = selWp.gameObject;
                    Event.current.Use();
                } while (false);
                
            }
          
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            return true;
        }

        Vector3 GetIndicatorPosition(Vector2 guiPos) {
            WayPoint wp = (WayPoint)target;
            Vector2 pos = Event.current.mousePosition;
            Ray ray = HandleUtility.GUIPointToWorldRay(pos);
            Vector3 p = new Vector3();
            if (Mathf.Abs(ray.origin.x - (wp.transform.position.x)) > Mathf.Abs((ray.origin.y) - (wp.transform.position.y)))
            {
                p.x = ray.origin.x;
                p.y = wp.transform.position.y;
            }
            else
            {
                p.x = wp.transform.position.x;
                p.y = ray.origin.y;
            }
            p.z = wp.transform.position.z;
            return p;
        }

        static public WayPoint NewWayPoint() {
            GameObject go = new GameObject("wp");
            WayPoint point = go.AddComponent<WayPoint>();
            return point;
        }

        WayPoint GetTouchedWayPoint(Vector2 pos) {
            WayPoint wp = null;
            WayPoint[] wps = Level.CurLvl.WayPointObjs.GetComponentsInChildren<WayPoint>();
            foreach (var tmp in wps) {
                if (tmp.IsBoundaryContainPoint(pos)) {
                    wp = tmp;
                    break;
                }
            }
            return wp;
        }

      
    }
}

