using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveAmount = 30f;
    [SerializeField] private float zoomChangeAmount = 5f;
    [SerializeField] private float zoomScrollAmount = 30f;
    [SerializeField] private float zoomScrollMultiplier = 10f;
    [SerializeField] private float minZoomAmount = 1f;
    [SerializeField] private float maxZoomAmount = 30f;
    [SerializeField] private Collider2D camMoveArea;

    private void Update()
    {        
        CameraMove();
        CameraZoom();
    }
    private void CameraZoom()
    {
        float zoom = Camera.main.orthographicSize;

        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            zoom -= zoomChangeAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            zoom += zoomChangeAmount * Time.deltaTime;
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                zoom -= zoomScrollAmount * Time.deltaTime * zoomScrollMultiplier;
            }
            else
            {
                zoom -= zoomScrollAmount * Time.deltaTime;
            }

        }
        if (Input.mouseScrollDelta.y < 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                zoom += zoomScrollAmount * Time.deltaTime * zoomScrollMultiplier;
            }
            else
            {
                zoom += zoomScrollAmount * Time.deltaTime;
            }

        }
        zoom = Mathf.Clamp(zoom, minZoomAmount, maxZoomAmount);

        Camera.main.orthographicSize = zoom;
    }
    private void CameraMove()
    {
        Vector3 camPos = transform.position;
        if (Input.GetKey(KeyCode.W) && transform.position.y < camMoveArea.bounds.extents.y)
        {
            camPos.y += moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y > -camMoveArea.bounds.extents.y)
        {
            camPos.y -= moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) && transform.position.x > -camMoveArea.bounds.extents.x)
        {
            camPos.x -= moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < camMoveArea.bounds.extents.x)
        {
            camPos.x += moveAmount * Time.deltaTime;
        }
        transform.position = camPos;
    }
}
