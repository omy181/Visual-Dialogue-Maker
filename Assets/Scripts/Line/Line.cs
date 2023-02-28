using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Line
{
    public int Talker;
    public string LineText;

    public List<Parameter> Parameters;

    public List<int> Next;

    public int ID;

    public Vector3 BlockPosition;
}


[System.Serializable]
public class Parameter
{
    public int Param;
    public int Operator;
    public float Value;
}