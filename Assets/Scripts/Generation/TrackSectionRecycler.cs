using UnityEngine;


namespace PulseJump.Generation
{
    public class TrackSectionRecycler : MonoBehaviour
    {
        private TrackSectionGenerator _generator;


        public void Initialize(
            TrackSectionGenerator generator)
        {
            _generator =
                generator;
        }


        public void CheckRecycle(
            float recycleZ)
        {
            if (transform.position.z <= recycleZ)
            {
                _generator.RecycleSection(
                    gameObject);
            }
        }
    }
}