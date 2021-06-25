using Enemy.Components;
using Leopotam.Ecs;

namespace Enemy.Systems {
    sealed class AddDamageSystem : IEcsRunSystem 
    {
        readonly EcsWorld _world = null;
        
        private readonly EcsFilter<EnemyComponent> _enemyFilter = null;
        private readonly EcsFilter<ClickEventComponent> _clickEventFilter = null;
        
        void IEcsRunSystem.Run() 
        {
            if (!_clickEventFilter.IsEmpty())
            {
                foreach (var idx in _enemyFilter)
                {
                    _enemyFilter.Get1(idx).Hp -= 10;
                }
            }
        }
    }
}