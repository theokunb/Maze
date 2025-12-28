using UnityEngine;

public class ToysService : MonoBehaviour, IService
{
    [SerializeField] private Toy Toy6;

    private Toy _toy;

    public Toy GetToyPrefab(int sideCount)
    {
        if(sideCount == 6)
        {
            return Toy6;
        }

        throw new System.Exception("side count not supported");
    }

    public void SetToy(Toy toy) => _toy = toy;
    public void ResetToyRotation()
    {
        if (_toy == null)
        {
            return;
        }

        _toy.transform.rotation = Quaternion.identity;
    }
}
