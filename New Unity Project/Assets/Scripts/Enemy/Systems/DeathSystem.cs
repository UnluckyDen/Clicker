using System.Collections;
using System.Collections.Generic;
using Enemy.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Enemy.Systems
{
    public class DeathSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<EnemyComponent> _enemyFilter = null;

        public void Run()
        {
            foreach (var idx in _enemyFilter)
            {
                if (!_enemyFilter.IsEmpty() && _enemyFilter.Get1(idx).Hp <= 0)
                {
                    _world.NewEntity().Get<DeathEventComponent>();
                    _enemyFilter.Get1(idx).EnemyGameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                }
            }
        }
    }
}