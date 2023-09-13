using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraSizeSpeed;
    [SerializeField] private float cameraMinSize;
    [SerializeField] private float cameraMaxSize;

    [SerializeField] private float currentCameraSize = 5f;

    private Vector2 lastMousePosition;

    private void Update()
    {
        CameraMove();
        CameraSize();
    }

    private void CameraMove()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 delta = worldPosition - lastMousePosition;

        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            Camera.main.transform.position -= (Vector3)delta;
            worldPosition -= delta;
        }

        lastMousePosition = worldPosition;
    }

    private void CameraSize()
    {
        currentCameraSize -= Input.GetAxis("Mouse ScrollWheel") * cameraSizeSpeed;
        currentCameraSize = Mathf.Clamp(currentCameraSize, cameraMinSize, cameraMaxSize);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, currentCameraSize, Time.deltaTime * 10);
    }
}