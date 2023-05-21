using System;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private Glass _glassTemplate;

    public int Size => _size;

    private void Awake()
    {
        transform.localScale = new Vector3(_size, _size, _size);
    }
}
