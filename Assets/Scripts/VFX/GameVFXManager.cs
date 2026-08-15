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


        private void OnEnable()
        {
            BarrierController.BarrierPassed +=
                OnBarrierPassed;

            BarrierController.PlayerFailed +=
                OnPlayerFailed;
        }


        private void OnDisable()
        {
            BarrierController.BarrierPassed -=
                OnBarrierPassed;

            BarrierController.PlayerFailed -=
                OnPlayerFailed;
        }


        private void OnBarrierPassed()
        {
            // Small satisfying shake.
            if (cameraShake != null)
            {
                cameraShake.Shake(
                    passShakeDuration,
                    passShakeStrength);
            }
        }


        private void OnPlayerFailed()
        {
            // Stronger shake for failure.
            if (cameraShake != null)
            {
                cameraShake.Shake(
                    failShakeDuration,
                    failShakeStrength);
            }
        }
    }
}