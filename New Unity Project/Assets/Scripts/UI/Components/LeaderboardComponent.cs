using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Components
{
    public struct LeaderboardComponent
    {
        public List<LeaderboardData> LeaderboardData;
    }

    public class LeaderboardData
    {
        private string _name;
        private int _score;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Score
        {
            get => _score;
            set => _score = value;
        }
    }
}