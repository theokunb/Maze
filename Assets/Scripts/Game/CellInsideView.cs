using System;
using UnityEngine;

public class CellInsideView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private CellInside _cellInside;

    public event Action<CellInsideView> FinishAchived;
    public event Action HoleAchived;

    public void Setup(CellView cellView, Material material)
    {
        _meshRenderer.material = material;
        _cellInside = cellView.CellInside;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            if(_cellInside == CellInside.Finish)
            {
                FinishAchived?.Invoke(this);
            }
            else if(_cellInside == CellInside.Hole)
            {
                HoleAchived?.Invoke();
            }
        }
    }
}
