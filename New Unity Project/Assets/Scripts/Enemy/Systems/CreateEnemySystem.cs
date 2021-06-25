using System.Collections;
using System.Collections.Generic;
using Clicker.Components;
using Enemy.Data;
using Enemy.Components;
using Leopotam.Ecs;
using UI.Components;
using UnityEngine;

namespace Enemy.Systems
{
    public class CreateEnemySystem : IEcsInitSystem,IEcsRunSystem
    {
        private readonly EnemyData _enemyData = null;

        private readonly EcsWorld _world = null;

        private readonly EcsFilter<LevelComponent> _levelFilter = null;
        private readonly EcsFilter<SuccessEventComponent> _successEventFilter;
        private readonly EcsFilter<EndOfAnimationEventComponent> _endOfAnimationFilter = null;
        private readonly EcsFilter<RestartEventComponent> _restartEventFilter = null;
        
        private int _enemyLevel;

        public void Init()
        {
            CreateEnemy();
        }
        
        public void Run()
        {
            if ((!_endOfAnimationFilter.IsEmpty() || !_restartEventFilter.IsEmpty()) && _successEventFilter.IsEmpty())
            {
                CreateEnemy();
            }
        }

        private void CreateEnemy()
        {
            foreach (var index in _levelFilter)
            { 
                _enemyLevel =  _levelFilter.Get1(index).CurrentLevel;
            }
            
            var enemyPrefab = _enemyData.enemySettingsList[_enemyLevel].enemyPrefab;
            var enemyGameObject = Object.Instantiate(enemyPrefab,new Vector3(-3.8f,3.85f,0f), Quaternion.identity);
            var entity = _world.NewEntity();
            entity.Replace(new EnemyComponent
                {
                    Hp = _enemyData.enemySettingsList[_enemyLevel].hp,
                    Level = _enemyData.enemySettingsList[_enemyLevel].level,
                    EnemyGameObject = enemyGameObject
                }
            );
        }
    }
}