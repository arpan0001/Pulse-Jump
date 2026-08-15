using UnityEngine;

namespace PulseJump.Generation
{
    public class TrackGenerator : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private TrackPool trackPool;


        [Header("Track Settings")]

        [SerializeField]
        private float trackLength = 10f;

        [SerializeField]
        private int startingSegments = 6;


        private float _nextZ;


        private void Start()
        {
            GenerateInitialTrack();
        }


        private void GenerateInitialTrack()
        {
            _nextZ = 0f;


            for (int i = 0; i < startingSegments; i++)
            {
                SpawnTrack();
            }
        }


        private void SpawnTrack()
        {
            if (trackPool == null)
            {
                Debug.LogError(
                    "TrackGenerator: TrackPool is missing.",
                    this);

                return;
            }


            TrackSegment segment =
                trackPool.Get();


            if (segment == null)
                return;


            segment.transform.localPosition =
                new Vector3(
                    0f,
                    0f,
                    _nextZ);


            segment.ResetSegment();


            TrackRecycler recycler =
                segment.GetComponent<TrackRecycler>();


            if (recycler != null)
            {
                recycler.Initialize(this);
            }


            _nextZ += trackLength;
        }


        public void RecycleTrack(
            TrackSegment segment)
        {
            if (segment == null)
                return;


            Debug.Log(
                "RECYCLING TRACK: " +
                segment.name);


            segment.transform.localPosition =
                new Vector3(
                    0f,
                    0f,
                    _nextZ);


            // Reset existing barrier.
            segment.ResetSegment();


            TrackRecycler recycler =
                segment.GetComponent<TrackRecycler>();


            if (recycler != null)
            {
                recycler.Initialize(this);
            }


            _nextZ += trackLength;
        }
    }
}