using System;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using Enemy.Systems;
using Enemy;
using Enemy.Data;
using UnityEngine;

namespace Clicker
{ 
    public class GameStartup : MonoBehaviour 
    {
        [SerializeField] private EnemyData enemyData;
        
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
                .Add(new EnemySystem())
                .Add(new ClickSystem())
                .Add(new TimerSystem())

                .Inject(enemyData);
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