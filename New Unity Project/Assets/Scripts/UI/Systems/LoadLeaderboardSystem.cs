using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Leopotam.Ecs;
using Newtonsoft.Json;
using UI.Components;
using UnityEngine;

namespace UI.Systems
{
    public class LoadLeaderboardSystem: IEcsInitSystem
    {
        private readonly EcsWorld _world = null;

        private readonly LinkData _linkData = null;
        public void Init()
        {
            var entity = _world.NewEntity();
            entity.Replace(new LeaderboardComponent
            {
                LeaderboardData = LoadLeaderboardJson(_linkData.Link)
            });
        }

        public List<LeaderboardData> LoadLeaderboardJson(string link)
        {
            try
            {
                Uri uri = new Uri(link);
                string json = new WebClient().DownloadString(uri);
                List<LeaderboardData> playerData = JsonConvert.DeserializeObject<List<LeaderboardData>>(json);
                return playerData;
            }
            catch (WebException e)
            {
                LeaderboardData lostConnection = new LeaderboardData();
                List<LeaderboardData> playerData = new List<LeaderboardData>();
                lostConnection.Name = "Connection lost";
                lostConnection.Score = 0;
                playerData.Add(lostConnection);
                return playerData;
            }
        }
    }
}

