using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserPanelView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _speedMovingField;
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
            float tmp;
            if (float.TryParse(_speedMovingField.text, out tmp))
            {
                _userPanelModel.ParametersEvent?.Invoke(tmp, tmp, tmp, tmp);
            }
        }));
    }
    
}
