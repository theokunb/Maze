using UnityEngine;

public class CellInsideView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private CellInside _cellInside;

    public void Setup(CellView cellView, Material material)
    {
        _meshRenderer.material = material;
        _cellInside = cellView.CellInside;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (_cellInside == CellInside.Finish)
            {
                var gameService = ServiceLocator.Instance.GetService<GameService>();
                if (gameService == null)
                {
                    return;
                }

                gameService.OnFinish();
            }
            else if (_cellInside == CellInside.Hole)
            {
                var toyService = ServiceLocator.Instance.GetService<ToysService>();
                if (toyService != null)
                {
                    toyService.ResetToyRotation();
                }

                player.ReturnToStart();
            }
        }
    }
}
