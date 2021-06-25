using System;
using Clicker.Components;
using Enemy.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Enemy.Systems
{
    public class ClickSystem : IEcsRunSystem,IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private Camera _camera;

        private readonly EcsFilter<EnemyComponent> _enemyFilter = null;
        private readonly EcsFilter<SuccessEventComponent, LoseEventComponent> _displayScreensFilter = null;

        public void Init()
        {
            _camera = Camera.main;
        }
        
        public void Run()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics2D.Raycast(ray.origin,ray.direction))
                {
                    var clickGameObject = Physics2D.Raycast(ray.origin,ray.direction).collider.gameObject;
                    foreach (var idx in _enemyFilter)
                    {
                        if (_enemyFilter.Get1(idx).EnemyGameObject == clickGameObject && _displayScreensFilter.IsEmpty())
                        {
                            var entity = _world.NewEntity().Get<ClickEventComponent>();
                        }
                    }
                }
            }
        }
    }
}