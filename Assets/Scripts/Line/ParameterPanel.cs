using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParameterPanel : MonoBehaviour
{
    [SerializeField] TMP_Dropdown ParameterField;
    [SerializeField] TMP_Dropdown OperatorField;
    [SerializeField] TMP_InputField ValueField;
    void Start()
    {
        BlockManager.instance.RefreshDialog += RefreshParameters;

        if (ParameterField.options.Count == 0)
            SetParameters();
    }

    public Parameter SaveParameter()
    {
        Parameter p = new();
        p.Param = ParameterField.value;
        p.Operator = OperatorField.value;
        p.Value = float.Parse(ValueField.text);

        return p;
    }
    public void RefreshParameters()
    {
        int v = ParameterField.value;
        ParameterField.ClearOptions();
        ParameterField.AddOptions(BlockManager.instance.parameters);
        ParameterField.value = v;
    }

    public void SetParameters(int Paramvalue = 0, int Opervalue = 0, float value = 0)
    {
        ParameterField.ClearOptions();
        ParameterField.AddOptions(BlockManager.instance.parameters);
        ParameterField.value = Paramvalue;

        OperatorField.ClearOptions();
        OperatorField.AddOptions(BlockManager.instance.operators);
        OperatorField.value = Opervalue;

        ValueField.text = value.ToString();
    }

    public void DeleteParameter()
    {
        Destroy(this.gameObject);
    }
}
