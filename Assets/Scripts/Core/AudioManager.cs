using UnityEngine;
using PulseJump.Obstacles;

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

        [Header("Barrier Pass Sound")]

        [SerializeField]
        private float barrierPassCooldown = 0.15f;


        private float nextBarrierPassSoundTime;


        
        private void Awake()
        {
            SetupAudioSources();
        }


        
        private void Start()
        {
            PlayGameplayMusic();
        }

        
        public void PauseMusic()
        {
            if (musicSource == null)
                return;

            musicSource.Pause();
        }


        
        public void ResumeMusic()
        {
            if (musicSource == null)
                return;

            musicSource.UnPause();
        }


        // Configures the music and sound effect audio sources.
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

        // Connects the audio manager to barrier events.
        private void OnEnable()
        {
            BarrierController.BarrierPassed += OnBarrierPassed;
            BarrierController.PlayerFailed += OnPlayerFailed;
        }


        // Removes the audio manager from the barrier events.
        private void OnDisable()
        {
            BarrierController.BarrierPassed -= OnBarrierPassed;
            BarrierController.PlayerFailed -= OnPlayerFailed;
        }

        
        private void OnPlayerFailed()
        {
            PlayGameOverSound();
        }


        // Plays a sound when the player passes a barrier.
        private void OnBarrierPassed()
        {
            PlayBarrierPassSound();
        }

        // Starts playing the gameplay music.
        public void PlayGameplayMusic()
        {
            if (musicSource == null)
                return;

            if (gameplayMusic == null)
                return;

            musicSource.clip = gameplayMusic;
            musicSource.Play();
        }


        // Stops the currently playing music.
        public void StopMusic()
        {
            if (musicSource == null)
                return;

            musicSource.Stop();
        }

      
        public void PlayPulseSound()
        {
            PlaySFX(pulseSound);
        }


        // Plays the barrier pass sound while preventing it from playing too frequently.
        public void PlayBarrierPassSound()
        {
            if (Time.unscaledTime < nextBarrierPassSoundTime)
            {
                return;
            }

            nextBarrierPassSoundTime =  Time.unscaledTime + barrierPassCooldown;

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


        // Plays the given sound effect through the SFX audio source.
        private void PlaySFX(AudioClip clip)
        {
            if (sfxSource == null)
                return;

            if (clip == null)
                return;

            sfxSource.PlayOneShot( clip,  sfxVolume);
        }
    }
}