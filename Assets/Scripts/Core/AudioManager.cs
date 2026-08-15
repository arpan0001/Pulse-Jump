using UnityEngine;

namespace PulseJump.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Audio Sources")]

        [SerializeField]
        private AudioSource musicSource;

        [SerializeField]
        private AudioSource sfxSource;


        [Header("Music")]

        [SerializeField]
        private AudioClip gameplayMusic;


        [Header("SFX")]

        [SerializeField]
        private AudioClip pulseSound;

        [SerializeField]
        private AudioClip barrierPassSound;

        [SerializeField]
        private AudioClip gameOverSound;

        [SerializeField]
        private AudioClip buttonClickSound;


        [Header("Volume")]

        [SerializeField]
        [Range(0f, 1f)]
        private float musicVolume = 0.4f;

        [SerializeField]
        [Range(0f, 1f)]
        private float sfxVolume = 1f;


        private void Awake()
        {
            SetupAudioSources();
        }


        private void Start()
        {
            PlayGameplayMusic();
        }


        private void SetupAudioSources()
        {
            if (musicSource != null)
            {
                musicSource.loop = true;
                musicSource.playOnAwake = false;
                musicSource.volume = musicVolume;
            }


            if (sfxSource != null)
            {
                sfxSource.playOnAwake = false;
                sfxSource.volume = sfxVolume;
            }
        }


        // ==================================================
        // MUSIC
        // ==================================================

        public void PlayGameplayMusic()
        {
            if (musicSource == null)
                return;

            if (gameplayMusic == null)
                return;

            musicSource.clip = gameplayMusic;
            musicSource.Play();
        }


        public void StopMusic()
        {
            if (musicSource == null)
                return;

            musicSource.Stop();
        }


        // ==================================================
        // SFX
        // ==================================================

        public void PlayPulseSound()
        {
            PlaySFX(pulseSound);
        }


        public void PlayBarrierPassSound()
        {
            PlaySFX(barrierPassSound);
        }


        public void PlayGameOverSound()
        {
            PlaySFX(gameOverSound);
        }


        public void PlayButtonClick()
        {
            PlaySFX(buttonClickSound);
        }


        private void PlaySFX(AudioClip clip)
        {
            if (sfxSource == null)
                return;

            if (clip == null)
                return;

            sfxSource.PlayOneShot(
                clip,
                sfxVolume);
        }
    }
}