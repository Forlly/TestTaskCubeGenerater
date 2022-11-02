using System;
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
        _speedMovingField.text = gameModel.GetSpeedMoving();
        _speedSpawningField.text = gameModel.GetSpeedSpawning();
        _xOffsetField.text = gameModel.GetXOffset();
        _yOffsetField.text = gameModel.GetYOffset();

    }
    
    private void Start()
    {
        _speedMovingField.onEndEdit.AddListener(ValidateSpeedMovingInputData);
        _speedSpawningField.onEndEdit.AddListener(ValidateSpeedSpawningInputData);
        _xOffsetField.onEndEdit.AddListener(ValidateXOffsetInputData);
        _yOffsetField.onEndEdit.AddListener(ValidateYOffsetInputData);
        
        _saveButton.onClick.AddListener( (() =>
        {
            int speedSpawning;
            float speedMoving, xOffset, yOffset;
            if (float.TryParse(_speedMovingField.text, out speedMoving) 
                && int.TryParse(_speedSpawningField.text, out speedSpawning)
                && float.TryParse(_xOffsetField.text, out xOffset) 
                && float.TryParse(_yOffsetField.text, out yOffset))
            {
                _gameModel.ParametersEvent?.Invoke(Math.Abs(speedMoving), Math.Abs(speedSpawning), xOffset, yOffset);
            }
        }));
    }


    private void ValidateSpeedMovingInputData(string inputData)
    {
        float exampleData;
        if (!float.TryParse(inputData, out exampleData) )
        {
            _speedMovingField.image.color = new Color(1f, 0f, 0.08f, 0.54f);
        }
        else
        {
            _speedMovingField.image.color = new Color(0.32f, 1f, 0.23f, 0.54f);
        }
    }
    private void ValidateXOffsetInputData(string inputData)
    {
        float exampleData;
        if (!float.TryParse(inputData, out exampleData) )
        {
            _xOffsetField.image.color = new Color(1f, 0f, 0.08f, 0.54f);
        }
        else
        {
            _xOffsetField.image.color = new Color(0.32f, 1f, 0.23f, 0.54f);
        }
    }
    private void ValidateYOffsetInputData(string inputData)
    {
        float exampleData;
        if (!float.TryParse(inputData, out exampleData) )
        {
            _yOffsetField.image.color = new Color(1f, 0f, 0.08f, 0.54f);
        }
        else
        {
            _yOffsetField.image.color = new Color(0.32f, 1f, 0.23f, 0.54f);
        }
    }
    
    private void ValidateSpeedSpawningInputData(string inputData)
    {
        int exampleData;
        if (!int.TryParse(inputData, out exampleData))
        {
            _speedSpawningField.image.color = new Color(1f, 0f, 0.08f, 0.54f);
        }
        else
        {
            _speedSpawningField.image.color = new Color(0.32f, 1f, 0.23f, 0.54f);
        }
    }
    
    
    
}
