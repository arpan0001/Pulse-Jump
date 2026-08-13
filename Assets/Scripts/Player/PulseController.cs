using System.Collections;
using UnityEngine;

namespace PulseJump.Player
{
    public class PulseController : MonoBehaviour
    {
        [Header("Pulse Settings")]

        [SerializeField]
        private float pulseScale = 1.5f;

        [SerializeField]
        private float expandDuration = 0.12f;

        [SerializeField]
        private float holdDuration = 0.20f;

        [SerializeField]
        private float shrinkDuration = 0.18f;


        private Vector3 _normalScale;

        private Coroutine _pulseCoroutine;

        private bool _isPulsing;


        public bool IsPulsing => _isPulsing;


        private void Awake()
        {
            _normalScale = transform.localScale;
        }


        public void TryPulse()
        {
            if (_pulseCoroutine != null)
                return;

            _pulseCoroutine = StartCoroutine(PulseRoutine());
        }


        private IEnumerator PulseRoutine()
        {
            _isPulsing = true;


            yield return
                StartCoroutine( ScaleTo( _normalScale * pulseScale,expandDuration));


            yield return
                new WaitForSeconds(holdDuration);


            yield return
                StartCoroutine( ScaleTo(_normalScale,shrinkDuration));


            _isPulsing = false;

            _pulseCoroutine = null;
        }

        private void CheckPulse()
        {
            Debug.Log("Pulse Check Triggered!");
        }
        private IEnumerator ScaleTo(Vector3 targetScale,float duration)
        {
            Vector3 startScale =  transform.localScale;


            if (duration <= 0f)
            {
                transform.localScale =targetScale;

                yield break;
            }


            float elapsed = 0f;


            while (elapsed < duration)
            {
                elapsed +=  Time.deltaTime;


                float t =  elapsed / duration;


                transform.localScale =
                    Vector3.Lerp(  startScale, targetScale, t);


                yield return null;
            }


            transform.localScale =   targetScale;
        }
    }
}