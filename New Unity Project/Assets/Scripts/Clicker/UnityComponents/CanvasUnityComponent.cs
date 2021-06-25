using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clicker.UnityComponents
{
    public class CanvasUnityComponent : MonoBehaviour
    {
        public GameObject timerUI;
        public GameObject hpUI;
        public GameObject levelUI;
        public GameObject winScreen;
        public GameObject loseScreen;

        public Button loseRestartButton;
        public Button winRestartButton;
        public Button continueButton;

        public TMP_Text result;
        public TMP_Text leaderBoard;
    }
}