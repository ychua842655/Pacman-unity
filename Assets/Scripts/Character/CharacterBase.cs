using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PM {


    public enum MoveDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    };

    public class CharacterBase : MonoBehaviour
    {
        const float PixelPerUnitInvert = 0.01f;
        MoveDirection mCurDirection = MoveDirection.None;
        public float mMoveSpeed = 100f;
        public float mRadius = 16 * 0.01f;
        
        protected float mMoveSpeedFactor = 0.01f; //defined by property pixels per unit of Sprite
        //private float mMoveSpeedExtraFactor = 1f;//this will be changed if Charactor close to end
        protected Vector2 mMoveVector = Vector2.zero;
        
        public List<WayPoint> mCurpath = new List<WayPoint>();// from index 0 to index 1




        // Start is called before the first frame update
        void Start()
        {
        }

        virtual public void SetVec2Position(Vector2 pos) {
            transform.position = new Vector3(pos.x,pos.y,transform.position.z);
        }

        virtual public Vector2 GetVec2Position() {
            return new Vector2(transform.position.x,transform.position.y);
        }

        static public Vector2 VectorOfDirection(MoveDirection direction) {
            Vector2 vec = Vector2.zero;
            switch (direction)
            {
                case MoveDirection.Up:
                    {
                        vec = Vector2.up;
                        break;
                    }
                case MoveDirection.Down:
                    {
                        vec = Vector2.down;
                        break;
                    }
                case MoveDirection.Left:
                    {
                        vec = Vector2.left;
                        break;
                    }
                case MoveDirection.Right:
                    {
                        vec = Vector2.right;
                        break;
                    }
            }
            return vec;
        }

        virtual public bool IsBoundaryContainPoint(Vector2 vec)
        {
            var local = GetVec2Position();
            if ((local - vec).SqrMagnitude() < mRadius * mRadius)
                return true;
            else
                return false;
        }

        //virtual public void TurnTo(MoveDirection direction)
        //{
        //    mMoveVector = VectorOfDirection(direction);
            
        //}



    }
}

