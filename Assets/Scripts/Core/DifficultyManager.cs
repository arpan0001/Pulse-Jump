using UnityEngine;
using PulseJump.Level;

namespace PulseJump.Game
{
    public class DifficultyManager : MonoBehaviour
    {
        public enum DifficultyLevel
        {
            Easy,
            Medium,
            Hard,
            VeryHard
        }


        public DifficultyLevel CurrentLevel
        {
            get;
            private set;
        }


        [Header("References")]

        [SerializeField]
        private WorldMovement worldMovement;


        [Header("Speed")]

        [SerializeField]
        private float startingSpeed = 5f;

        [SerializeField]
        private float maximumSpeed = 10f;

        [SerializeField]
        private float speedIncreasePerSecond = 0.05f;


        [Header("Obstacle Chance")]

        [SerializeField]
        [Range(0f, 1f)]
        private float easyObstacleChance = 0.35f;

        [SerializeField]
        [Range(0f, 1f)]
        private float mediumObstacleChance = 0.50f;

        [SerializeField]
        [Range(0f, 1f)]
        private float hardObstacleChance = 0.65f;

        [SerializeField]
        [Range(0f, 1f)]
        private float veryHardObstacleChance = 0.80f;


        [Header("Minimum Safe Segments")]

        [SerializeField]
        private int easySafeSegments = 3;

        [SerializeField]
        private int mediumSafeSegments = 2;

        [SerializeField]
        private int hardSafeSegments = 1;

        [SerializeField]
        private int veryHardSafeSegments = 1;


        private float _elapsedTime;
        private float _currentSpeed;
        private bool _isRunning;



        public float CurrentSpeed => _currentSpeed;

        public float ElapsedTime => _elapsedTime;


        private void Start()
        {
            _currentSpeed =
                startingSpeed;

            _elapsedTime = 0f;

            _isRunning = false;

            worldMovement.SetMoveSpeed(
                _currentSpeed);
        }

        public void StartDifficulty()
        {
            _isRunning = true;
        }


        public void StopDifficulty()
        {
            _isRunning = false;
        }
        private void Update()
        {
            if (!_isRunning)
                return;


            _elapsedTime +=
                Time.deltaTime;


            UpdateDifficulty(
                _elapsedTime);
        }

        // --------------------------------------------------
        // DIFFICULTY LEVEL
        // --------------------------------------------------

        private void UpdateDifficultyLevel(
            float elapsedTime)
        {
            if (elapsedTime < 20f)
            {
                CurrentLevel =
                    DifficultyLevel.Easy;
            }
            else if (elapsedTime < 40f)
            {
                CurrentLevel =
                    DifficultyLevel.Medium;
            }
            else if (elapsedTime < 60f)
            {
                CurrentLevel =
                    DifficultyLevel.Hard;
            }
            else
            {
                CurrentLevel =
                    DifficultyLevel.VeryHard;
            }
        }


        // --------------------------------------------------
        // WORLD SPEED
        // --------------------------------------------------

        private void UpdateDifficulty(
            float elapsedTime)
        {
            float targetSpeed =
                startingSpeed +
                elapsedTime *
                speedIncreasePerSecond;


            _currentSpeed =
                Mathf.Min(
                    targetSpeed,
                    maximumSpeed);


            if (worldMovement != null)
            {
                worldMovement.SetMoveSpeed(
                    _currentSpeed);
            }
        }


        // --------------------------------------------------
        // OBSTACLE CHANCE
        // --------------------------------------------------

        public float GetObstacleChance()
        {
            switch (CurrentLevel)
            {
                case DifficultyLevel.Easy:
                    return easyObstacleChance;


                case DifficultyLevel.Medium:
                    return mediumObstacleChance;


                case DifficultyLevel.Hard:
                    return hardObstacleChance;


                case DifficultyLevel.VeryHard:
                    return veryHardObstacleChance;


                default:
                    return easyObstacleChance;
            }
        }


        // --------------------------------------------------
        // MINIMUM SAFE DISTANCE
        // --------------------------------------------------

        public int GetMinimumSafeSegments()
        {
            switch (CurrentLevel)
            {
                case DifficultyLevel.Easy:
                    return easySafeSegments;


                case DifficultyLevel.Medium:
                    return mediumSafeSegments;


                case DifficultyLevel.Hard:
                    return hardSafeSegments;


                case DifficultyLevel.VeryHard:
                    return veryHardSafeSegments;


                default:
                    return easySafeSegments;
            }
        }
    }
}