using System.Collections.Generic;
using UnityEngine;

namespace PulseJump.Generation
{
    public class TrackPool : MonoBehaviour
    {
        [Header("Pool")]

        [SerializeField]
        private TrackSegment trackPrefab;

        [SerializeField]
        private int poolSize = 6;


        [Header("Parent")]

        [SerializeField]
        private Transform world;


        private Queue<TrackSegment> _pool = new Queue<TrackSegment>();

        private void Awake()
        {
            CreatePool();
        }


        // Creates and stores the initial track segments in the pool.
        private void CreatePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                TrackSegment segment = Instantiate( trackPrefab, world);

                segment.gameObject.SetActive(false);

                _pool.Enqueue(segment);
            }
        }


        // Gets an available track segment from the pool.
        public TrackSegment Get()
        {
            if (_pool.Count == 0)
            {
                return null;
            }

            TrackSegment segment = _pool.Dequeue();

            segment.gameObject.SetActive(true);

            return segment;
        }


        // Returns a track segment to the pool for future reuse.
        public void Return( TrackSegment segment)
        {
            if (segment == null)
                return;

            segment.gameObject.SetActive(false);

            _pool.Enqueue(segment);
        }
    }
}