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


        // Initializes the game over state and hides the game over panel.
        private void Awake()
        {
            _gameOver = false;

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(false);
            }
        }

        // Starts listening for the player failure event.
        private void OnEnable()
        {
            BarrierController.PlayerFailed += TriggerGameOver;
        }


        // Stops listening for the player failure event.
        private void OnDisable()
        {
            BarrierController.PlayerFailed -= TriggerGameOver;
        }


        // Stops the game, displays the final statistics, and shows the game over panel.
        public void TriggerGameOver()
        {
            if (_gameOver)
                return;

            _gameOver = true;

            if (audioManager != null)
            {
                audioManager.StopMusic();
            }
           

            if (gameStatistics != null)
            {
                if (finalScoreText != null)
                {
                    finalScoreText.text ="Score: " + gameStatistics.Score;
                }

                if (finalDistanceText != null)
                {
                    finalDistanceText.text = "Distance: " +  gameStatistics.Distance.ToString("0") +  " m";
                }
            }

            if (worldMovement != null)
            {
                worldMovement.StopMovement();
            }

            if (difficultyManager != null)
            {
                difficultyManager.StopDifficulty();
            }

            if (gameStatistics != null)
            {
                gameStatistics.StopStatistics();
            }

            if (pulseController != null)
            {
                pulseController.SetInputEnabled(false);
            }

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }

        // Restarts the current scene and resets the game.
        public void RestartGame()
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}