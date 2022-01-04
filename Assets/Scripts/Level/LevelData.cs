using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PM {
    public class LevelData : ScriptableObject {

        //public List<WayPoint> mWayPoints = new List<WayPoint>();


        public class Manager:Singleton<LevelData.Manager>{
            
        }

    }


}