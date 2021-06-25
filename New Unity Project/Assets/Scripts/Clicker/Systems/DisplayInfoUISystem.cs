using System;
using System.Collections;
using System.Collections.Generic;
using Clicker.UnityComponents;
using Enemy.Components;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace Clicker.Systems
{
    public class DisplayInfoUISystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly CanvasUnityComponent _canvasUnityComponent = null;

        private readonly EcsFilter<TimerComponent> _timerFilter;
        private readonly EcsFilter<EnemyComponent> _enemyFilter;

        private TextMeshProUGUI _showTime;
        private TextMeshProUGUI _showHp;
        private TextMeshProUGUI _showLevel;
        
        public void Init()
        { 
            _showTime = _canvasUnityComponent.timerUI.GetComponent<TextMeshProUGUI>();
            _showHp = _canvasUnityComponent.hpUI.GetComponent<TextMeshProUGUI>();
            _showLevel = _canvasUnityComponent.levelUI.GetComponent<TextMeshProUGUI>();
        }
        
        public void Run()
        {
            foreach (var idx in _timerFilter)
            {
                _showTime.text = Convert.ToString(_timerFilter.Get1(idx).CurrentTime);
                _showHp.text = "HP " + Convert.ToString(_enemyFilter.Get1(idx).Hp);
                _showLevel.text ="Lvl " + Convert.ToString(_enemyFilter.Get1(idx).Level);
            }
        }
    }
}