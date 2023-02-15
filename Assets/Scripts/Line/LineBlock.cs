using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LineBlock : MonoBehaviour
{
    [SerializeField] TMP_InputField TalkerField;
    [SerializeField] TMP_InputField LineField;
    [SerializeField] TMP_InputField ChoiceLabelField;

    public int ID;

    public Line line;
    public bool hasused;

    public Transform OutputsParent;
    public Transform InputsParent;

    [SerializeField] bool StaticBlock;

    private void Start()
    {
        RefreshNodes();
    }

    public void RefreshNodes()
    {

        return;
        /*
        if (OutputsParent)
        {
            for (int i = 0; i < OutputsParent.childCount; i++)
            {
                if (!OutputsParent.GetChild(i).GetComponent<IONode>().ConnectedNode)
                {
  
                        Destroy(OutputsParent.GetChild(i).gameObject);
                   
                }
            }

            Instantiate(BlockManager.instance.OutputNodePrefab, OutputsParent);
        }

        if (InputsParent)
        {

            for (int i = 0; i < InputsParent.childCount; i++)
            {
                if (!InputsParent.GetChild(i).GetComponent<IONode>().ConnectedNode)
                {
                        Destroy(InputsParent.GetChild(i).gameObject);
                }
            }

            Instantiate(BlockManager.instance.InputNodePrefab, InputsParent);
        }*/
    }
    public Line Save()
    {
        Line l = new Line();

        

        if (hasused)
        {
            return line;
        }
        else
        {

            l.ChoiceLabel = ChoiceLabelField.text;
            l.LineText = LineField.text;
            l.Talker = TalkerField.text;

            l.ID = ID;

            l.Next = new List<Line>();

            for (int i = 0; i < OutputsParent.childCount; i++)
            {
                if (OutputsParent.GetChild(i).GetComponent<IONode>().ConnectedNode)
                    l.Next.Add(OutputsParent.GetChild(i).GetComponent<IONode>().ConnectedNode.GetComponent<IONode>().NodeBlock.Save());
            }

            line = l;
            hasused = true;
        }

        return l;
    }

    public void Load(Line l,Vector3 pos)
    {

        ChoiceLabelField.text = l.ChoiceLabel;
        LineField.text = l.LineText;
        TalkerField.text = l.Talker;
        ID = l.ID;

        SaveLoadSystem.instance.lineblocks.Add(this);

        for (int i = 0; i < l.Next.Count; i++)
        {
            LineBlock lb = SaveLoadSystem.instance.DoesIDExist(l.Next[i].ID);

            if (lb)
            {
                NodeInteractions.instance.ConnectNodesAuto(this, lb);
            }
            else
            {
                
                GameObject block = BlockManager.instance.CreateEmptyBlock();

                block.transform.position = pos + new Vector3(5, i * 3, 0);

                block.GetComponent<LineBlock>().Load(l.Next[i], block.transform.position);

                NodeInteractions.instance.ConnectNodesAuto(this, block.GetComponent<LineBlock>());
            }

            
        }
    }

    public void CleanPastData()
    {
        hasused = false;

        for (int i = 0; i < OutputsParent.childCount; i++)
        {
            if (OutputsParent.GetChild(i).GetComponent<IONode>().ConnectedNode)
                OutputsParent.GetChild(i).GetComponent<IONode>().ConnectedNode.GetComponent<IONode>().NodeBlock.CleanPastData();
        }
    }

    public void DeleteForUI()
    {
        Delete();
    }
    public bool Delete()
    {
        if (StaticBlock)
        {
            ChoiceLabelField.text = "";
            LineField.text = "";
            TalkerField.text = "";
            return false;
        }


        if (OutputsParent)
            for (int i = 0; i < OutputsParent.childCount; i++)
            { 
                OutputsParent?.GetChild(i)?.GetComponent<IONode>().LoseConnection();
            }

        if(InputsParent)
            for (int i = 0; i < InputsParent.childCount; i++)
            {
                InputsParent?.GetChild(i)?.GetComponent<IONode>().LoseConnection();
            }

        CardSortingManager.instance.CardDestroy(this.gameObject);
         Destroy(this.gameObject);

        return true;
    }
}
