using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefullStuff;
using UnityEngine.Rendering;
public class NodeConnections : MonoBehaviour
{

    public LineRenderer ConnectionRenderer;
    public Vector3 StartPos;
    public Vector3 EndPos;
    public bool isConnected;

    [Header("Bezier Points")]
    public Transform P2;
    public Transform P3;
    void Update()
    {
        if (isConnected)
        {
            SetPos();

            CheckConnectionSortLayer();
        }
    }

    void SetPos()
    {
            if (GetComponent<IONode>().ConnectedNode == null)
            {
                EndPos = UsefullLib.GetMousePos();
            }
            else
            {
                EndPos = GetComponent<IONode>().ConnectedNode.transform.position;
            }
            StartPos = transform.position;

            ConnectionRenderer.SetPosition(0, StartPos);
            ConnectionRenderer.SetPosition(ConnectionRenderer.positionCount - 1, EndPos);       
       
        for(int i = 1;i< ConnectionRenderer.positionCount-1; i++)
        {

            float partsize = (EndPos.x - StartPos.x) / (ConnectionRenderer.positionCount - 1);
            float HeightDifference = Mathf.Abs(EndPos.y - StartPos.y);
            Vector3 pos = new Vector3(StartPos.x + partsize * i, 0, StartPos.z);
            float t = (float)i / (float)ConnectionRenderer.positionCount;
            //  P2.position = new Vector3(StartPos.x + (EndPos.x - StartPos.x)/2,EndPos.y, 0);
            //  P3.position = new Vector3(StartPos.x + (EndPos.x - StartPos.x) / 2,StartPos.y, 0);
          
            P2.position = new Vector3(StartPos.x , StartPos.y + (EndPos.y - StartPos.y) / 2, 0);
             P3.position = new Vector3(EndPos.x ,StartPos.y + (EndPos.y - StartPos.y) / 2, 0);

            Vector3 Bezier = UsefullLib.CalculateBezierPos(StartPos,P2.position,P3.position,EndPos,t);
            pos = new Vector3(Bezier.x, Bezier.y, pos.z);
            
            ConnectionRenderer.SetPosition(i, pos);
        }
        
    }

    public void Connect(bool state)
    {
        SetPos();

        isConnected = state;
        ConnectionRenderer.enabled = state;

        if (!state)
        {
            ConnectionRenderer.transform.SetParent(this.transform);
        }
        else
        {
            ConnectionRenderer.transform.SetParent(null);           
        }
    }

    void CheckConnectionSortLayer()
    {
        int parentorder = transform.parent.parent.GetComponent<SortingGroup>().sortingOrder;

        if (GetComponent<IONode>().ConnectedNode != null)
        {
            int connectedorder = GetComponent<IONode>().ConnectedNode.GetComponent<IONode>().NodeBlock.GetComponent<SortingGroup>().sortingOrder;

            if (connectedorder > parentorder)
            {
                ConnectionRenderer.sortingOrder = parentorder - 1;
            }
            else
            {
                ConnectionRenderer.sortingOrder = connectedorder - 1;
            }
        }
        else
        {
            ConnectionRenderer.sortingOrder = parentorder - 1;
        }

    }
}
