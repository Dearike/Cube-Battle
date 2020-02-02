using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    [SerializeField] private float time;
    void Start()
    {
        Destroy(this, time);
    }
}
