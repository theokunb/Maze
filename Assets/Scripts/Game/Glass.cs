using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private GameObject _wallTemplate;

    private void Start()
    {
        transform.localPosition = new Vector3(0, _wallTemplate.transform.localScale.y + transform.localScale.y / 2, 0);
        transform.localScale = new Vector3(2 * _wallTemplate.transform.localScale.x * _wallTemplate.transform.localScale.y + transform.localScale.x,
            transform.localScale.y,
            2 * _wallTemplate.transform.localScale.x * _wallTemplate.transform.localScale.y + transform.localScale.z);
    }
}
