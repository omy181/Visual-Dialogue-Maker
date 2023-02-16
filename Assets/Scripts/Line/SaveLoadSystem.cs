using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using TMPro;
public class SaveLoadSystem : MonoBehaviour
{
    public LineBlock StartBlock;

    public int lastID = 0;
    public List<LineBlock> lineblocks = new List<LineBlock>();
    public int GenerateID
    {
        get
        {
            lastID++;
            return lastID;
        }
    }

    public static SaveLoadSystem instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CleanPastData();
    }
    public void Save()
    {

        string p = EditorUtility.SaveFilePanel("Save Dialogue","","New Dialogue","dlog");

        if(p == "")
        {
            Holylib.Debug.TextShower.ShowText("Couldn't save",4);
            return;
        }

        CleanPastData();

        Line Dialogue = StartBlock.Save();

        Dialogue.lastID = lastID;

        string data = JsonUtility.ToJson(Dialogue);

        File.WriteAllText(p, data);

        Holylib.Debug.TextShower.ShowText("Saved to " + p, 4);

        CleanPastData();
    }

    public void Load()
    {
        string p = EditorUtility.OpenFilePanel("Open Dialogue", "","dlog");

        if (!File.Exists(p))
        {
            Holylib.Debug.TextShower.ShowText("Couldn't Load", 4);
            return;
        }

        CleanPastData();

        string json = File.ReadAllText(p);

        Line l = JsonUtility.FromJson<Line>(json);

        CardSortingManager.instance.ClearEveryCard();

        StartBlock.Load(l,StartBlock.transform.position);

        lastID = l.lastID;

        Holylib.Debug.TextShower.ShowText("Loaded",4);

        CleanPastData();
    }

    void CleanPastData()
    {
        foreach(LineBlock lb in lineblocks)
        {
            lb.CleanPastData();
        }

        lineblocks.Clear();
    }
    public LineBlock DoesIDExist(int id)
    {
        foreach(LineBlock lb in lineblocks)
        {
            if(lb.ID == id)
            {
                return lb;
            }
        }

        return null;
    }
}
