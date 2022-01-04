using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PM {
    public class InputManager : Singleton<InputManager>
    {

        public delegate void MoveEventDelegate(MoveDirection direction);
        public MoveEventDelegate mEventCallback;

        //private static InputManager s_instance = null;
        //public static InputManager Instance {
        //    get {
                
        //    }
        //}

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (horizontal != 0) {
                if (mEventCallback != null) {
                    mEventCallback(horizontal>0?MoveDirection.Right:MoveDirection.Left);
                }
            }
            if (vertical != 0) {
                if (mEventCallback != null)
                {
                    mEventCallback(vertical > 0 ? MoveDirection.Up : MoveDirection.Down);
                }
            }
        }
    }

}
