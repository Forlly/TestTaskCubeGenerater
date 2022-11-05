using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Models
{
    public class GameModel
    {
        public int AmountPool;
        public int TickTime;
        public ObjectsPoolModel ObjectsPoolModel;
        public Action<float, int, float, float> ParametersEvent;
        
        private float _speedMoving;
        private int _spawningDelay;
        private float _xOffset;
        private float _yOffset;

        private List<IUnit> _unitsOnScreen = new List<IUnit>();
        private bool _onSimulation;
        private int _currentSpawningDelay;
        
        public async void Init()
        {
            _speedMoving = 0.1f;
            _spawningDelay = 2000;
            _currentSpawningDelay = _spawningDelay;
            _xOffset = 20f;
            _yOffset = 0f;

            AmountPool = 128;
            TickTime = 2;
            
            SubscribeEvents();
            
            ObjectsPoolModel = new ObjectsPoolModel();
            ObjectsPoolModel.Init(this);
            await Tick(TickTime);
        }

        public async Task Tick(int msec)
        {
            _onSimulation = true;

            while (_onSimulation)
            {
                Debug.Log(ObjectsPoolModel.GetPoolCount);
                TrySpawnObjects(msec);
                
                foreach (IUnit unit in _unitsOnScreen.Reverse<IUnit>())
                {
                    if (!unit.Move())
                    {
                        ObjectsPoolModel.TurnOfObject(unit);
                        _unitsOnScreen.Remove(unit);
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

        public string GetSpeedMoving()
        {
            return _speedMoving.ToString();
        }
        
        public string GetYOffset()
        {
            return _yOffset.ToString();
        }
        
        public string GetXOffset()
        {
            return _xOffset.ToString();
        }
        
        public string GetSpeedSpawning()
        {
            return _spawningDelay.ToString();
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
            foreach (IUnit unit in _unitsOnScreen)
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
                
                _unitsOnScreen.Add(unit);
            }
        }

        public void EndModel()
        {
            UnsubscribeEvents();
            _onSimulation = false;
        }
        
    }
}