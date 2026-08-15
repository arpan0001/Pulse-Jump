using UnityEngine;

namespace PulseJump.Game
{
    public class PauseController : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private GameStartController gameStartController;


        [Header("Pause UI")]

        [SerializeField]
        private GameObject pausePanel;


        private bool _isPaused;


        public bool IsPaused => _isPaused;


        private void Awake()
        {
            _isPaused = false;


            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }
        }


        // ==================================================
        // PAUSE
        // ==================================================

        public void PauseGame()
        {
            // Don't pause before gameplay starts.
            if (gameStartController != null &&
                !gameStartController.IsGameStarted)
            {
                return;
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


        // ==================================================
        // RESUME
        // ==================================================

        public void ResumeGame()
        {
            if (!_isPaused)
                return;


            _isPaused = false;


            if (pausePanel != null)
            {
                pausePanel.SetActive(false);
            }


            Time.timeScale = 1f;
        }


        // ==================================================
        // TOGGLE
        // ==================================================

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


        // ==================================================
        // CLEANUP
        // ==================================================

        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }
    }
}