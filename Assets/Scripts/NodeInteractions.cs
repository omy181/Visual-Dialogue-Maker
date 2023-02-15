using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteractions : MonoBehaviour
{
    public GameObject NodeHold;

    
    public GameObject SeenNode;

    public static NodeInteractions instance;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mouseRay.origin, mouseRay.direction);

        GameObject hitobj = GetComponent<BlockInteractions>().TopHitOBJ(hits);
        if(hitobj != null)
        {
            if(hitobj.tag != "IONode")
            {
                hitobj = null;
            }
        }

        SeenNode = hitobj;


        
            if (Input.GetMouseButton(0)) { Hold(); }
            if (Input.GetMouseButtonUp(0)) { Connect(); UnHold(); }
       
    }

    void Hold()
    {
        if (NodeHold != null || FindObjectOfType<BlockInteractions>().CardHold != null) { return; }

        if (SeenNode != null)
        {
            SeenNode.GetComponent<IONode>().LoseConnection();
            NodeHold = SeenNode;
            NodeHold.GetComponent<IONode>().DrawConnection();


        }
    }

    void UnHold()
    {
        if (NodeHold == null) { return; }

        NodeHold.GetComponent<IONode>().NodeBlock.GetComponent<LineBlock>().RefreshNodes();
        NodeHold.GetComponent<IONode>().LoseConnection();

        // create new block
      //  GameObject a = BlockManager.instance.CreateEmptyBlock();
      //  ConnectNodesAuto(NodeHold.GetComponent<IONode>().NodeBlock.GetComponent<LineBlock>(),a.GetComponent<LineBlock>());

            NodeHold = null;       
    }

    void Connect()
    {
        if(NodeHold == null || SeenNode == null) { return; }
        if((int)NodeHold.GetComponent<IONode>().iotype.ioType + (int)SeenNode.GetComponent<IONode>().iotype.ioType != 0) { return; }

        SeenNode.GetComponent<IONode>().LoseConnection();
        SeenNode.GetComponent<IONode>().ConnectedNode = NodeHold;
        NodeHold.GetComponent<IONode>().ConnectedNode = SeenNode;       
        NodeHold.GetComponent<IONode>().DrawConnection();

        RefreshNodes(NodeHold.GetComponent<IONode>());

        NodeHold = null;
    }


    public void ConnectNodesAuto(LineBlock from, LineBlock to)
    {

        NodeHold = GetEmptyNode(from.OutputsParent).gameObject;
        SeenNode = GetEmptyNode(to.InputsParent).gameObject;

        Connect();

        UnHold();
    }

    IONode GetEmptyNode(Transform lb)
    {
            for(int i = 0;i < lb.childCount; i++)
            {
                if( !lb.GetChild(i).GetComponent<IONode>().ConnectedNode)
                {
                    return lb.GetChild(i).GetComponent<IONode>();
                }
            }

        Debug.LogError("Get Empty Node NULL");
        return null;
    }

    public void RefreshNodes(IONode node)
    {
        node.NodeBlock.GetComponent<LineBlock>().RefreshNodes();
        if(node.ConnectedNode) node.ConnectedNode.GetComponent<IONode>().NodeBlock.GetComponent<LineBlock>().RefreshNodes();
    }
}
