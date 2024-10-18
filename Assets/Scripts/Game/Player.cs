using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform _startPoint;
    private Vector3 _offset = new Vector3(0, 0.4f, 0);

    public void Setup(Transform startPoint, int size)
    {
        _startPoint = startPoint;
        transform.localScale = transform.localScale * ((float)size / 10);
        _offset = _offset * ((float)size / 10);
    }

    public void ReturnToStart()
    {
        transform.position = _startPoint.position + _offset;
    }
}
