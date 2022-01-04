using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace PM {
    [InitializeOnLoad]
    public static class SceneContext
    {
        static Vector3 mousePosition;
        public static bool InWayPointEdit = false;
        //new way points during WayPointEdit
        //public static List<WayPoint> mWayPointsDuringEdit = new List<WayPoint>();

        static SceneContext()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;

            Undo.undoRedoPerformed += OnUndoExecuted;

        }

        static void OnSceneGUI(SceneView sceneView)
        {
            
        }
  

        static void OnUndoExecuted() {
            //if (InWayPointEdit) {
            Debug.Log("undo executed in editor of WayPoint");
                
            //}
        }

        [MenuItem("Tools/Level/Editor")]
        static void LevelCreate() {
            Level.CreateManager();
        }
    }

}

