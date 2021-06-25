using System;
using System.Collections;
using System.Collections.Generic;
using Clicker.UnityComponents;
using Client;
using Leopotam.Ecs;
using UI.Components;
using UnityEngine;

namespace UI.UnityButtonsScripts
{
    public class CreateContinueEventSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;

        private readonly CanvasUnityComponent _canvasUnityComponent = null;

        public void Init()
        {
            _canvasUnityComponent.continueButton.onClick.AddListener(() =>
            {
                _world.NewEntity().Get<ContinueEventComponent>();
            });
        }
    }
}