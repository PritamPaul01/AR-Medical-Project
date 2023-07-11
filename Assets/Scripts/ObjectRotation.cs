using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 2f;
    public float rotationFactor = 5f;
    public float zoomSpeed = 0.001f;
    public bool rotationEnabled = true;

    private bool isRotating = false;
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }
    void Update()
    {

        if (rotationEnabled && Input.touchCount == 1)
        {
            //Handle rotation
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isRotating = true;
                    break;
                case TouchPhase.Moved:
                    if (isRotating)
                    {
                        float rotationX = -touch.deltaPosition.y * rotationSpeed * Time.deltaTime * rotationFactor;
                        float rotationY = touch.deltaPosition.x * rotationSpeed * Time.deltaTime * rotationFactor;

                        transform.Rotate(rotationX, rotationY, 0, Space.World);
                    }
                    break;
                case TouchPhase.Ended:
                    isRotating = false;
                    break;
            }
        }
         // Handle pinch-to-zoom
         if (rotationEnabled && Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

            float prevMagnitude = (touch1PrevPos - touch2PrevPos).magnitude;
            float currentMagnitude = (touch1.position - touch2.position).magnitude;

            float deltaMagnitude = prevMagnitude - currentMagnitude;

            Vector3 newScale = transform.localScale + Vector3.one * deltaMagnitude * zoomSpeed;
            transform.localScale = newScale;
        }
        
    }
    public void ResetObject()
    {
        transform.rotation = Quaternion.identity;
        transform.localScale = initialScale;
        gameObject.SetActive(true);
    }
}