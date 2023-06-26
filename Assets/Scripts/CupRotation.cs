using UnityEngine;

public class CupRotation : MonoBehaviour
{
    private float rotationSpeed = 1f;
    private bool isRotatingByMouse = false;
    private float rotationDirection = 1f;
    private float rotationToContinue = 1f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotatingByMouse = true;
        }
        else if (Input.GetMouseButton(0))
        {
            rotationDirection = Input.GetAxis("Mouse X");
            rotationToContinue = Mathf.Sign(rotationDirection);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotatingByMouse = false;
        }
        if (isRotatingByMouse)
        {
            transform.Rotate(0f, rotationDirection * rotationSpeed * -1 * 3, 0f);
        }
    }

    void FixedUpdate()
    {
        if(!isRotatingByMouse)
        {
            transform.Rotate(0f, rotationToContinue * rotationSpeed * -1, 0f);
        }
    }
}
