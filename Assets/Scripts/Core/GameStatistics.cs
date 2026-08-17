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


        // Returns the total time the game has been running.
        public float ElapsedTime => _elapsedTime;

        public float Distance => _distance;

        public int Score =>  _score;
        public bool IsRunning =>  _isRunning;


        // Updates the game time and distance every frame while statistics are running.
        private void Update()
        {
            if (!_isRunning)
                return;

            float deltaTime = Time.deltaTime;

            _elapsedTime +=  deltaTime;

            UpdateDistance(deltaTime);
        }


        // Calculates and updates the distance using the current game speed.
        private void UpdateDistance( float deltaTime)
        {
            if (difficultyManager == null)
                return;

            float speed = difficultyManager.CurrentSpeed;

            _distance += speed * deltaTime;
        }

        public void StartStatistics()
        {
            _isRunning = true;
        }

        // Resets the statistics and keeps the system stopped when the scene starts.
        private void Start()
        {
            ResetStatistics();

            StopStatistics();
        }

        public void StopStatistics()
        {
            _isRunning = false;
        }


        // Resets the time, distance, and score back to zero.
        public void ResetStatistics()
        {
            _elapsedTime = 0f;
            _distance = 0f;
            _score = 0;
        }


        // Adds the given amount to the player's score.
        public void AddScore(int amount)
        {
            if (amount <= 0)
                return;


            _score += amount;
        }
    }
}