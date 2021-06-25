using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker.Data
{
    [CreateAssetMenu(fileName = "TimerData",menuName = "Timer Data",order = 1)]
    public class TimerData : ScriptableObject
    {
        public float timeOnLevel;
        public float timeForAdvertising;
    }
}