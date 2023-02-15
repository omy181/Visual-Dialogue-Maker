using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Holylib.Debug
{
    public class DebugManager : MonoBehaviour
    {
        public static DebugManager instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }

            instance = this;
            DebugUI = Instantiate(DebugUI);
        }
    

    [Header("Prefabs")]
    public GameObject DebugUI;
    public GameObject TextPrefab;
    public List<GameObject> TextObjects;
    [Header("Text Positions")]
    [Tooltip("Give a value between 0 - 1")]
    public Vector2 TextOffset;
    [Tooltip("Give a value between 0 - 1")]
    public float TextDistance;


        [Space]
        public GameObject[] ValueObjects = new GameObject[10];
        [Header("ValuePositions")]
        [Tooltip("Give a value between 0 - 1")]
        public Vector2 ValueOffset;
        [Tooltip("Give a value between 0 - 1")]
        public float ValueDistance;

        // TEXTS
        public void ShowText(string text, float duration)
    {
        GameObject textobject = Instantiate(TextPrefab, DebugUI.transform);
        TextObjects.Add(textobject);
        textobject.GetComponent<TextMeshProUGUI>().text = text;
        StartCoroutine(DestroyText(textobject, duration));
        RefreshText();
    }

    void RefreshText()
    {
        for (int i = 0; i < TextObjects.Count; i++)
        {
            Vector3 pos = (TextOffset + new Vector2(0, -(TextObjects.Count - i) * TextDistance));
            pos = new Vector3(pos.x * Screen.width, pos.y * Screen.height, pos.z);
            TextObjects[i].transform.position = pos;
        }
    }
    IEnumerator DestroyText(GameObject text, float duration)
    {
        yield return new WaitForSeconds(duration);
        TextObjects.Remove(text);
        Destroy(text);
        RefreshText();
    }
        // VALUES
        public void ShowValue(string value, int id = 0)
        {
            GameObject valueobject;

            if (ValueObjects[id] == null)
            {
                valueobject = Instantiate(TextPrefab, DebugUI.transform);
                ValueObjects[id] = valueobject;
            }
            else
            {
                valueobject = ValueObjects[id];
            }

            
            valueobject.GetComponent<TextMeshProUGUI>().text = value;
            RefreshValue(id);
        }

        void RefreshValue(int id)
        {
                Vector3 pos = (ValueOffset + new Vector2(0, -id * ValueDistance));
                pos = new Vector3(pos.x * Screen.width, pos.y * Screen.height, pos.z);
                ValueObjects[id].transform.position = pos;           
        }
    }

    public static class TextShower
    {
        public static void ShowText(string text, float duration)
        {
            DebugManager.instance.ShowText(text, duration);
        }

        public static void ShowValue(string value,int id = 0)
        {
            DebugManager.instance.ShowValue(value,id);
        }
    }
}
