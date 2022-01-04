using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PM {
    public class WayPoint : MonoBehaviour
    {
        
        public const float Range = 0.2F;

        public List<Connection> mConnections = new List<Connection>();

        private void Start()
        {
            
        }

        public Connection addConnection(WayPoint wp, float cost, Connection.ConnectionType ct=Connection.ConnectionType.Normal) {
            Connection c = new Connection(wp,cost,ct);
            bool contain = false;
            foreach(var t in mConnections)
            {
                if (t.mPoint == wp) {
                    contain = true;
                    break;
                }
            }
            if(!contain)
                mConnections.Add(c);
            return c;
        }

        public Vector2 GetVec2Position() {
            return new Vector2(transform.position.x,transform.position.y);
        }

        public WayPoint GetConnectedWpWithDirection(MoveDirection direction) {
            Vector2 vec = CharacterBase.VectorOfDirection(direction);
            return GetConnectedWpWithVec(vec);
        }

        /// <summary>
        /// if there is a connection type is Portal,and not match the vec, it will always return the portal one
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public WayPoint.Connection GetConnectionWithVec(Vector2 vec) {
            WayPoint.Connection rc = null;
            WayPoint.Connection portalC = null;
            foreach (var c in mConnections) {
                if (c.mType == Connection.ConnectionType.Portal) {
                    portalC = c;
                    continue;
                }
                Vector2 v = c.mPoint.GetVec2Position() - GetVec2Position();
                v.Normalize();
                
                if (v == vec) { //TODO: use approximate instead of equal
                    rc = c;
                    break;
                }
            }
            if (rc == null) rc = portalC;
            return rc;
        }
        
        public WayPoint GetConnectedWpWithVec(Vector2 vec) {

            foreach (var c in mConnections) {
                Vector2 v = c.mPoint.GetVec2Position() - this.GetVec2Position();
                v.Normalize();
                if (v == vec) {
                    return c.mPoint;
                }
            }
            return null;

        }
        //public List<>

        private void OnDrawGizmos()
        {
            //Gizmos.DrawIcon(transform.position, "xiaoyuandian4.png", true, Color.green);
            Gizmos.DrawIcon(transform.position, "xiaoyuandian4.png");
        }

        private void OnDrawGizmosSelected()
        {
            foreach (var c in mConnections)
            {
                if (c.mPoint != null)
                {
                    var sc = UnityEditor.Handles.color;
                    UnityEditor.Handles.color = Color.green;
                    UnityEditor.Handles.DrawDottedLine(transform.position, c.mPoint.transform.position, 1);
                    UnityEditor.Handles.color = sc;
                }
            }
        }

        public bool IsBoundaryContainPoint(Vector2 pos)
        {
            Vector2 local = new Vector2(transform.position.x, transform.position.y);
            float sqrMag = (local - pos).sqrMagnitude;
            //Debug.Log(string.Format("sqrMag :{0}",sqrMag));
            if (sqrMag < Range * Range) {
                return true;
            }
            return false;
        }

        [System.Serializable]
        public class Connection
        {
            public enum ConnectionType {
                Normal,
                Portal      //传送门
            };
            public WayPoint mPoint;

            public float mCost;
            /// <summary>
            /// way type: 0: go pixel by pixel ,1:as portal
            /// </summary>
            ///
            public ConnectionType mType;

            
            public ConnectionType wayType {
                get {
                    return mType;
                } set
                {
                    mType = value;
                }
            }

            public Connection(WayPoint wp, float cost, ConnectionType ct = ConnectionType.Normal)
            {
                mPoint = wp;
                mCost = cost;
                mType = ct;
            }

            public bool IsEquals(Connection other) {
                 return this.mPoint == other.mPoint;
            }


            static public float calCost(WayPoint wp1,WayPoint wp2,ConnectionType ct= ConnectionType.Normal) {
                float cost = 0;
                if (ct == 0) {
                    Vector2 one = new Vector2(wp1.transform.position.x,wp1.transform.position.y);
                    Vector2 other = new Vector2(wp2.transform.position.x,wp2.transform.position.y);
                    cost = (one - other).sqrMagnitude;
                }

                return cost;
            }
        }

    }


}
