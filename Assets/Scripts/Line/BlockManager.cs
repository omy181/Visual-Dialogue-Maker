using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefullStuff;

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

        talkers.Add("Woman");
        talkers.Add("Me");
    }
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
}
