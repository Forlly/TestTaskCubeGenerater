using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserPanelView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _speedMovingField;
    [SerializeField] private TMP_InputField _speedSpawningField;
    [SerializeField] private TMP_InputField _xOffsetField;
    [SerializeField] private TMP_InputField _yOffsetField;
    [SerializeField] private Button _saveButton;
    private GameModel _gameModel;

    public void Init(GameModel gameModel)
    {
        _gameModel = gameModel;
    }
    
    private void Start()
    {
        _saveButton.onClick.AddListener( (() =>
        {
            int speedSpawning;
            float speedMoving, xOffset, yOffset;
            if (float.TryParse(_speedMovingField.text, out speedMoving) 
                && int.TryParse(_speedSpawningField.text, out speedSpawning)
                && float.TryParse(_xOffsetField.text, out xOffset) 
                && float.TryParse(_yOffsetField.text, out yOffset))
            {
                _gameModel.ParametersEvent?.Invoke(speedMoving, speedSpawning * 1000, xOffset, yOffset);
            }
        }));
    }
    
}
