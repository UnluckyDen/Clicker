using System.Collections;
using System.Collections.Generic;
using Enemy.Data;
using Enemy.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Enemy.Systems
{
    public class EnemySystem : IEcsInitSystem
    {
        private readonly EnemyData _enemyData = null;

        private readonly EcsWorld _world = null;

        public void Init()
        {
            var enemyPrefab = _enemyData.enemySettingsList[0].enemyPrefab;
            Object.Instantiate(enemyPrefab,new Vector3(-3.8f,3.85f,0f), Quaternion.identity);
            var entity = _world.NewEntity();
            entity.Replace(new EnemyComponent
                {
                    Hp = _enemyData.enemySettingsList[0].hp,
                    Level = _enemyData.enemySettingsList[0].level
                }
            );
        }
    }
}