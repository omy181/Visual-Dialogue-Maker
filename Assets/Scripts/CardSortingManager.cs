using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class CardSortingManager : MonoBehaviour
{
    public List<GameObject> cardList = new List<GameObject>();

    public static CardSortingManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        foreach(GameObject card in GameObject.FindGameObjectsWithTag("LineBlock"))
        {
            cardList.Add(card);
        }

        SetSortOrder();
    }

    void SetSortOrder()
    {
        for (int i =0;i<cardList.Count;i++)
        {
            cardList[i].GetComponent<SortingGroup>().sortingOrder = i;
        }        
    }

    public void PutCardOnTop(GameObject card)
    {
        cardList.Remove(card);
        cardList.Add(card);

        SetSortOrder();
    }

    public void NewCardInitialize(GameObject card)
    {
        cardList.Add(card);

        SetSortOrder();
    }

    public void CardDestroy(GameObject card)
    {
        cardList.Remove(card);

        SetSortOrder();
    }

    public void ClearEveryCard()
    {
        int count = cardList.Count;
        int j = 0;
        for (int i = 0;i<count;i++)
        {
            if (!cardList[j].GetComponent<LineBlock>().Delete()) j++;
        }

        SaveLoadSystem.instance.lastID = 0;

        Holylib.Debug.TextShower.ShowText("Cleared", 4);
    }
}
