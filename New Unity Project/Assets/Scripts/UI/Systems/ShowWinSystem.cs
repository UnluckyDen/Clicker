using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Clicker;
using Clicker.Components;
using Clicker.UnityComponents;
using Leopotam.Ecs;
using TMPro;
using Ui.Components;
using UI.Components;
using UnityEngine;

namespace UI.Systems
{
    public class ShowWinSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly CanvasUnityComponent _canvasUnityComponent = null;

        private readonly EcsFilter<SuccessEventComponent> _successEventFilter = null;
        private readonly EcsFilter<RestartEventComponent> _restartEventComponent = null;
        private readonly EcsFilter<TimerComponent> _timeFilter = null;
        private readonly EcsFilter<LeaderboardComponent> _leaderBoardFilter = null;

        private GameObject _winScreen;
        private TMP_Text _result;
        private TMP_Text _leaderBoard;

        private List<LeaderboardData> _leaderboardDatas = new List<LeaderboardData>();

        private string _leaderboardString;
        
        private float _currentEndTime;

        public void Init()
        {
            _winScreen = _canvasUnityComponent.winScreen;
            _result = _canvasUnityComponent.result;
            _leaderBoard = _canvasUnityComponent.leaderBoard;
        }

        public void Run()
        {
            if (!_successEventFilter.IsEmpty())
            {
                foreach (var index in _timeFilter)
                {
                    _currentEndTime = _timeFilter.Get1(index).EndStageFor;
                }
                _winScreen.SetActive(true);
                _result.text = _currentEndTime.ToString();
                GenerateLeaderboard();
                LeaderBoardToString(SortLeaderboard(_leaderboardDatas));
                
                _leaderBoard.text = _leaderboardString;

            }

            if (!_restartEventComponent.IsEmpty())
            {
                _winScreen.SetActive(false);
            }
        }

        private void GenerateLeaderboard()
        {
            foreach (var index in _leaderBoardFilter)
            {
                _leaderboardDatas = _leaderBoardFilter.Get1(index).LeaderboardData;
            }
            
            LeaderboardData playerBestResult = new LeaderboardData();
            playerBestResult.Name = "Your result";
            playerBestResult.Score = Convert.ToInt32(PlayerPrefs.GetFloat("bestTime"));
            if (!_leaderboardDatas.Contains(playerBestResult))
            {
                _leaderboardDatas.Add(playerBestResult);
            }
        }

        private List<LeaderboardData> SortLeaderboard(List<LeaderboardData> leaderboardData)
        {
            List<LeaderboardData> sortedList = leaderboardData.OrderBy(x => x.Score).ToList();
            return sortedList;
        }

        private string LeaderBoardToString(List<LeaderboardData> leaderboardData)
        {
            foreach (var pair in leaderboardData)
            {
                _leaderboardString += pair.Name + " - " + pair.Score + "\n";
            }
            return _leaderboardString.Trim();
        }
    }
}