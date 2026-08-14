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
            TrackSegment segment =
                trackPool.Get();


            // Position relative to World.
            segment.transform.localPosition =
                new Vector3(
                    0f,
                    0f,
                    _nextZ);


            // Give the recycler a reference
            // to this generator.
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
            // Move this segment to the
            // end of the track.
            segment.transform.localPosition =
                new Vector3(
                    0f,
                    0f,
                    _nextZ);


            // Re-initialize recycler.
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