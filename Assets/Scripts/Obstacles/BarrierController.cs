using UnityEngine;
using PulseJump.Player;

namespace PulseJump.Obstacles
{
    public class BarrierController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            EvaluatePlayer(other);
        }


        private void EvaluatePlayer(Collider other)
        {
            PulseController pulse =
                other.GetComponentInParent<PulseController>();


            if (pulse == null)
            {
                Debug.LogError(
                    "PulseController is missing from player.",
                    other.gameObject);

                return;
            }


            if (pulse.IsPulsing)
            {
                Debug.Log(
                    "SUCCESS: Barrier passed while pulsing.");
            }
            else
            {
                Debug.Log(
                    "FAIL: Player was not pulsing.");
            }
        }
    }
}