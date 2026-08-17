using UnityEngine;
using PulseJump.Audio;

namespace PulseJump.Game
{
    public class PauseController : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private GameStartController gameStartController;

        [SerializeField]
        private AudioManager audioManager;

        [Header("Pause UI")]

        [SerializeField]
        private GameObject pausePanel;


        private bool _isPaused;


        // Returns whether the game is currently paused.
        public bool IsPaused => _isPaused;


        // Initializes the pause state and hides the pause panel.
        private void Awake()
        {
            _isPaused = false;


            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }
        }

        // Pauses the game, shows the pause panel, and pauses the music.
        public void PauseGame()
        {
            if (gameStartController != null && !gameStartController.IsGameStarted)
            {
                return;
            }
            if (audioManager != null)
            {
                audioManager.PauseMusic();
            }

            if (_isPaused)
                return;

            _isPaused = true;


            if (pausePanel != null)
            {
                pausePanel.SetActive(true);
            }


            Time.timeScale = 0f;
        }

        // Resumes the game, hides the pause panel, and resumes the music.
        public void ResumeGame()
        {
            if (!_isPaused)
                return;

            if (audioManager != null)
            {
                audioManager.ResumeMusic();
            }
            _isPaused = false;


            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }


            Time.timeScale = 1f;
        }

        // Switches between the paused and resumed game states.
        public void TogglePause()
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }


        // Makes sure the game time scale is restored when this object is destroyed.
        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }
    }
}