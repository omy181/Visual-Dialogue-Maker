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

    public Dictionary<int,LineBlock> lineblocks = new();
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

    public void Save()
    {

        string p = EditorUtility.SaveFilePanel("Save Dialogue","","New Dialogue","dlog");

        if(p == "")
        {
            Holylib.Debug.TextShower.ShowText("Couldn't save",4);
            return;
        }


        Dialogue dialogue = new();

        StartBlock.Save(dialogue);

        dialogue.talkers = BlockManager.instance.talkers;

        dialogue.lastID = lastID;

        string data = JsonUtility.ToJson(dialogue);

        File.WriteAllText(p, data);

        Holylib.Debug.TextShower.ShowText("Saved to " + p, 4);


    }

    public void Load()
    {
        string p = EditorUtility.OpenFilePanel("Open Dialogue", "","dlog");

        if (!File.Exists(p))
        {
            Holylib.Debug.TextShower.ShowText("Couldn't Load", 4);
            return;
        }

        lineblocks.Clear();

        string json = File.ReadAllText(p);

        Dialogue dialogue = JsonUtility.FromJson<Dialogue>(json);

        CardSortingManager.instance.ClearEveryCard();

        BlockManager.instance.talkers = dialogue.talkers;

        StartBlock.Load(dialogue,0,StartBlock.transform.position);

        lastID = dialogue.lastID;

        Holylib.Debug.TextShower.ShowText("Loaded",4);

    }

}
