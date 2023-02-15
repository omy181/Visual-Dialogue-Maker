using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefullStuff;
public class CamMovement : MonoBehaviour
{
    Vector3 InitPos;
    bool isMoving;
    public Transform Player;
    public float FocusSpeed;

    [Header("Zoom")]
    public float ZoomSensitivity;
    public float MaxZoom;
    public float MinZoom;
    void Update()
    {
        // Cam Movement
        if (Input.GetMouseButton(2))
        {
            if(!isMoving)
            {
                InitPos = UsefullLib.GetMousePos();
                isMoving = true;
            }
        }
        else
        {
            isMoving = false;
        }

        // Cam Zoom
        Zoom();

        // Return Back to Player

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Focus();
        }
    }

    public void Focus()
    {
        StartCoroutine(Focus(Player.position));
    }

    IEnumerator Focus(Vector3 pos)
    {
        pos = new Vector3(pos.x, pos.y, transform.position.z);
        bool cond = false;
        while (!cond)
        {
            cond = (Vector3.Angle(transform.position,pos)<5 && pos.magnitude - transform.position.magnitude < 3f) || isMoving;
            transform.position = Vector3.Slerp(transform.position,pos, Time.deltaTime * FocusSpeed);
            yield return new WaitForSeconds(0.01f);

        }

        
    }
    void Zoom()
    {
        float scrool = Input.GetAxis("Mouse ScrollWheel") * (-ZoomSensitivity);
        float size = GetComponent<Camera>().orthographicSize + scrool;
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(size, MinZoom, MaxZoom);
    }

    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        if (isMoving == false) { return; }
        Vector3 curpos = UsefullLib.GetMousePos();

        Vector3 Destination = (curpos - transform.position);

        transform.position = InitPos - Destination;
    }
}
