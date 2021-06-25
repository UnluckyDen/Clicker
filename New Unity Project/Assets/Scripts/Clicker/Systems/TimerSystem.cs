using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using Clicker.Components;
using Clicker.Data;
using Ui.Components;
using UI.Components;
using UnityEngine;

namespace Clicker.Systems
{
    public class TimerSystem : IEcsRunSystem,IEcsInitSystem
    {
        private readonly TimerData _timerData = null;
        
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<TimerComponent> _timeFilter = null;
        private readonly EcsFilter<RestartEventComponent> _restartEventFilter = null;
        private readonly EcsFilter<SuccessEventComponent> _successEvenFilter = null;
        private readonly EcsFilter<RewardedEventComponent> _rewardedEventFilter = null;

        private float _currentTime;
        private float _timeForCurrentStage;

        private bool _timerIsPlaying = true;
        
        public void Init()
        {
            StartTimer();
        }
        
        public void Run()
        {
            TimerRun();
            TimeChecker();
            if (!_restartEventFilter.IsEmpty())
            {
                RestartTimer();
            }

            if (!_successEvenFilter.IsEmpty())
            {
                foreach (var idx in _timeFilter)
                {
                    _timeFilter.Get1(idx).EndStageFor = _timeForCurrentStage - _currentTime;
                }
                if ( _timeForCurrentStage - _currentTime < PlayerPrefs.GetFloat("bestTime"))
                {
                    PlayerPrefs.SetFloat("bestTime",_timeForCurrentStage - _currentTime);
                }
                
                StopTimer();
            }

            if (!_rewardedEventFilter.IsEmpty())
            {
                AddTimeToTimer();
            }
        }

        private void RestartTimer()
        {
            _timerIsPlaying = true;
            _currentTime = _timerData.timeOnLevel;
            _timeForCurrentStage = _timerData.timeOnLevel;
        }

        private void AddTimeToTimer()
        {
            _timerIsPlaying = true;
            _currentTime += _timerData.timeForAdvertising;
            _timeForCurrentStage += _timerData.timeForAdvertising;
        }

        private void StartTimer()
        {
            _timeForCurrentStage = _timerData.timeOnLevel;
            _currentTime = _timerData.timeOnLevel;
            var entity = _world.NewEntity();
            entity.Replace(new TimerComponent()
                {
                    CurrentTime = _currentTime
                }
            );
        }

        private void TimerRun()
        {
            if (_timerIsPlaying)
            {
                _currentTime -= Time.deltaTime;
                foreach (var idx in _timeFilter)
                {
                    _timeFilter.Get1(idx).CurrentTime = _currentTime;
                }
            }
        }

        private void TimeChecker()
        {
            foreach (var idx in _timeFilter)
            {
                if (_timeFilter.Get1(idx).CurrentTime <= 0)
                {
                    _currentTime = 0;
                    StopTimer();
                    _world.NewEntity().Get<LoseEventComponent>();
                }
            }
        }

        private void StopTimer()
        {
            foreach (var idx in _timeFilter)
            {
                _timeFilter.Get1(idx).CurrentTime = _currentTime;
            }   
            _timerIsPlaying = false;
        }
    }
}

