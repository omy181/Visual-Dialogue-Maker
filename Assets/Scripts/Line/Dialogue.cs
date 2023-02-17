
[System.Serializable]
public class Dialogue
{
    public SerializableDictionary<int,Line> lines;

    public int lastID;

    public Dialogue()
    {
        lines = new SerializableDictionary<int,Line>();
    }
}
