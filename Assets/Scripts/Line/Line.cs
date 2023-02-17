using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Line
{
    public int Talker;
    public string LineText;

    public List<int> Next;

    public int ID;

    public Vector3 BlockPosition;
}
