using System.Collections;
using UnityEngine;

namespace PulseJump.VFX
{
    public class PulseVFXController : MonoBehaviour
    {
        [Header("Pulse Shader")]

        [SerializeField]
        private GameObject pulseShaderObject;


        [SerializeField]
        private Transform pulseShaderTransform;


        [Header("Scale")]

        [SerializeField]
        private float startScale = 1.0f;

        [SerializeField]
        private float peakScale = 1.25f;

        [SerializeField]
        private float expandDuration = 0.12f;

        [SerializeField]
        private float holdDuration = 0.20f;

        [SerializeField]
        private float shrinkDuration = 0.18f;




        private Coroutine _pulseRoutine;


        private void Awake()
        {
            if (pulseShaderObject != null)
            {
                pulseShaderObject.SetActive(false);
            }
        }


        public void PlayPulseEffect()
        {
            if (_pulseRoutine != null)
            {
                StopCoroutine(_pulseRoutine);
            }


            _pulseRoutine =
                StartCoroutine(
                    PulseEffectRoutine());
        }


        private IEnumerator PulseEffectRoutine()
        {
            if (pulseShaderObject == null ||
                pulseShaderTransform == null)
            {
                yield break;
            }


            pulseShaderObject.SetActive(true);


            // Start small.
            pulseShaderTransform.localScale =
                Vector3.one * startScale;


            // Play particles.
            


            // Expand.
            yield return StartCoroutine(
                ScaleShader(
                    startScale,
                    peakScale,
                    expandDuration));


            // Hold.
            yield return new WaitForSeconds(
                holdDuration);


            // Shrink.
            yield return StartCoroutine(
                ScaleShader(
                    peakScale,
                    startScale,
                    shrinkDuration));


            pulseShaderObject.SetActive(false);


            _pulseRoutine = null;
        }


        private IEnumerator ScaleShader(
            float from,
            float to,
            float duration)
        {
            float elapsed = 0f;


            while (elapsed < duration)
            {
                elapsed +=
                    Time.deltaTime;


                float t =
                    elapsed / duration;


                t = Mathf.SmoothStep(
                    0f,
                    1f,
                    t);


                float scale =
                    Mathf.Lerp(
                        from,
                        to,
                        t);


                pulseShaderTransform.localScale =
                    Vector3.one * scale;


                yield return null;
            }


            pulseShaderTransform.localScale =
                Vector3.one * to;
        }
    }
}