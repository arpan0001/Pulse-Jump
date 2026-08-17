using System.Collections;
using UnityEngine;
using PulseJump.Audio;
using PulseJump.VFX;

namespace PulseJump.Player
{
    public class PulseController : MonoBehaviour
    {
        [Header("Pulse Settings")]

        [SerializeField]
        private float pulseScale = 1.5f;

        [SerializeField]
        private AudioManager audioManager;
        [SerializeField]
        private float expandDuration = 0.12f;

        [SerializeField]
        private PulseVFXController pulseVFX;

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


        // Returns whether the player is currently performing a pulse.
        public bool IsPulsing => _isPulsing;


        // Saves the player's original scale and sets the initial input state.
        private void Awake()
        {
            // Store player's original scale.
            _normalScale = transform.localScale;

            // Pulse should normally be disabled

            _inputEnabled = inputEnabledAtStart;
        }

        // Checks for player input and starts a pulse when the player taps.
        private void Update()
        {
            if (!_inputEnabled)
                return;

            if (_isPulsing)
                return;

            if (WasTapped())
            {
                TryPulse();
            }
        }


        // Checks whether the player has tapped the screen or mouse.
        private bool WasTapped()
        {
         #if UNITY_EDITOR

            return Input.GetMouseButtonDown(0);

         #else

            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
         #endif
        }

        // Enables or disables pulse input.
        public void SetInputEnabled(bool enabled)
        {
            _inputEnabled = enabled;


            if (!enabled)
            {
                Debug.Log("Pulse input disabled.");
            }
            else
            {
                Debug.Log("Pulse input enabled.");
            }
        }




        // Starts the pulse if input is enabled and no other pulse is running.
        public void TryPulse()
        {
            if (!_inputEnabled)
                return;

            if (_isPulsing)
                return;

            if (_pulseCoroutine != null)
                return;
            if (pulseVFX != null)
            {
                pulseVFX.PlayPulseEffect();
            }



            if (audioManager != null)
            {
                audioManager.PlayPulseSound();
            }


            _pulseCoroutine =
                StartCoroutine(PulseRoutine());
        }


        // Controls the complete pulse animation from expanding to shrinking.
        private IEnumerator PulseRoutine()
        {
            _isPulsing = true;

            yield return StartCoroutine( ScaleTo(_normalScale *  pulseScale,  expandDuration ));

            yield return new WaitForSeconds( holdDuration);

            yield return StartCoroutine( ScaleTo ( _normalScale, shrinkDuration ));


            transform.localScale = _normalScale;


            _isPulsing = false;


            _pulseCoroutine = null;
        }


        // Smoothly changes the player's scale to the target scale.
        private IEnumerator ScaleTo( Vector3 targetScale, float duration)
        {
            Vector3 startScale =  transform.localScale;

            if (duration <= 0f)
            {
                transform.localScale =  targetScale;

                yield break;
            }


            float elapsed = 0f;


            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;


                float t =  elapsed /  duration;

                t = Mathf.SmoothStep( 0f,1f, t);


                transform.localScale = Vector3.Lerp( startScale,  targetScale,t  );


                yield return null;
            }


            transform.localScale =targetScale;
        }
    }
}