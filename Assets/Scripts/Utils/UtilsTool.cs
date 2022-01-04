using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilsTool
{
    public static bool IsEqualf(float f1, float f2) {
        return Mathf.Abs(f1 - f2) < 0.0001;
    }

    public static bool IsPointOnSegmentf(Vector2 p1, Vector2 p2, Vector2 q) {
        //mult 100 to avoid precision problem
        //Vector2Int pi1 = new Vector2Int((int)p1.x * 100, (int)p1.y * 100);
        //Vector2Int pi2 = new Vector2Int((int)p2.x * 100, (int)p2.y * 100);
        //Vector2Int qi = new Vector2Int((int)q.x * 100, (int)q.y * 100);
        if (IsEqualf((p1.x - q.x) * (p1.y - q.y), (p2.x - q.x) * (p2.y - q.y)))
        {
            if (Mathf.Min(p1.x, p2.x) <= q.x && Mathf.Max(p1.x, p2.x) >= q.x) {
                if (Mathf.Min(p1.y, p2.y) <= q.y && Mathf.Max(p1.y, p2.y) >= q.y) {
                    return true;
                }
            }
        }
        return false;
        
        //if ((p1.x - q.x) * (p1.y - q.y) == (p2.x - q.x) * (p2.y - q.y) //  if two vector on line ,cross value is 0 
        //    && Mathf.Min(p1.x, p2.x) <= q.x && Mathf.Max(p1.x, p2.y) >= q.x //
        //    && Mathf.Min(p1.y, p2.y) <= q.y && Mathf.Max(p1.y, p2.y) >= q.x) //
        //{
        //    return true;
        //}
        //return false;
    }

}
