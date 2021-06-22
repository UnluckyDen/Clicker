using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Data
{
    [CreateAssetMenu(fileName = "EnemyData",menuName = "Enemy Data",order = 0)]
    public class EnemyData : ScriptableObject
    {
        [System.Serializable]
        public struct EnemySettings
        {
            public string name;

            public GameObject enemyPrefab;
            
            public int level;
            public int hp;
        }

        public List<EnemySettings> enemySettingsList;
    }
}
