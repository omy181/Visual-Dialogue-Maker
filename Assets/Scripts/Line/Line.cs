using System.Collections.Generic;

[System.Serializable]
public class Line
{
    public string Talker;
    public string LineText;
    public string ChoiceLabel;

    public List<int> Next;

    public int ID;

}
