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


        // Generates the initial track when the scene starts.
        private void Start()
        {
            GenerateInitialTrack();
        }


        // Creates the starting number of track segments.
        private void GenerateInitialTrack()
        {
            _nextZ = 0f;

            for (int i = 0; i < startingSegments; i++)
            {
                SpawnTrack();
            }
        }


        // Gets a track segment from the pool and places it at the next position.
        private void SpawnTrack()
        {
            if (trackPool == null)
            {
                return;
            }

            TrackSegment segment = trackPool.Get();


            if (segment == null)
                return;

            segment.transform.localPosition = new Vector3( 0f,  0f, _nextZ);

            segment.ResetSegment();

            TrackRecycler recycler = segment.GetComponent<TrackRecycler>();


            if (recycler != null)
            {
                recycler.Initialize(this);
            }

            _nextZ += trackLength;
        }


        // Moves a recycled track segment to the next position and resets it.
        public void RecycleTrack(TrackSegment segment)
        {
            if (segment == null)
                return;

            segment.transform.localPosition =  new Vector3( 0f,  0f,  _nextZ);
        
            segment.ResetSegment();

            TrackRecycler recycler = segment.GetComponent<TrackRecycler>();


            if (recycler != null)
            {
                recycler.Initialize(this);
            }


            _nextZ += trackLength;
        }
    }
}