using UnityEngine;
using PulseJump.Player;
using PulseJump.Core;

namespace PulseJump.Obstacles
{
    public class BarrierController : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private PulseController pulseController;


        private bool _hasBeenChecked;
        private bool _hasPassed;


        public void ResetBarrier()
        {
            _hasBeenChecked = false;
            _hasPassed = false;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (_hasBeenChecked)
                return;


            if (!other.CompareTag("Player"))
                return;


            _hasBeenChecked = true;


            EvaluatePlayer();
        }


        private void EvaluatePlayer()
        {
            if (pulseController == null)
            {
                Debug.LogError(
                    "PulseController is missing.",
                    this);

                return;
            }


            if (pulseController.IsPulsing)
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
            if (_hasPassed)
                return;


            _hasPassed = true;


            Debug.Log(
                "Barrier passed!");
        }


        private void FailBarrier()
        {
            Debug.Log(
                "Barrier failed!");


            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}