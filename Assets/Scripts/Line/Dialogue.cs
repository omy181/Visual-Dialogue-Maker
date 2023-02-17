
using System.Collections.Generic;

[System.Serializable]
public class Dialogue
{
    public SerializableDictionary<int,Line> lines;

    public List<string> talkers;

    public int lastID;

    public Dialogue()
    {
        lines = new SerializableDictionary<int,Line>();
    }
}
