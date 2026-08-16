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


        private BarrierController _barrierController;

        [SerializeField]
        private FlyAwayOnTaggedHit[] resettableBarriers;
        private void Awake()
        {
            CacheBarrier();
        }


        private void CacheBarrier()
        {
            if (barrier == null)
            {
                Debug.LogError(
                    "TrackSegment: Barrier is not assigned.",
                    this);

                return;
            }


            _barrierController =
                barrier.GetComponentInChildren<BarrierController>();


            if (_barrierController == null)
            {
                Debug.LogError(
                    "TrackSegment: BarrierController not found.",
                    barrier);

                return;
            }
        }


        // --------------------------------------------------
        // RESET TRACK
        // --------------------------------------------------

        public void ResetSegment()
        {
            if (barrier == null)
            {
                Debug.LogError(
                    "TrackSegment: Barrier is missing.",
                    this);

                return;
            }
            foreach (FlyAwayOnTaggedHit barrier in resettableBarriers)
            {
                barrier.ResetForTrackReuse();
            }

            if (_barrierController == null)
            {
                CacheBarrier();
            }


            // Reset barrier evaluation.
            if (_barrierController != null)
            {
                _barrierController.ResetBarrier();
            }


            // Make sure barrier is active.
            barrier.SetActive(true);


            Debug.Log(
                "TRACK RESET: " +
                gameObject.name);
        }
    }
}