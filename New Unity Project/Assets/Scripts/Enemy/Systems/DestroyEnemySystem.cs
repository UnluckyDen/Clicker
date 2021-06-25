using System.Collections;
using System.Collections.Generic;
using Clicker.Components;
using Enemy.Components;
using Enemy.Data;
using Leopotam.Ecs;
using UI.Components;
using UnityEngine;

namespace Enemy.Systems
{
    public class DestroyEnemySystem : MonoBehaviour,IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<EnemyComponent> _enemyFilter = null;
        private readonly EcsFilter<EndOfAnimationEventComponent> _destroyEventFilter = null;
        private readonly EcsFilter<RestartEventComponent> _restartEventFilter = null;
        public void Run()
        {
            if (!_destroyEventFilter.IsEmpty() || !_restartEventFilter.IsEmpty())
            {
                foreach (var index in _enemyFilter)
                {
                    {
                        Destroy(_enemyFilter.Get1(index).EnemyGameObject);
                        _enemyFilter.GetEntity(index).Destroy();
                    }
                }
            }
        }
    }
}