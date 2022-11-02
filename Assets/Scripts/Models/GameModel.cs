using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Models
{
    public class GameModel
    {
        public ObjectsPoolModel ObjectsPoolModel;
        public Action<float, int, float, float> ParametersEvent;
        
        private float _speedMoving;
        private int _spawningDelay;
        private float _xOffset;
        private float _yOffset;

        private List<IUnit> _units = new List<IUnit>();
        private bool _onSimulation;
        private int _currentSpawningDelay;
        
        public async void Init()
        {
            _speedMoving = 1f;
            _spawningDelay = 4000;
            _currentSpawningDelay = _spawningDelay;
            _xOffset = 10f;
            _yOffset = 10f;
            
            SubscribeEvents();
            
            ObjectsPoolModel = new ObjectsPoolModel();
            ObjectsPoolModel.Init();
            await Tick(10);
        }

        public async Task Tick(int msec)
        {
            _onSimulation = true;
            
            while (_onSimulation)
            {
                TrySpawnObjects(msec);
                
                foreach (IUnit unit in _units.Reverse<IUnit>())
                {
                    if (!unit.Move())
                    {
                        ObjectsPoolModel.TurnOfObject(unit);
                        _units.Remove(unit);
                    }
                }

                await Task.Delay(msec);
            }
            
            EndModel();
        }
        
        private void SubscribeEvents()
        {
            ParametersEvent += OnParametersChanged;
        }
   
        private void UnsubscribeEvents()
        {
            ParametersEvent -= OnParametersChanged;
        }

        public void OnParametersChanged(float speedMoving, int speedSpawning, float xOffset, float yOffset)
        {
            _speedMoving = speedMoving;
            _spawningDelay = speedSpawning;
            _xOffset = xOffset;
            _yOffset = yOffset;

            UpdateUnitsData();
        }

        private void UpdateUnitsData()
        {
            foreach (IUnit unit in _units)
            {
                unit.TargetPosition = new Vector3(_xOffset, _yOffset, 1);
                unit.SpeedMoving = _speedMoving;
            }
        }
        
        private void TrySpawnObjects(int msec)
        {
            _currentSpawningDelay += msec;

            if (_spawningDelay <= _currentSpawningDelay)
            {
                _currentSpawningDelay -= _spawningDelay;
                
                IUnit unit = ObjectsPoolModel.GetPooledObject();
                
                unit.TargetPosition = new Vector3(_xOffset, _yOffset, 1);
                unit.SpeedMoving = _speedMoving;
                
                _units.Add(unit);
            }
        }

        public void EndModel()
        {
            UnsubscribeEvents();
            _onSimulation = false;
        }
        
    }
}