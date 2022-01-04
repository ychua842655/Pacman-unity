using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace PM {
    [CustomEditor(typeof(Level))]
    public class LevelEditor : Editor
    {

        public void OnEnable()
        {
            
        }


        public void testSceneGUI(SceneView sv) {

            
        }

        public void OnSceneGUI()
        {
            
            Handles.BeginGUI();
            //Debug.Log(SceneView.currentDrawingSceneView.pivot);
            GUILayout.BeginVertical(GUILayout.MinWidth(100));
            if (GUILayout.Button("Add WayPoint"))
            {
                GameObject go = new GameObject("wp");
                go.transform.parent = Level.CurLvl.WayPointObjs.transform;
                go.AddComponent<WayPoint>();
                go.name = "wp-" + go.GetHashCode();
            }

            GUILayout.Space(5);
            if (GUILayout.Button("Align Waypoints")) {
                ReorderWaypoints();
                AlignWaypoints();
                RecalculateWayCost();
            }
            //GUILayout.Space(5);
            //if (GUILayout.Button("Reorder Waypoints")) {
            //    ReorderWaypoints();
            //}

            GUILayout.EndVertical();
           
            Handles.EndGUI();
           

            //Vector3 pos = new Vector3();

            //Quaternion rotate = Quaternion.identity;
            //Handles.Disc(rotate, pos, Vector3.up, 1F, true, 1);

            //if (Event.current.clickCount > 0) {
            //    Debug.Log("click out :" + Event.current.clickCount);
            //}
            //Debug.Log(pos);
        }


        private static bool IsInHorizontal(Vector3 one, Vector3 other) {

            return Mathf.Abs(one.x - other.x) < Mathf.Abs(one.y - other.y);
            //bool horizontal = true;
            //if (Mathf.Abs(one.x - other.x) > Mathf.Abs(one.y - other.y)) {
            //    horizontal = false;
            //}

            //return horizontal;
        }

        public static void RemoveInvalidConnection() {
            WayPoint[] allWps = Level.CurLvl.WayPointObjs.GetComponentsInChildren<PM.WayPoint>();
            foreach (WayPoint wp in allWps) {
                List<WayPoint.Connection> invalidC = new List<WayPoint.Connection>();
                foreach (var c in wp.mConnections) {
                    if (c.mPoint == null)
                    {
                        invalidC.Add(c);
                    }
                }
                foreach (var c in invalidC) {
                    wp.mConnections.Remove(c);
                }
            }
        }

        public static void RecalculateWayCost() {
            WayPoint[] allWps = Level.CurLvl.WayPointObjs.GetComponentsInChildren<PM.WayPoint>();
            List<WayPoint> markWps = new List<WayPoint>();
            foreach (WayPoint wp in allWps) {
                for(int i=0;i<wp.mConnections.Count;++i)
                {
                    var c = wp.mConnections[i];
                    if (markWps.Contains(c.mPoint)) {
                        continue;
                    }
                    c.mCost = WayPoint.Connection.calCost(wp, c.mPoint, c.mType);
                }

                markWps.Add(wp);
            }
        }

        private static int wpComparion(WayPoint a, WayPoint b) {
            return 0;
        }

       
        private static void ReorderWaypoints() {
            //float offY = 2.25f, offX = 2.25f;
            float width = 4.5f, height = 4.5f;
            Dictionary<WayPoint, float> priority = new Dictionary<WayPoint, float>();
            WayPoint[] allWps = Level.CurLvl.WayPointObjs.GetComponentsInChildren<PM.WayPoint>();
            //System.Array.Sort(allWps,);
            //order value: y * 100 + x,set position of the Letf-top  as the origin point
            Func<WayPoint, float> CalculatePriority = (WayPoint wp) =>
            {
                float v = (height * 0.5f - wp.transform.position.y) * 10;
                v += (width * 0.5f + wp.transform.position.x);
                return v;
            };


            foreach (var wp in allWps)
            {
                priority[wp] = CalculatePriority(wp);
            }


            Array.Sort(allWps, (WayPoint a,WayPoint b)=> {
                float av = 0, bv = 0 ;
                if (priority.ContainsKey(a))
                {
                    av = priority[a];
                }
                else {
                    av = CalculatePriority(a);
                    priority[a] = av;
                }
                if (priority.ContainsKey(b))
                {
                    bv = priority[b];
                }
                else {
                    bv = CalculatePriority(b);
                    priority[b] = bv;
                }
                int rt = 0;
                if (av < bv) rt = -1;
                if (av > bv) rt = 1;

                return rt;
            });
            for (int i = 0; i < allWps.Length; ++i) {
                var wp = allWps[i];
                wp.transform.SetSiblingIndex(i);
            }
            //foreach (var wp in allWps) {
            //    Debug.Log(string.Format("name - {0},pv- {1}",wp.gameObject.name,priority[wp]));
            //}

        }


        public static void AlignWaypoints() {

            WayPoint[] allWps = Level.CurLvl.WayPointObjs.GetComponentsInChildren<PM.WayPoint>();
            List<WayPoint> hMark = new List<WayPoint>();
            List<WayPoint> vMark = new List<WayPoint>();
            foreach (WayPoint wp in allWps) {
                Debug.Log("wp name :"+wp.gameObject.name);
                List<WayPoint.Connection> invalidC = new List<WayPoint.Connection>();
                for(int i=0;i<wp.mConnections.Count;++i)
                {
                    var c = wp.mConnections[i];
                    if (c.mPoint == null) {
                        invalidC.Add(c);
                        continue;
                    }

                    Vector3 newPos = c.mPoint.transform.position;
                    if (IsInHorizontal(wp.transform.position, c.mPoint.transform.position))
                    {
                        if (hMark.IndexOf(c.mPoint) != -1) {
                            continue;
                        }
                        newPos.x = wp.transform.position.x;
                        hMark.Add(c.mPoint);
                    }
                    else {
                        if (vMark.IndexOf(c.mPoint) != -1)
                        {
                            continue;
                        }
                        newPos.y = wp.transform.position.y;
                        vMark.Add(c.mPoint);
                    }
                    c.mPoint.transform.position = newPos;
                    c.mCost = WayPoint.Connection.calCost(wp,c.mPoint,c.mType);
                }
                foreach (var c in invalidC) {
                    wp.mConnections.Remove(c);
                }
                
            }
        }



    }

}

