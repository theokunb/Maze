using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Glass _glassTemplate;

    private int _size;
    public int Size => _size;

    public event Action SizeChanged;

    public void SetSize(int size)
    {
        transform.localScale = new Vector3(size, size, size);
        _size = size;

        SizeChanged?.Invoke();
    }
}
