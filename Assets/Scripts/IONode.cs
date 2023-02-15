using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IONode : MonoBehaviour
{
    public IOType iotype;
    public GameObject ConnectedNode;
    public LineBlock NodeBlock;
    [Space]
    public SpriteRenderer ConnectionIconRenderer;
    [Space]
    public bool isused;
    void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = iotype.Base;
        NodeBlock = transform.parent.parent.GetComponent<LineBlock>();

        ConnectionIconRenderer.sprite = iotype.UnConnectedIcon;
    }

    public void DrawConnection()
    {
        GetComponent<NodeConnections>().Connect(true);

        ConnectionIconRenderer.sprite = iotype.ConnectedIcon;
        if(ConnectedNode != null)
        {
            ConnectedNode.GetComponent<IONode>().ConnectionIconRenderer.sprite = iotype.ConnectedIcon;

            NodeInteractions.instance.RefreshNodes(ConnectedNode.GetComponent<IONode>());
        }

        
    }

    public void LoseConnection()
    {        
        if(ConnectedNode != null)
        {
            ConnectedNode.GetComponent<IONode>().ConnectedNode.GetComponent<NodeConnections>().Connect(false);
            ConnectedNode.GetComponent<IONode>().ConnectedNode = null;
            ConnectedNode.GetComponent<IONode>().ConnectionIconRenderer.sprite = iotype.UnConnectedIcon;
            ConnectedNode.GetComponent<NodeConnections>().Connect(false);

            NodeInteractions.instance.RefreshNodes( ConnectedNode.GetComponent<IONode>());

            ConnectedNode = null;
         
        }

       

        GetComponent<NodeConnections>().Connect(false);

        ConnectionIconRenderer.sprite = iotype.UnConnectedIcon;
    }
  
}
