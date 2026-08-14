using UnityEngine;

namespace PulseJump.Game
{
    public class GameStatistics : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private DifficultyManager difficultyManager;


        private float _elapsedTime;
        private float _distance;
        private int _score;


        private bool _isRunning;


        public float ElapsedTime =>
            _elapsedTime;


        public float Distance =>
            _distance;


        public int Score =>
            _score;


        public bool IsRunning =>
            _isRunning;


        private void Update()
        {
            if (!_isRunning)
                return;


            float deltaTime =
                Time.deltaTime;


            _elapsedTime +=
                deltaTime;


            UpdateDistance(deltaTime);
        }


        private void UpdateDistance(
            float deltaTime)
        {
            if (difficultyManager == null)
                return;


            float speed =
                difficultyManager.CurrentSpeed;


            _distance +=
                speed * deltaTime;
        }


        public void StartStatistics()
        {
            _isRunning = true;
        }


        public void StopStatistics()
        {
            _isRunning = false;
        }


        public void ResetStatistics()
        {
            _elapsedTime = 0f;
            _distance = 0f;
            _score = 0;
        }


        public void AddScore(int amount)
        {
            if (amount <= 0)
                return;


            _score += amount;
        }
    }
}