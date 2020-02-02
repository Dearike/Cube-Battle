using UnityEngine;

public class UnitCanvasController : MonoBehaviour
{
    private GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("MainCamera");
    }

    private void Update()
    {
        transform.LookAt(mainCamera.transform);
    }
}
