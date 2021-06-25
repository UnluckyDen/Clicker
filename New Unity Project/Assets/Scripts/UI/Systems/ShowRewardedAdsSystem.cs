using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UI.Components;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using Ui.Components;
using UnityEngine;

namespace UI.Systems
{
    public class ShowRewardedAdsSystem : IEcsRunSystem, IEcsInitSystem
    {
        private RewardedAd _rewardedAd;

        private readonly EcsWorld _world = null;
        
        private bool _rewarded;
        
        private readonly EcsFilter<RestartEventComponent> _restartEventFilter = null;

        public void Init()
        {
            _rewardedAd = new RewardedAd(adUnitId:"ca-app-pub-3940256099942544/5224354917");
            
            // Called when an ad request has successfully loaded.
            _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            // Called when an ad request failed to load.
            _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            // Called when an ad is shown.
            _rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            // Called when an ad request failed to show.
            _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            // Called when the user should be rewarded for interacting with the ad.
            _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            _rewardedAd.OnAdClosed += HandleRewardedAdClosed;
            
            AdRequest request = new AdRequest.Builder().Build();
            _rewardedAd.LoadAd(request);
        }
        

        public void Run()
        {
           
        }

        private bool ShowRewardingAds()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                if (_rewardedAd.IsLoaded()) {
                    _rewardedAd.Show();
                }
            }
            return _rewarded;
        }
        
        
        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdLoaded event received");
        }

        private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToLoad event received with message: "
                + args.LoadAdError);
        }
        public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToLoad event received with message: "
                + args.Message);
        }

        public void HandleRewardedAdOpening(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdOpening event received");
        }

        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToShow event received with message: "
                + args.Message);
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleRewardedAdClosed event received");
        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            // string type = args.Type;
            // double amount = args.Amount;
            // MonoBehaviour.print(
            //     "HandleRewardedAdRewarded event received for "
            //     + amount.ToString() + " " + type);
            _world.NewEntity().Get<RewardedEventComponent>();
        }
    }
}