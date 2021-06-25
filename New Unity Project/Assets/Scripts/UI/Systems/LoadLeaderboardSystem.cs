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
            if (LoadLeaderboardJson(_linkData.Link) != null)
            {
                Debug.Log(LoadLeaderboardJson(_linkData.Link)[1].Name);
            }
            else
            {
                Debug.Log("Internet trables");
            }
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
                return null;
            }
        }
    }
}

