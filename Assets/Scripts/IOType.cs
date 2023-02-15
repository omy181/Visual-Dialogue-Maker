using UnityEngine;
public enum IOtype { Input = -1, Output = 1}

[CreateAssetMenu]
public class IOType : ScriptableObject
{
    public IOtype ioType;
    public Sprite Base;
    public Sprite ConnectedIcon;
    public Sprite UnConnectedIcon;

}
