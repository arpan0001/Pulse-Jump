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
        private float shrinkScale = 0.9f;

        [SerializeField]
        private float expandDuration = 0.12f;

        [SerializeField]
        private float holdDuration = 0.20f;

        [SerializeField]
        private float shrinkDuration = 0.18f;


        private Coroutine _pulseRoutine;
        private bool _effectsBlocked;


        // Disables the pulse shader when the object starts.
        private void Awake()
        {
            if (pulseShaderObject != null)
            {
                pulseShaderObject.SetActive(false);
            }
        }


        // Starts the pulse visual effect if effects are not blocked.
        public void PlayPulseEffect()
        {
            if (_effectsBlocked)
                return;

            if (_pulseRoutine != null)
            {
                StopCoroutine(_pulseRoutine);
            }

            _pulseRoutine = StartCoroutine(PulseEffectRoutine());
        }


        // Controls the complete pulse effect from start to finish.
        private IEnumerator PulseEffectRoutine()
        {
            if (pulseShaderObject == null || pulseShaderTransform == null)
            {
                yield break;
            }

            pulseShaderObject.SetActive(true);

            pulseShaderTransform.localScale = Vector3.one * startScale;

            yield return StartCoroutine(ScaleShader( startScale,  peakScale,  expandDuration));

            yield return new WaitForSeconds( holdDuration);

            yield return StartCoroutine(ScaleShader( peakScale, shrinkScale,shrinkDuration));

            pulseShaderObject.SetActive(false);

            _pulseRoutine = null;
        }

        // Enables or disables the pulse effects and stops any running effect if blocked.
        public void SetEffectsBlocked(bool blocked)
        {
            _effectsBlocked = blocked;

            if (blocked)
            {
                if (_pulseRoutine != null)
                {
                    StopCoroutine(_pulseRoutine);
                    _pulseRoutine = null;
                }

                if (pulseShaderObject != null)
                {
                    pulseShaderObject.SetActive(false);
                }
            }
        }

        // Smoothly changes the pulse shader scale from one size to another.
        private IEnumerator ScaleShader(float from, float to, float duration)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float t = elapsed / duration;

                t = Mathf.SmoothStep( 0f, 1f, t);

                float scale = Mathf.Lerp(from,to, t);

                pulseShaderTransform.localScale =Vector3.one * scale;

                yield return null;
            }

            pulseShaderTransform.localScale = Vector3.one * to;
        }
    }
}