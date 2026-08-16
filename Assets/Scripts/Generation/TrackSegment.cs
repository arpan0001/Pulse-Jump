using UnityEngine;
using PulseJump.Obstacles;
using PulseJump.Game;

namespace PulseJump.Generation
{
    public class TrackSegment : MonoBehaviour
    {
        [Header("Barrier")]

        [SerializeField]
        private GameObject barrier;

        [SerializeField]
        private FlyAwayOnTaggedHit[] resettableBarriers;


        private BarrierController[] barrierControllers;


        private void Awake()
        {
            CacheBarriers();
        }


        private void CacheBarriers()
        {
            if (barrier == null)
            {
                Debug.LogError(
                    "TrackSegment: Barrier is not assigned.",
                    this);

                return;
            }

            barrierControllers =
                barrier.GetComponentsInChildren<
                    BarrierController>(true);

            if (barrierControllers.Length == 0)
            {
                Debug.LogError(
                    "TrackSegment: No BarrierControllers found.",
                    barrier);
            }
        }


        public void ResetSegment()
        {
            if (barrier == null)
            {
                Debug.LogError(
                    "TrackSegment: Barrier is missing.",
                    this);

                return;
            }

            // Activate the complete barrier hierarchy first.
            barrier.SetActive(true);

            if (barrierControllers == null ||
                barrierControllers.Length == 0)
            {
                CacheBarriers();
            }

            // Reset every flying/destructible object.
            foreach (FlyAwayOnTaggedHit flyAwayBarrier
                in resettableBarriers)
            {
                if (flyAwayBarrier != null)
                {
                    flyAwayBarrier.ResetForTrackReuse();
                }
            }

            // Reset every BarrierController in this TrackSegment.
            foreach (BarrierController controller
                in barrierControllers)
            {
                if (controller != null)
                {
                    controller.ResetBarrier();
                }
            }

            Debug.Log(
                "TRACK RESET: " +
                gameObject.name);
        }
    }
}