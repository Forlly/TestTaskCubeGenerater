using System;
using Models;
using UnityEngine;

namespace Views
{
    public class ViewManager : MonoBehaviour
    {
        public static ViewManager Instance;
        
        [SerializeField] private UserPanelView _userPanelView;
        [SerializeField] public ObjectsPoolView _objectsPoolView;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        
        public void Init(GameModel gameModel)
        {
            _userPanelView.Init(gameModel);
            _objectsPoolView.Init();
        }
        
    }
}