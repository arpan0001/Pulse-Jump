using UnityEngine;

namespace PulseJump.VFX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class SpeedLineVFX : MonoBehaviour
    {
        [Header("Track Speed")]

        [SerializeField]
        private float minimumTrackSpeed = 10f;

        [SerializeField]
        private float maximumTrackSpeed = 100f;


        [Header("Particles At Low / High Speed")]

        [SerializeField]
        private float minimumEmissionRate = 3f;

        [SerializeField]
        private float maximumEmissionRate = 55f;

        [SerializeField]
        private float minimumSimulationSpeed = 0.4f;

        [SerializeField]
        private float maximumSimulationSpeed = 2.2f;


        private ParticleSystem particleSystem;
        private ParticleSystem.MainModule mainModule;
        private ParticleSystem.EmissionModule emissionModule;


        private void Awake()
        {
            particleSystem = GetComponent<ParticleSystem>();

            mainModule = particleSystem.main;
            emissionModule = particleSystem.emission;
        }


        private void Start()
        {
            // Start with fewer, slower speed lines.
            SetTrackSpeed(minimumTrackSpeed);
        }


        public void SetTrackSpeed(float trackSpeed)
        {
            float t = Mathf.InverseLerp( minimumTrackSpeed, maximumTrackSpeed, trackSpeed);

            emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve( Mathf.Lerp( minimumEmissionRate,maximumEmissionRate, t));

            mainModule.simulationSpeed = Mathf.Lerp( minimumSimulationSpeed, maximumSimulationSpeed,t);
        }
    }
}