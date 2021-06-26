using System;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using Clicker.Components;
using Clicker.Data;
using Enemy.Systems;
using Enemy.Data;
using Clicker.Systems;
using UnityEngine;
using Clicker.UnityComponents;
using Client;
using Enemy.Components;
using Ui.Components;
using UI.Components;
using UI.Systems;
using UI.UnityButtonsScripts;

namespace Clicker
{ 
    public class GameStartup : MonoBehaviour 
    {
        [SerializeField] private EnemyData enemyData;
        [SerializeField] private TimerData timerData;
        [SerializeField] private LinkData linkData;
        
        [SerializeField] private CanvasUnityComponent canvasUnityComponents;
        
        private EcsWorld _world; 
        private EcsSystems _systems;
        
        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            _systems
                .Add(new CreateContinueEventSystem())
                .Add(new CreateRestartEventSystem())
                .Add(new DeathSystem())
                .Add(new ClickSystem())
                .Add(new AnimatorSystem())
                .Add(new DestroyEnemySystem())
                .Add(new LevelSystem())
                .Add(new CreateEnemySystem())
                .Add(new AddDamageSystem())
                .Add(new DisplayInfoUISystem())
                .Add(new LoadLeaderboardSystem())
                .Add(new TimerSystem())
                .Add(new ShowLoseSystem())
                .Add(new ShowWinSystem())
                .Add(new ShowRewardedAdsSystem())

                .OneFrame<ClickEventComponent>()
                .OneFrame<DeathEventComponent>()
                .OneFrame<EndOfAnimationEventComponent>()
                .OneFrame<LoseEventComponent>()
                .OneFrame<SuccessEventComponent>()
                .OneFrame<ContinueEventComponent>()
                .OneFrame<RewardedEventComponent>()
                .OneFrame<RestartEventComponent>()


                .Inject(enemyData)
                .Inject(canvasUnityComponents)
                .Inject(timerData)
                .Inject(linkData);
            _systems.Init();
        }
        private void Update() 
        {
            _systems?.Run();
        }
        
        private void OnDestroy()
        {
            if (_systems == null) return;
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}