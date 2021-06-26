using System.Collections;
using System.Collections.Generic;
using Clicker.UnityComponents;
using Leopotam.Ecs;
using UI.Components;
using UnityEngine;

namespace UI.UnityButtonsScripts
{
    public class CreateRestartEventSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;

        private readonly CanvasUnityComponent _canvasUnityComponent = null;

        public void Init()
        {
            _canvasUnityComponent.loseRestartButton.onClick.AddListener(() =>
            {
                _world.NewEntity().Get<RestartEventComponent>();
            });

            _canvasUnityComponent.winRestartButton.onClick.AddListener(() =>
            {
                _world.NewEntity().Get<RestartEventComponent>();
            });
        }
    }
}

