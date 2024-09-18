using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomSpeed = 10f;
    public float moveSpeed = 5f;
    public float minFov = 15f;
    public float maxFov = 90f;
    public float minOrthographicSize = 5f;
    public float maxOrthographicSize = 20f;

    private Vector3 dragOrigin;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        HandleZoom();
        HandleMovement();
    }

    void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (mainCamera.orthographic == false)
        {
            if (scrollInput != 0.0f)
            {
                float newFov = mainCamera.fieldOfView - scrollInput * zoomSpeed;
                mainCamera.fieldOfView = Mathf.Clamp(newFov, minFov, maxFov);
            }
        }
        else
        {
            if (scrollInput != 0.0f)
            {
                float newSize = mainCamera.orthographicSize - scrollInput * zoomSpeed;
                mainCamera.orthographicSize = Mathf.Clamp(newSize, minOrthographicSize, maxOrthographicSize);
            }
        }
    }

    void HandleMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 difference = mainCamera.ScreenToWorldPoint(dragOrigin) - mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mainCamera.transform.position += difference;
        dragOrigin = Input.mousePosition;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        mainCamera.transform.Translate(new Vector3(h, v, 0) * moveSpeed * Time.deltaTime, Space.World);
    }
}
