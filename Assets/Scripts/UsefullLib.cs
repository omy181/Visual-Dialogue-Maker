using UnityEngine;

namespace UsefullStuff
{
    public class UsefullLib
    {
        public static Vector3 GetMousePos()
        {
            Vector3 TargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TargetPos = new Vector3(TargetPos.x, TargetPos.y, 0);
            return TargetPos;
        }

        public static Vector3 CalculateBezierPos(Vector3 P1,Vector3 P2, Vector3 P3, Vector3 P4,float t)
        {
            Vector3 A = Vector3.Lerp(P1,P2,t);
            Vector3 B = Vector3.Lerp(P2,P3,t);
            Vector3 C = Vector3.Lerp(P3,P4,t);
            Vector3 D = Vector3.Lerp(A,B,t);
            Vector3 E = Vector3.Lerp(B,C,t);
            Vector3 P = Vector3.Lerp(D,E,t);

            return P;
        }
    }
}