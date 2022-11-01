using System;
using UnityEngine;

public class UserPanelModel
{
   private float _speedMoving;
   private float _speedSpawning;
   private float _xOffset;
   private float _yOffset;

   public Action<float, float, float, float> ParametersEvent;

   public void Init()
   {
      SubscribeEvents();
   }

   private void SubscribeEvents()
   {
      ParametersEvent += OnParametersChanged;
   }
   
   private void UnsubscribeEvents()
   {
      ParametersEvent -= OnParametersChanged;
   }

   public void OnParametersChanged(float speedMoving, float speedSpawning, float xOffset, float yOffset)
   {
      Debug.Log(speedMoving);
   }
}
