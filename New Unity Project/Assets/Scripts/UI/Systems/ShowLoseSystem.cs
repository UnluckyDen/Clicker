using System.Collections;
using System.Collections.Generic;
using Clicker.Components;
using Clicker.UnityComponents;
using Leopotam.Ecs;
using Ui.Components;
using UI.Components;
using UnityEngine;

namespace UI.Systems
{
    public class ShowLoseSystem : IEcsRunSystem,IEcsInitSystem
    {
        private readonly CanvasUnityComponent _canvasUnityComponent = null;

        private readonly EcsFilter<LoseEventComponent> _loseEventFilter = null;
        private readonly EcsFilter<RestartEventComponent> _restartEventComponent = null;
        private readonly EcsFilter<RewardedEventComponent> _rewardedEventComponent = null;

        private GameObject _loseScreen;
        public void Init()
        {
            _loseScreen = _canvasUnityComponent.loseScreen;
        }
        
        public void Run()
        {
            if (!_loseEventFilter.IsEmpty())
            {
                _loseScreen.SetActive(true);
            }

            if (!_restartEventComponent.IsEmpty() || !_rewardedEventComponent.IsEmpty())
            {
                _loseScreen.SetActive(false);
            }
        }
    }
}