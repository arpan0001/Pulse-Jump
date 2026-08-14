using UnityEngine;

namespace PulseJump.Generation
{
    public class TrackRecycler : MonoBehaviour
    {
        [SerializeField]
        private float recycleZ = -20f;

        private TrackGenerator _generator;
        private TrackSegment _segment;

        private bool _recycled;


        public void Initialize(TrackGenerator generator)
        {
            _generator = generator;
            _segment = GetComponent<TrackSegment>();

            _recycled = false;
        }


        private void Update()
        {
            // Safety check
            if (_generator == null)
                return;

            if (_segment == null)
                return;

            if (_recycled)
                return;


            if (transform.position.z <= recycleZ)
            {
                _recycled = true;

                _generator.RecycleTrack(_segment);
            }
        }
    }
}