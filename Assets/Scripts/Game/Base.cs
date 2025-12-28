using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Glass _glassTemplate;

    private int _size;
    private bool _initialized = false;

    public int Size => _size;

    public void SetSize(int size)
    {
        if(_initialized == true)
        {
            return;
        }

        transform.localScale = new Vector3(size, size, size);
        _size = size;

        var cameraFollow = ServiceLocator.Instance.GetService<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.SetBase(this);
            cameraFollow.OnBaseSizeChanged();
        }

        _initialized = true;
    }
}
