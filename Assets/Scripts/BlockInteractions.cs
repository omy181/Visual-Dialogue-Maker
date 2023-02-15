using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefullStuff;
using UnityEngine.Rendering;
public class BlockInteractions: MonoBehaviour
{
    public GameObject CardHold;
    Vector3 mousecardoffset;

    void Update()
    {

        //// Mouse On Card

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
       RaycastHit2D[] hits = Physics2D.RaycastAll(mouseRay.origin,mouseRay.direction);
    
        if (Input.GetMouseButton(0)) { Hold(TopHitOBJ(hits)); }            

        if (Input.GetMouseButtonUp(0)) { UnHold(); }


        /////////////////  Move Card
        ///

        if (CardHold != null)
        {
            Vector3 TargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TargetPos = new Vector3(mousecardoffset.x + TargetPos.x, mousecardoffset.y + TargetPos.y,0);

            CardHold.transform.position = Vector3.MoveTowards(CardHold.transform.position,TargetPos,Time.deltaTime*100);
        }
    }

    void Hold(GameObject card)
    {
        if (CardHold != null || card == null || FindObjectOfType<NodeInteractions>().NodeHold != null) { return; }
        if (card.tag == "IONode") { return; }
        CardHold = card;

        mousecardoffset = card.transform.position - UsefullLib.GetMousePos();

         FindObjectOfType<CardSortingManager>().PutCardOnTop(card);
    }

    void UnHold()
    {
        CardHold = null;
    }

    public GameObject TopHitOBJ(RaycastHit2D[] hits)
    {
        if (hits.Length == 0) { return null; }

        List<RaycastHit2D> cardhitList = new List<RaycastHit2D>();
        List<RaycastHit2D> nodehitlist = new List<RaycastHit2D>();

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "LineBlock")
            {
                cardhitList.Add(hit);
            }

            if (hit.collider.tag == "IONode")
            {
                nodehitlist.Add(hit);
            }
        }

        RaycastHit2D topcardhit = new RaycastHit2D();
        if (cardhitList.Count > 0)
        {
            topcardhit = cardhitList[0];

            for (int i = 1; i < cardhitList.Count; i++)
            {
                if (topcardhit.collider.GetComponent<SortingGroup>().sortingOrder < cardhitList[i].collider.GetComponent<SortingGroup>().sortingOrder)
                {
                    topcardhit = cardhitList[i];
                }
            }
        }

        RaycastHit2D topnodehit = new RaycastHit2D();
        if (nodehitlist.Count > 0)
        {
            topnodehit = nodehitlist[0];

            for (int i = 1; i < nodehitlist.Count; i++)
            {
                if (topnodehit.collider.GetComponent<IONode>().NodeBlock.GetComponent<SortingGroup>().sortingOrder < nodehitlist[i].collider.GetComponent<IONode>().NodeBlock.GetComponent<SortingGroup>().sortingOrder)
                {
                    topnodehit = nodehitlist[i];
                }
            }
        }

        if (cardhitList.Count == 0 && nodehitlist.Count == 0)
        {
            return null;
        }
        if (cardhitList.Count > 0 && nodehitlist.Count == 0)
        {
            return topcardhit.collider.gameObject;
        }
        if (cardhitList.Count == 0 && nodehitlist.Count > 0)
        {
            return topnodehit.collider.gameObject;
        }


        if (topcardhit.collider.GetComponent<SortingGroup>().sortingOrder > topnodehit.collider.GetComponent<IONode>().NodeBlock.GetComponent<SortingGroup>().sortingOrder)
        {
            return topcardhit.collider.gameObject;
        }
        else
        {
            return topnodehit.collider.gameObject;
        }

    }
}
