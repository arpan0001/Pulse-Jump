using UnityEngine;

namespace PulseJump.Generation
{
    public class TrackSegment : MonoBehaviour
    {
        [Header("Barrier Spawn")]
        [SerializeField]
        private Transform barrierSpawnPoint;


        private GameObject _activeBarrier;


        public Transform BarrierSpawnPoint =>
            barrierSpawnPoint;


        public GameObject ActiveBarrier =>
            _activeBarrier;


        public void SetBarrier(
            GameObject barrier)
        {
            ClearBarrier();


            if (barrier == null)
                return;


            _activeBarrier = barrier;


            Transform parent =
                barrier.transform;


            parent.SetParent(
                barrierSpawnPoint);


            parent.localPosition =
                Vector3.zero;


            parent.localRotation =
                Quaternion.identity;


            parent.localScale =
                Vector3.one;


            barrier.SetActive(true);
        }


        public void ClearBarrier()
        {
            if (_activeBarrier == null)
                return;


            _activeBarrier.SetActive(false);


            _activeBarrier.transform.SetParent(
                null);


            _activeBarrier = null;
        }
    }
}