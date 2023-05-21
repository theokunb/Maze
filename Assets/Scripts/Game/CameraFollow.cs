using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Base _base;

    private void Start()
    {
        const float factor = 1.7f;

        transform.position -= transform.forward * _base.Size * factor;
    }
}
