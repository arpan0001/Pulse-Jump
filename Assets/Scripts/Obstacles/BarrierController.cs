using System;
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

        [SerializeField]
        private ParticleSystem barrierParticles;


        private PulseController _pulseController;
        private GameStatistics _gameStatistics;

        private bool _evaluated;

        public static event Action BarrierPassed;
        public static event Action PlayerFailed;


        // --------------------------------------------------
        // UNITY
        // --------------------------------------------------

        private void Awake()
        {
            FindReferences();
        }


        private void OnEnable()
        {
            // Reset only when this barrier is enabled.
            _evaluated = false;

            FindReferences();
        }


        // --------------------------------------------------
        // REFERENCES
        // --------------------------------------------------

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


            if (barrierParticles == null)
            {
                barrierParticles =
                    GetComponentInChildren<ParticleSystem>();
            }
        }


        // --------------------------------------------------
        // RESET
        // --------------------------------------------------

        public void ResetBarrier()
        {
            _evaluated = false;

            FindReferences();


            if (barrierParticles != null)
            {
                barrierParticles.Stop(
                    true,
                    ParticleSystemStopBehavior.StopEmittingAndClear);
            }


            Debug.Log(
                "BARRIER RESET: " +
                gameObject.name);
        }


        // --------------------------------------------------
        // TRIGGER
        // --------------------------------------------------

        private void OnTriggerEnter(Collider other)
        {
            if (_evaluated)
                return;


            // Use root so child objects such as
            // Shield FX do not cause problems.
            if (!other.transform.root.CompareTag("Player"))
                return;


            Debug.Log(
                "PLAYER ENTERED BARRIER: " +
                gameObject.name);


            EvaluatePlayer();
        }


        // --------------------------------------------------
        // EVALUATE
        // --------------------------------------------------

        private void EvaluatePlayer()
        {
            if (_evaluated)
                return;


            // IMPORTANT:
            // Lock this barrier immediately.
            _evaluated = true;


            if (_pulseController == null)
            {
                FindReferences();
            }


            if (_pulseController == null)
            {
                Debug.LogError(
                    "BarrierController: PulseController not found.",
                    this);

                return;
            }


            PlayBarrierParticles();


            if (_pulseController.IsPulsing)
            {
                PassBarrier();
            }
            else
            {
                FailBarrier();
            }
        }


        // --------------------------------------------------
        // PARTICLES
        // --------------------------------------------------

        private void PlayBarrierParticles()
        {
            if (barrierParticles == null)
            {
                FindReferences();
            }


            if (barrierParticles == null)
            {
                Debug.LogWarning(
                    "BarrierController: ParticleSystem not found.",
                    this);

                return;
            }


            barrierParticles.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear);

            barrierParticles.Play();


            Debug.Log(
                "Playing barrier particles");
        }


        // --------------------------------------------------
        // PASS
        // --------------------------------------------------

        private void PassBarrier()
        {
            Debug.Log("BARRIER PASSED");


            // Notify AudioManager and GameVFXManager
            BarrierPassed?.Invoke();


            if (_gameStatistics == null)
            {
                FindReferences();
            }


            if (_gameStatistics != null)
            {
                _gameStatistics.AddScore(1);
            }
        }


        // --------------------------------------------------
        // FAIL
        // --------------------------------------------------

        private void FailBarrier()
        {
            Debug.Log(
                "BARRIER FAILED");


            PlayerFailed?.Invoke();
        }
    }
}