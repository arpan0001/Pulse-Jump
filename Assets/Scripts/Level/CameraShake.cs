using System.Collections;
using UnityEngine;

namespace PulseJump.CameraSystem
{
    public class CameraShake : MonoBehaviour
    {
        private Vector3 _originalPosition;
        private Coroutine _shakeRoutine;

        private void Awake()
        {
            _originalPosition = transform.localPosition;
        }

        public void Shake(float duration, float strength)
        {
            if (_shakeRoutine != null)
            {
                StopCoroutine(_shakeRoutine);
            }

            _shakeRoutine = StartCoroutine(ShakeRoutine(duration, strength));
        }

        private IEnumerator ShakeRoutine( float duration,float strength)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;

                float x = Random.Range(-1f, 1f) * strength;
                float y = Random.Range(-1f, 1f) * strength;

                transform.localPosition =  _originalPosition + new Vector3(x, y, 0f);

                yield return null;
            }

            transform.localPosition = _originalPosition;

            _shakeRoutine = null;
        }
    }
}