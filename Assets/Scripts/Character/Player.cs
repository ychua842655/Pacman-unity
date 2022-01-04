using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PM {
    public class Player : CharacterBase
    {

        private MoveDirection mTurnDirection = MoveDirection.None;
        
        // Start is called before the first frame update
        void Start()
        {
            Vector2 v1 = new Vector2(-2.045281f, 1);
            Vector2 v2 = new Vector2(-1.19f,1);
            Vector2 q = new Vector2(-1.061391f,1);
            if (UtilsTool.IsPointOnSegmentf(v1, v2, q)) {
                Debug.Log("assetion is work fine!");
            }

        }

        private void OnEnable()
        {
            InputManager.Instance.mEventCallback += handleMoveEvent;
        }

        private void OnDisable()
        {
            InputManager.Instance.mEventCallback -= handleMoveEvent;
        }

        // Update is called once per frame
        void Update()
        {
            if (!mMoveVector.Equals(Vector2.zero))
            {
    
                if (mCurpath.Count == 2)
                {
                    Vector2 movement = mMoveVector * (mMoveSpeedFactor * mMoveSpeed * Time.deltaTime);
                    Vector2 curPt = GetVec2Position();
                    Vector2 nextPt = curPt + movement;
                    //still in the way segment of the path
                    if (UtilsTool.IsPointOnSegmentf(mCurpath[0].GetVec2Position(), mCurpath[1].GetVec2Position(), nextPt))
                    {
                        if (mTurnDirection != MoveDirection.None)
                        {
                            var newVec = VectorOfDirection(mTurnDirection);
                            if (newVec == mMoveVector*-1) //turn-back
                            {
                                mMoveVector *= -1;
                                var tmp = mCurpath[0];
                                mCurpath[0] = mCurpath[1];
                                mCurpath[1] = tmp;
                                mTurnDirection = MoveDirection.None;
                            }
                        }
                        
                        this.SetVec2Position(nextPt);
                    }
                    else
                    {

                        //reach the end of the path and change to new path
                        if (UtilsTool.IsPointOnSegmentf(curPt, nextPt, mCurpath[1].GetVec2Position()))
                        {
                            Vector2 turnVec = mMoveVector;
                            WayPoint turnWp = mCurpath[1];
                            if (mTurnDirection != MoveDirection.None)
                            {
                                turnVec = VectorOfDirection(mTurnDirection);
                            }
                            WayPoint.Connection keepWay = turnWp.GetConnectionWithVec(mMoveVector);
                            WayPoint.Connection tendWay = turnWp.GetConnectionWithVec(turnVec);
                            
                            //only if keep the way and turn is disable
                            if (keepWay != null && (turnVec == mMoveVector || tendWay == null))
                            {
                                if (keepWay.mType == WayPoint.Connection.ConnectionType.Portal)
                                {
                                    this.SetVec2Position(keepWay.mPoint.GetVec2Position());
                                }
                                else {
                                    this.SetVec2Position(nextPt);
                                }
                            }
                            else
                            {
                                if (tendWay != null&&tendWay.mType == WayPoint.Connection.ConnectionType.Portal)
                                {
                                    this.SetVec2Position(tendWay.mPoint.GetVec2Position());
                                }
                                else {
                                    this.SetVec2Position(turnWp.GetVec2Position());
                                }
                            }

                            if (tendWay != null && tendWay.mType == WayPoint.Connection.ConnectionType.Portal)
                            {
                                mCurpath[0] = tendWay.mPoint;
                                var c = tendWay.mPoint.GetConnectionWithVec(mMoveVector);
                                if (c != null && c.mType != WayPoint.Connection.ConnectionType.Portal)
                                {
                                    mCurpath[1] = c.mPoint;
                                }
                                else {
                                    mCurpath.RemoveAt(1);
                                }

                            } else {
                                if (turnVec != mMoveVector && tendWay != null)
                                {
                                    mCurpath[1] = tendWay.mPoint;
                                    mMoveVector = turnVec;
                                }
                                else if (keepWay != null)
                                {
                                    mCurpath[1] = keepWay.mPoint;
                                }
                                else
                                {
                                    mCurpath.RemoveAt(1);
                                    mMoveVector = Vector2.zero;
                                }

                                mCurpath[0] = turnWp;
                                mTurnDirection = MoveDirection.None;
                            }
                        }
                        else
                        {
                            Debug.Log(string.Format("11111. beginWp:{0}, next pt:{1}, endWp:{2} ", mCurpath[0].GetVec2Position().x, nextPt.x, mCurpath[1].GetVec2Position().x));
                            Debug.Log(string.Format("22222. cur pt:{0}, endWp:{1} ,next pt:{2} ", curPt.x, mCurpath[1].GetVec2Position().x, nextPt.x));
                        }
                    }
                }
                else {
                    Debug.Log("error.............");
                }
            }
            else if (mTurnDirection != MoveDirection.None) {
                mMoveVector = VectorOfDirection(mTurnDirection);
                //stand in a way point
                if (mCurpath.Count == 1) {
                    WayPoint endWp = mCurpath[0].GetConnectedWpWithVec(mMoveVector);
                    SetVec2Position(mCurpath[0].GetVec2Position());
                    if (endWp != null)
                    {
                        mCurpath.Add(endWp);
                    }
                    else {
                        mMoveVector.Set(0,0);
                    }
                }
                mTurnDirection = MoveDirection.None;
            }
        }

      

        private void handleMoveEvent(MoveDirection direction) {
            mTurnDirection = direction;
        }
    }
}

