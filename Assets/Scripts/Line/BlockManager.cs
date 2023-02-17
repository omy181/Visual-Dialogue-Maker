using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefullStuff;
using TMPro;

public class BlockManager : MonoBehaviour
{
    [SerializeField]GameObject EmptyBlock;

    public GameObject OutputNodePrefab;
    public GameObject InputNodePrefab;

    public List<string> talkers = new();

    public static BlockManager instance ;
    private void Awake()
    {
        instance = this;
    }

    public Action RefreshDialog;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A))
        {
            GameObject a = CreateEmptyBlock();
            a.GetComponent<LineBlock>().ID = SaveLoadSystem.instance.GenerateID;
        }
    }

    public GameObject CreateEmptyBlock()
    {
        GameObject a = Instantiate(EmptyBlock, UsefullLib.GetMousePos(), Quaternion.identity, null);
        CardSortingManager.instance.PutCardOnTop(a);
        return a;
    }


    [SerializeField] TMP_InputField TalkerField;

    public void AddTalker()
    {
        string talker = TalkerField.text;

        if(talker == "")
        {
            Holylib.Debug.TextShower.ShowText("Enter a talker name", 4);
            return;
        }

        if (talkers.Contains(talker))
        {
            Holylib.Debug.TextShower.ShowText("Talker '" + talker + "' already exists", 4);
            return;
        }

        talkers.Add(talker);
        RefreshDialog();

        Holylib.Debug.TextShower.ShowText("Talker '"+ talker + "' added", 4);
    }
}
