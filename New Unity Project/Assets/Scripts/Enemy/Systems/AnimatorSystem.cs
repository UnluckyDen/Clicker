using System.Collections;
using System.Collections.Generic;
using Enemy.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Enemy.Systems
{
    public class AnimatorSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<EnemyComponent> _enemyFilter = null;
        private readonly EcsFilter<ClickEventComponent> _clickEventFilter = null;
        private readonly EcsFilter<DeathEventComponent> _deathEventFilter = null;
        
        public void Run()
        {
            if (!_clickEventFilter.IsEmpty())
            {
                foreach (var idx in _enemyFilter)
                {
                    _enemyFilter.Get1(idx).EnemyGameObject.GetComponent<Animator>().SetTrigger("Click");
                }
            }

            if (!_deathEventFilter.IsEmpty())
            {
                foreach (var idx in _enemyFilter)
                {
                    _enemyFilter.Get1(idx).EnemyGameObject.GetComponent<Animator>().SetBool("Death",true);
                    if (_enemyFilter.Get1(idx).EnemyGameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0)
                        .IsName("End"))
                    {
                        _world.NewEntity().Get<EndOfAnimationEventComponent>();
                    }
                }
            }
        }
    }
}
