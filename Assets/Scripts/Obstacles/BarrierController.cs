using UnityEngine;
using PulseJump.Game;
using PulseJump.Player;

namespace PulseJump.Obstacles
{
    public class BarrierController : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private Transform evaluationPoint;


        private PulseController _pulseController;

        private GameStatistics _gameStatistics;

        private bool _evaluated;

        // Sends the failure event to GameOverController.
        public static event System.Action PlayerFailed;


        private void Awake()
        {
            FindReferences();
        }


        private void OnEnable()
        {
            // Important for pooled/reused barriers.
            _evaluated = false;

            FindReferences();
        }


        private void FindReferences()
        {
            if (_pulseController == null)
            {
                _pulseController =
                    FindFirstObjectByType<PulseController>();
            }


            if (_gameStatistics == null)
            {
                _gameStatistics =
                    FindFirstObjectByType<GameStatistics>();
            }


            if (_pulseController == null)
            {
                Debug.LogError(
                    "BarrierController: PulseController not found.",
                    this);
            }


            if (_gameStatistics == null)
            {
                Debug.LogError(
                    "BarrierController: GameStatistics not found.",
                    this);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (_evaluated)
                return;


            if (!other.CompareTag("Player"))
                return;


            EvaluatePlayer();
        }


        private void EvaluatePlayer()
        {
            if (_evaluated)
                return;


            _evaluated = true;


            if (_pulseController == null)
            {
                FindReferences();
            }


            if (_pulseController == null)
            {
                Debug.LogError(
                    "Cannot evaluate barrier because PulseController is missing.",
                    this);

                return;
            }


            if (_pulseController.IsPulsing)
            {
                PassBarrier();
            }
            else
            {
                FailBarrier();
            }
        }


        private void PassBarrier()
        {
            Debug.Log(
                "Barrier passed successfully!");


            if (_gameStatistics == null)
            {
                _gameStatistics =
                    FindFirstObjectByType<GameStatistics>();
            }


            if (_gameStatistics != null)
            {
                _gameStatistics.AddScore(1);
            }
        }


        private void FailBarrier()
        {
            Debug.Log(
                "Barrier failed!");

            // Notify GameOverController.
            PlayerFailed?.Invoke();
        }
    }
}