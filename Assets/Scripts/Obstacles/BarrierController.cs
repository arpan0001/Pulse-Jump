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

        private ParticleSystem _barrierParticles;

        private bool _evaluated;


        public static event System.Action BarrierPassed;

        public static event System.Action PlayerFailed;


        private void Awake()
        {
            _pulseController =
                FindFirstObjectByType<PulseController>();

            _gameStatistics =
                FindFirstObjectByType<GameStatistics>();

            _barrierParticles =
                GetComponentInChildren<ParticleSystem>(true);


            if (_barrierParticles == null)
            {
                Debug.LogError(
                    "BarrierController: ParticleSystem NOT FOUND!",
                    this);
            }
            else
            {
                Debug.Log(
                    "BarrierController: ParticleSystem found.",
                    this);
            }
        }


        private void OnEnable()
        {
            _evaluated = false;

            if (_barrierParticles != null)
            {
                _barrierParticles.Stop(
                    true,
                    ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(
                "Barrier trigger detected: " +
                other.name);


            if (_evaluated)
                return;


            if (!other.CompareTag("Player"))
                return;


            Debug.Log(
                "PLAYER TOUCHED BARRIER");


            EvaluatePlayer();
        }


        private void EvaluatePlayer()
        {
            if (_evaluated)
                return;


            _evaluated = true;


            // -----------------------------------------
            // PLAY PARTICLE
            // -----------------------------------------

            if (_barrierParticles != null)
            {
                Debug.Log(
                    "Playing barrier particles");


                _barrierParticles.Stop(
                    true,
                    ParticleSystemStopBehavior.StopEmittingAndClear);


                _barrierParticles.Play();
            }


            // -----------------------------------------
            // CHECK PULSE
            // -----------------------------------------

            if (_pulseController == null)
            {
                _pulseController =
                    FindFirstObjectByType<PulseController>();
            }


            if (_pulseController == null)
            {
                Debug.LogError(
                    "PulseController not found!",
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
                "BARRIER PASSED");


            BarrierPassed?.Invoke();


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
                "BARRIER FAILED");


            PlayerFailed?.Invoke();
        }
    }
}