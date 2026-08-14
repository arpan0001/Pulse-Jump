using UnityEngine;
using PulseJump.Level;

namespace PulseJump.Game
{
    public class DifficultyManager : MonoBehaviour
    {
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


        private float _elapsedTime;
        private float _currentSpeed;


        public float CurrentSpeed => _currentSpeed;


        private void Start()
        {
            _currentSpeed = startingSpeed;

            worldMovement.SetMoveSpeed(
                _currentSpeed);
        }


        private void Update()
        {
            _elapsedTime +=
                Time.deltaTime;


            UpdateDifficulty(
                _elapsedTime);
        }


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


            worldMovement.SetMoveSpeed(
                _currentSpeed);
        }
    }
}