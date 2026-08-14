using UnityEngine;
using PulseJump.Player;
using PulseJump.Game;

namespace PulseJump.Obstacles
{
    public class BarrierController : MonoBehaviour
    {

        [SerializeField]
        private GameStatistics gameStatistics;

        private void OnTriggerEnter(Collider other)
        {
            EvaluatePlayer(other);
        }

        private void PassBarrier()
        {
            Debug.Log("Barrier passed successfully!");


            if (gameStatistics != null)
            {
                gameStatistics.AddScore(1);
            }
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