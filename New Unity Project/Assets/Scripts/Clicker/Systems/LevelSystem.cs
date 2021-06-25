using Clicker.Components;
using Enemy.Components;
using Enemy.Data;
using Leopotam.Ecs;
using UI.Components;
using UnityEngine;

namespace Clicker.Systems 
{
	sealed class LevelSystem : IEcsRunSystem, IEcsInitSystem
	{
		private readonly EcsWorld _world = null;
		private LevelComponent _levelComponent;

		private readonly EcsFilter<EndOfAnimationEventComponent> _endOfAnimationFilter = null;
		private readonly EcsFilter<LevelComponent> _levelFilter = null;
		private readonly EcsFilter<RestartEventComponent> _restartEventFilter = null;
		
		private readonly EnemyData _enemyData = null;

		public void Init()
		{
			_world.NewEntity().Get<LevelComponent>();
		}

		void IEcsRunSystem.Run()
		{
			if (!_endOfAnimationFilter.IsEmpty())
			{
				UpdateLevel();
			}

			if (!_restartEventFilter.IsEmpty())
			{
				RestartLevel();
			}
		}

		private void UpdateLevel()
		{
			foreach (var index in _levelFilter)
			{
				if (_levelFilter.Get1(index).CurrentLevel + 1 < _enemyData.enemySettingsList.Count)
				{
					_levelFilter.Get1(index).CurrentLevel++;
				}
				else
				{
					_world.NewEntity().Get<SuccessEventComponent>();
				}
			}
		}

		private void RestartLevel()
		{
			foreach (var index in _levelFilter)
			{ 
				_levelFilter.Get1(index).CurrentLevel = 0;
			}
		}
	}
}