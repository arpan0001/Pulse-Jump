using TMPro;
using UnityEngine;

namespace PulseJump.UI
{
    public class StatisticsUI : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private PulseJump.Game.GameStatistics statistics;


        [SerializeField]
        private TMP_Text scoreText;


        [SerializeField]
        private TMP_Text distanceText;


        [SerializeField]
        private TMP_Text timerText;


        private void Update()
        {
            if (statistics == null)
                return;


            UpdateScore();
            UpdateDistance();
            UpdateTimer();
        }


        private void UpdateScore()
        {
            scoreText.text =
                statistics.Score.ToString();
        }


        private void UpdateDistance()
        {
            float distance =
                statistics.Distance;


            distanceText.text =
                Mathf.FloorToInt(distance) +
                " m";
        }


        private void UpdateTimer()
        {
            float time =
                statistics.ElapsedTime;


            int minutes =
                Mathf.FloorToInt(
                    time / 60f);


            int seconds =
                Mathf.FloorToInt(
                    time % 60f);


            timerText.text =
                string.Format(
                    "{0:00}:{1:00}",
                    minutes,
                    seconds);
        }
    }
}