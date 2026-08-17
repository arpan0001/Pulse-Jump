using System.Collections;
using UnityEngine;
using PulseJump.Obstacles;

namespace PulseJump.VFX
{
    public class MilestoneVFXController : MonoBehaviour
    {
        [Header("Milestone")]

        [SerializeField]
        private int scorePerMilestone = 10;


        [Header("UI Image")]

        [SerializeField]
        private RectTransform milestoneImage;

        [SerializeField]
        private float zoomDuration = 0.35f;

        [SerializeField]
        private float peakImageScale = 1.2f;


        [Header("Player Particles")]

        [SerializeField]
        private ParticleSystem[] milestoneParticles;


        [Header("Regular Pulse VFX")]

        [SerializeField]
        private PulseVFXController pulseVFXController;


        private int passedBarrierCount;
        private int milestoneNumber;
        private Coroutine milestoneRoutine;


        // Hides the milestone image and stops all milestone particles when the object starts.
        private void Awake()
        {
            if (milestoneImage != null)
            {
                milestoneImage.gameObject.SetActive(false);
            }

            StopMilestoneParticles();
        }


        // Starts listening for successful barrier passes.
        private void OnEnable()
        {
            BarrierController.BarrierPassed +=  CheckMilestone;
        }


        // Stops listening for barrier events and resets the VFX when disabled.
        private void OnDisable()
        {
            BarrierController.BarrierPassed -=CheckMilestone;

            StopMilestoneParticles();

            if (pulseVFXController != null)
            {
                pulseVFXController.SetEffectsBlocked(false);
            }
        }


        // Counts passed barriers and starts a milestone effect every set number of barriers.
        private void CheckMilestone()
        {
            passedBarrierCount++;

            if (passedBarrierCount % scorePerMilestone != 0)
            {
                return;
            }

            milestoneNumber++;

            if (milestoneRoutine != null)
            {
                StopCoroutine(milestoneRoutine);
            }

            milestoneRoutine = StartCoroutine(MilestoneRoutine());
        }


        // Runs the complete milestone effect sequence.
        private IEnumerator MilestoneRoutine()
        {
            
            if (pulseVFXController != null)
            {
                pulseVFXController.SetEffectsBlocked(true);
            }

            
            StopMilestoneParticles();

            yield return StartCoroutine( PlayZoomImage());

            PlayMilestoneParticle();

            milestoneRoutine = null;
        }


        // Shows the milestone image by smoothly zooming it in and then out.
        private IEnumerator PlayZoomImage()
        {
            if (milestoneImage == null)
                yield break;

            milestoneImage.gameObject.SetActive(true);

            float elapsed = 0f;

            while (elapsed < zoomDuration)
            {
                elapsed += Time.deltaTime;

                float t = Mathf.SmoothStep( 0f, 1f, elapsed / zoomDuration);

                float scale = Mathf.Lerp( 0.2f, peakImageScale,  t);

                milestoneImage.localScale =Vector3.one * scale;

                yield return null;
            }

            elapsed = 0f;

            while (elapsed < zoomDuration)
            {
                elapsed += Time.deltaTime;

                float t = Mathf.SmoothStep(0f, 1f,elapsed / zoomDuration);

                float scale = Mathf.Lerp(peakImageScale, 0f, t);

                milestoneImage.localScale = Vector3.one * scale;

                yield return null;
            }

            milestoneImage.gameObject.SetActive(false);
        }


        // Selects and plays the particle effect for the current milestone.
        private void PlayMilestoneParticle()
        {
            if (milestoneParticles == null ||
                milestoneParticles.Length == 0)
            {
                return;
            }

            int index = (milestoneNumber - 1) % milestoneParticles.Length;

            ParticleSystem particle = milestoneParticles[index];

            if (particle == null)
                return;

            particle.gameObject.SetActive(true);

            particle.Stop( true, ParticleSystemStopBehavior .StopEmittingAndClear);

            particle.Play();
        }


        // Stops and disables all milestone particle effects.
        private void StopMilestoneParticles()
        {
            if (milestoneParticles == null)
                return;

            foreach (ParticleSystem particle
                in milestoneParticles)
            {
                if (particle == null)
                    continue;

                particle.Stop( true, ParticleSystemStopBehavior.StopEmittingAndClear);

                particle.gameObject.SetActive(false);
            }
        }
    }
}