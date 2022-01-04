using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PM {
    public class Ghost : CharacterBase
    {
        public enum IDState {
            Monster,
            Snack   //
        }

        public enum WorkState {
            Patrol,
            Idel
        }

        private IDState mIdState = IDState.Monster;
        private WorkState mWorkState = WorkState.Idel;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            

        }

        void doIdel() {


        }

        void doEscape() {

        }


        void doPatrol() {


        }

    }

}
