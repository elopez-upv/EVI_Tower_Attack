using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;
    private float panSpeed = 8f;

    private float minX = -5f;
    private float maxX = 5f;
    private float minY = -5f;
    private float maxY = 5f;
    private float temp;

    private float zoomSpeed = 1.5f;
    private float minZoom = 4.0f;
    private float maxZoom = 7.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if (!doMovement)
        {
            return;
        }

        Vector3 pos = transform.position;

        if (Input.GetKey("w")){
            pos.y += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s")){
            pos.y -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("d")){
            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a")){
            pos.x -= panSpeed * Time.deltaTime;
        }
        
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera camera = GetComponent<Camera>();

        float newSize = camera.orthographicSize - scroll * zoomSpeed;

        newSize = Mathf.Clamp(newSize, minZoom, maxZoom);

        camera.orthographicSize = newSize;

        transform.position = pos;
    }
}
