using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform gameFieldCentre;
    private float rotationSpeed = 75f;
    private Touch initTouch;
    private float swypeRotationValue;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(gameFieldCentre.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(gameFieldCentre.position, Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        swypeRotationValue = Mathf.Abs(swypeRotationValue) < 1 ? 0 : swypeRotationValue * 0.9f;
        transform.RotateAround(gameFieldCentre.position, Vector3.up, swypeRotationValue * Time.deltaTime);

        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case (TouchPhase.Began):
                    initTouch = touch;
                    break;

                case (TouchPhase.Moved):
                    swypeRotationValue = touch.position.x - initTouch.position.x;
                    break;
            }
        }
    }

    public IEnumerator RotateCamera(float angle)
    {
        float currentAngle = 0f;
        while (true)
        {
            float step = rotationSpeed * Time.deltaTime;

            if(angle < 0)
            {
                step *= -1;
            }

            if (Mathf.Abs(currentAngle + step) > Mathf.Abs(angle))
            {
                step = angle - currentAngle;
                transform.Rotate(Vector3.right, step);
                break;
            }

            currentAngle += step;
            transform.Rotate(Vector3.right, step);
            yield return null;
        }
    }
}
