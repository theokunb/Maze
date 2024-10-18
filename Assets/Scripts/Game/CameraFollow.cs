using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Base _base;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        _base.SizeChanged += OnBaseSizeChanged;
    }

    private void OnDisable()
    {
        _base.SizeChanged -= OnBaseSizeChanged;
    }

    private void OnBaseSizeChanged()
    {
        const float factor = 1.7f;

        transform.position = _startPosition - transform.forward * _base.Size * factor;
    }
}
