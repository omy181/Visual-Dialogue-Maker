using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSLayoutGroup : MonoBehaviour
{
    [SerializeField] Transform Parent;
    [SerializeField] Vector3 GlobalOffset;
    [SerializeField] Vector3 LocalOffset;

    void Update()
    {
        Vector3 pos = transform.position + GlobalOffset;

        for(int i = 0; i < Parent.transform.childCount; i++)
        {
            Parent.GetChild(i).transform.position = pos;

            pos += LocalOffset;
        }
    }
}
