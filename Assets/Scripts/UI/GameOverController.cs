using UnityEngine;
using UnityEngine.SceneManagement;
using PulseJump.Obstacles;
using PulseJump.Audio;
using PulseJump.Player;
using PulseJump.Level;
using TMPro;

namespace PulseJump.Game
{
    public class GameOverController : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private WorldMovement worldMovement;

        [SerializeField]
        private AudioManager audioManager;
        [SerializeField]
        private DifficultyManager difficultyManager;

        [SerializeField]
        private GameStatistics gameStatistics;

        [Header("Game Over Statistics")]

        [SerializeField]
        private TMP_Text finalScoreText;

        [SerializeField]
        private TMP_Text finalDistanceText;

        [SerializeField]
        private PulseController pulseController;


        [Header("Game Over UI")]

        [SerializeField]
        private GameObject gameOverPanel;


        private bool _gameOver;


        public bool IsGameOver => _gameOver;


        // ==================================================
        // AWAKE
        // ==================================================

        private void Awake()
        {
            _gameOver = false;

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(false);
            }
        }

        private void OnEnable()
        {
            BarrierController.PlayerFailed += TriggerGameOver;
        }


        private void OnDisable()
        {
            BarrierController.PlayerFailed -= TriggerGameOver;
        }


        // ==================================================
        // GAME OVER
        // ==================================================

        public void TriggerGameOver()
        {
            if (_gameOver)
                return;

            _gameOver = true;

            if (audioManager != null)
            {
                audioManager.StopMusic();
            }
            // ----------------------------------------------
            // SHOW FINAL STATISTICS
            // ----------------------------------------------

            if (gameStatistics != null)
            {
                if (finalScoreText != null)
                {
                    finalScoreText.text =
                        "Score: " + gameStatistics.Score;
                }

                if (finalDistanceText != null)
                {
                    finalDistanceText.text =
                        "Distance: " +
                        gameStatistics.Distance.ToString("0") +
                        " m";
                }
            }


            // ----------------------------------------------
            // STOP WORLD
            // ----------------------------------------------

            if (worldMovement != null)
            {
                worldMovement.StopMovement();
            }


            // ----------------------------------------------
            // STOP DIFFICULTY
            // ----------------------------------------------

            if (difficultyManager != null)
            {
                difficultyManager.StopDifficulty();
            }


            // ----------------------------------------------
            // STOP STATISTICS
            // ----------------------------------------------

            if (gameStatistics != null)
            {
                gameStatistics.StopStatistics();
            }


            // ----------------------------------------------
            // DISABLE PULSE
            // ----------------------------------------------

            if (pulseController != null)
            {
                pulseController.SetInputEnabled(false);
            }


            // ----------------------------------------------
            // SHOW GAME OVER PANEL
            // ----------------------------------------------

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }

        // ==================================================
        // RESTART
        // ==================================================

        public void RestartGame()
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}