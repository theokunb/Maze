using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour, IService
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _finishMenu;

}
