using UnityEngine;
using PulseJump.Obstacles;
using PulseJump.CameraSystem;

namespace PulseJump.VFX
{
    public class GameVFXManager : MonoBehaviour
    {
        [Header("Camera Shake")]

        [SerializeField]
        private CameraShake cameraShake;


        [Header("Pass Settings")]

        [SerializeField]
        private float passShakeDuration = 0.08f;

        [SerializeField]
        private float passShakeStrength = 0.015f;


        [Header("Fail Settings")]

        [SerializeField]
        private float failShakeDuration = 0.25f;

        [SerializeField]
        private float failShakeStrength = 0.08f;


        // Starts listening for barrier pass and player failure events.
        private void OnEnable()
        {
            BarrierController.BarrierPassed += OnBarrierPassed;

            BarrierController.PlayerFailed += OnPlayerFailed;
        }


        // Stops listening for barrier pass and player failure events.
        private void OnDisable()
        {
            BarrierController.BarrierPassed -= OnBarrierPassed;

            BarrierController.PlayerFailed -= OnPlayerFailed;
        }


        // Plays a small camera shake when the player passes a barrier.
        private void OnBarrierPassed()
        {
            
            if (cameraShake != null)
            {
                cameraShake.Shake(passShakeDuration, passShakeStrength);
            }
        }


        // Plays a stronger camera shake when the player fails.
        private void OnPlayerFailed()
        {
            
            if (cameraShake != null)
            {
                cameraShake.Shake(failShakeDuration, failShakeStrength);
            }
        }
    }
}