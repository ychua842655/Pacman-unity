using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PM {
    public class Level : MonoBehaviour
    {
        const string ManagerName = "LevelManager";
        static Level sCurLv = null;
        static public Level CurLvl
        {
            get {
                if (sCurLv) {
                    //Debug.Log("curLv :" + sCurLv.GetHashCode());
                }
                if (Application.isEditor) { //because of always lose reference of the curLv, set sCurLv dynamical
                    CreateManager();
                    //Debug.Log("curLv :" + sCurLv.GetHashCode());
                }
                return sCurLv;
            } set {
                sCurLv = value;
            }
        }

        public GameObject WayPointObjs = null;
        public Player mCurPlayer = null;
        public List<Ghost> mGhosts = new List<Ghost>();

        static public void CreateManager() {
             
            var managerObj = GameObject.Find(ManagerName);
            if (!managerObj)
            {
                managerObj = new GameObject(ManagerName);
                CurLvl = managerObj.AddComponent<Level>();
                CurLvl.WayPointObjs = new GameObject("WayPoints");
                CurLvl.WayPointObjs.transform.parent = managerObj.transform;
            }
            else {
                CurLvl = managerObj.GetComponent<Level>();
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }


        #region Debug Draw

        private void OnDrawGizmos()
        {
            //Gizmos.DrawCube(this.transform.position, new Vector3(1,1,0));
        }

        void drawPathLine(WayPoint one, WayPoint.Connection c) {
            if (c.mType == WayPoint.Connection.ConnectionType.Normal)
                Gizmos.DrawLine(one.transform.position, c.mPoint.transform.position);
            else {
                //var sc = Gizmos.color;
                //Gizmos.color = Color.green;
                //Gizmos.DrawIcon(one.transform.position, "",true,Color.green);
                //Gizmos.DrawIcon(c.mPoint.transform.position, "",true,Color.green);
                var sc = Handles.color;
                Handles.color = Color.green;
                //Handles.dra
                Handles.DrawDottedLine(one.transform.position, c.mPoint.transform.position,1);
                Handles.color = sc;
                //Gizmos.color = sc;
            }

            //Debug.DrawLine(one.transform.position,other.transform.position);

        }

        private void OnDrawGizmosSelected()
        {
            WayPoint[] allWps = this.WayPointObjs.GetComponentsInChildren<WayPoint>();
            List<WayPoint> paintedWp = new List<WayPoint>();
            foreach (WayPoint wp in allWps) {
                foreach (var c in wp.mConnections) {
                    if (paintedWp.IndexOf(c.mPoint) != -1) {
                        drawPathLine(wp,c);
                    }
                }
                paintedWp.Add(wp);
            }
        }

        #endregion
    }

}
