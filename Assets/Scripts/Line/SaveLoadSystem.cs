using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using TMPro;
using SFB;

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

        

        string p = StandaloneFileBrowser.SaveFilePanel("Save Dialogue", "", "New Dialogue", "dlog");
        //EditorUtility.SaveFilePanel("Save Dialogue","","New Dialogue","dlog");

        if (p == "")
        {
            Holylib.Debug.TextShower.ShowText("Couldn't save",4);
            return;
        }


        Dialogue dialogue = new();

        StartBlock.Save(dialogue);

        dialogue.talkers = BlockManager.instance.talkers;
        dialogue.operators = BlockManager.instance.operators;
        dialogue.parameters = BlockManager.instance.parameters;

        dialogue.lastID = lastID;

        string data = JsonUtility.ToJson(dialogue);

        File.WriteAllText(p, data);

        Holylib.Debug.TextShower.ShowText("Saved to " + p, 4);


    }

    public void Load()
    {

        string p = StandaloneFileBrowser.OpenFilePanel("Open Dialogue", "", "dlog",false)[0];
            //EditorUtility.OpenFilePanel("Open Dialogue", "","dlog");

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
        BlockManager.instance.parameters = dialogue.parameters;
        BlockManager.instance.operators = dialogue.operators;

        StartBlock.Load(dialogue,0);

        lastID = dialogue.lastID;

        Holylib.Debug.TextShower.ShowText("Loaded",4);

    }

}
