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


        [Header("Input")]

        [SerializeField]
        private bool inputEnabledAtStart = false;


        private bool _inputEnabled;

        private Vector3 _normalScale;

        private Coroutine _pulseCoroutine;

        private bool _isPulsing;


        public bool IsPulsing => _isPulsing;


        // ==================================================
        // AWAKE
        // ==================================================

        private void Awake()
        {
            // Store player's original scale.
            _normalScale = transform.localScale;


            // Pulse should normally be disabled
            // when the scene starts.
            _inputEnabled =
                inputEnabledAtStart;
        }


        // ==================================================
        // UPDATE
        // ==================================================

        private void Update()
        {
            // Pulse input isn't active yet.
            if (!_inputEnabled)
                return;


            // Don't allow another pulse while
            // current pulse is running.
            if (_isPulsing)
                return;


            // Detect tap.
            if (WasTapped())
            {
                TryPulse();
            }
        }


        // ==================================================
        // INPUT
        // ==================================================

        private bool WasTapped()
        {
#if UNITY_EDITOR

            return Input.GetMouseButtonDown(0);

#else

            return Input.touchCount > 0 &&
                   Input.GetTouch(0).phase ==
                   TouchPhase.Began;

#endif
        }


        // ==================================================
        // ENABLE / DISABLE INPUT
        // ==================================================

        public void SetInputEnabled(bool enabled)
        {
            _inputEnabled = enabled;


            if (!enabled)
            {
                Debug.Log(
                    "Pulse input disabled.");
            }
            else
            {
                Debug.Log(
                    "Pulse input enabled.");
            }
        }


        // ==================================================
        // TRY PULSE
        // ==================================================

        public void TryPulse()
        {
            // Safety check.
            if (!_inputEnabled)
                return;


            // Don't start another pulse
            // while one is already running.
            if (_pulseCoroutine != null)
                return;


            _pulseCoroutine =
                StartCoroutine(
                    PulseRoutine());
        }


        // ==================================================
        // PULSE ROUTINE
        // ==================================================

        private IEnumerator PulseRoutine()
        {
            _isPulsing = true;


            // ----------------------------------------------
            // EXPAND
            // ----------------------------------------------

            yield return StartCoroutine(
                ScaleTo(
                    _normalScale *
                    pulseScale,

                    expandDuration
                )
            );


            // ----------------------------------------------
            // HOLD
            // ----------------------------------------------

            yield return new WaitForSeconds(
                holdDuration
            );


            // ----------------------------------------------
            // SHRINK
            // ----------------------------------------------

            yield return StartCoroutine(
                ScaleTo(
                    _normalScale,

                    shrinkDuration
                )
            );


            // ----------------------------------------------
            // RESET
            // ----------------------------------------------

            transform.localScale =
                _normalScale;


            _isPulsing = false;


            _pulseCoroutine = null;
        }


        // ==================================================
        // SCALE ANIMATION
        // ==================================================

        private IEnumerator ScaleTo(
            Vector3 targetScale,
            float duration)
        {
            Vector3 startScale =
                transform.localScale;


            // Handle zero duration.
            if (duration <= 0f)
            {
                transform.localScale =
                    targetScale;

                yield break;
            }


            float elapsed = 0f;


            while (elapsed < duration)
            {
                elapsed +=
                    Time.deltaTime;


                float t =
                    elapsed /
                    duration;


                // Smooth animation.
                t = Mathf.SmoothStep(
                    0f,
                    1f,
                    t
                );


                transform.localScale =
                    Vector3.Lerp(
                        startScale,
                        targetScale,
                        t
                    );


                yield return null;
            }


            transform.localScale =
                targetScale;
        }
    }
}