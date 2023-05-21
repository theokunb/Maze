using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform _startPoint;
    private Vector3 _offset = new Vector3(0, 0.5f, 0);

    public void Setup(Transform startPoint)
    {
        _startPoint = startPoint;
    }

    public void ReturnToStart()
    {
        transform.position = _startPoint.position + _offset;
    }
}
