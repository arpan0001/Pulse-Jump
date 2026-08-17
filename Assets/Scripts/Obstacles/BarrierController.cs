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

        private void Awake()
        {
            FindReferences();
        }

        private void OnEnable()
        {
            
            _evaluated = false;

            FindReferences();
        }


        // Finds the player controller, game statistics, and particle system.
        private void FindReferences()
        {
            if (_pulseController == null)
            {
                _pulseController = FindFirstObjectByType<PulseController>();
            }


            if (_gameStatistics == null)
            {
                _gameStatistics = FindFirstObjectByType<GameStatistics>();
            }


            if (barrierParticles == null)
            {
                barrierParticles = GetComponentInChildren<ParticleSystem>();
            }
        }


        // Resets the barrier so it can be used again.
        public void ResetBarrier()
        {
            _evaluated = false;

            FindReferences();


            if (barrierParticles != null)
            {
                barrierParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }


            Debug.Log( "BARRIER RESET: " + gameObject.name);
        }


        // Checks when the player enters the barrier and starts the evaluation.
        private void OnTriggerEnter(Collider other)
        {
            if (_evaluated)
                return;

            if (!other.transform.root.CompareTag("Player")) return;


            Debug.Log("PLAYER ENTERED BARRIER: " + gameObject.name);


            EvaluatePlayer();
        }


        // Checks whether the player is pulsing and decides if they pass or fail.
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



        // Plays the particle effect when the player reaches the barrier.
        private void PlayBarrierParticles()
        {
            if (barrierParticles == null)
            {
                FindReferences();
            }


            if (barrierParticles == null)
            {
               
                return;
            }


            barrierParticles.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);

            barrierParticles.Play();

        }


        // Handles a successful barrier pass and increases the player's score.
        private void PassBarrier()
        { 
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


     
        // Handles the player's failure when they do not pass the barrier correctly.
        private void FailBarrier()
        {
           
            PlayerFailed?.Invoke();
        }
    }
}