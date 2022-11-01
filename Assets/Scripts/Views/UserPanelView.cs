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
    private UserPanelModel _userPanelModel;

    public void Init(GameModel gameModel)
    {
        _userPanelModel = gameModel.UserPanelModel;
    }
    
    private void Start()
    {
        _saveButton.onClick.AddListener( (() =>
        {
            float speedMoving, speedSpawning, xOffset, yOffset;
            if (float.TryParse(_speedMovingField.text, out speedMoving) 
                && float.TryParse(_speedSpawningField.text, out speedSpawning)
                && float.TryParse(_xOffsetField.text, out xOffset) 
                && float.TryParse(_yOffsetField.text, out yOffset))
            {
                _userPanelModel.ParametersEvent?.Invoke(speedMoving, speedSpawning, xOffset, yOffset);
            }
        }));
    }
    
}
