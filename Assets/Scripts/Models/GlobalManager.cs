using Models;
using UnityEngine;
using Views;

public class GlobalManager : MonoBehaviour
{
    [SerializeField] private ViewManager _viewManager;
    private GameModel _gameModel;
    private void Awake()
    {
        _gameModel = new GameModel();
        _gameModel.Init();
        _viewManager.Init(_gameModel);
    }
}
