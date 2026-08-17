using System.Collections;
using PulseJump.Player;
using PulseJump.Level;
using UnityEngine;

namespace PulseJump.Game
{
    public class GameStartController : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private GameStatistics gameStatistics;

        [SerializeField]
        private DifficultyManager difficultyManager;

        [SerializeField]
        private WorldMovement worldMovement;

        [SerializeField]
        private PulseController pulseController;

        [Header("Gameplay UI")]

        [SerializeField]
        private GameObject pauseButton;
        [Header("Start UI")]

        [SerializeField]
        private GameObject tapToStartUI;


        [Header("Pulse Unlock")]

        [SerializeField]
        private float pulseUnlockDelay = 1f;


        private bool _gameStarted;

        private Coroutine _pulseUnlockCoroutine;

        public bool IsGameStarted => _gameStarted;


        // Initializes the game in a paused state and disables gameplay systems.
        private void Awake()
        {
            _gameStarted = false;

            Time.timeScale = 1f;

            if (pauseButton != null)
            {
                pauseButton.SetActive(false);
            }
           
            Time.timeScale = 0f;

            if (worldMovement != null)
            {
                worldMovement.StopMovement();
            }

            if (gameStatistics != null)
            {
                gameStatistics.StopStatistics();
            }

            if (difficultyManager != null)
            {
                difficultyManager.StopDifficulty();
            }

            if (pulseController != null)
            {
                pulseController.SetInputEnabled(false);
            }
        }

        // Keeps the game paused and shows the tap-to-start UI.
        private void Start()
        {
            
            Time.timeScale = 0f;

            if (tapToStartUI != null)
            {
                tapToStartUI.SetActive(true);
            }
        }

        // Waits for the player's first tap to start the game.
        private void Update()
        {
            if (_gameStarted)
                return;

            if (WasTapped())
            {
                StartGame();
            }
        }

        // Checks whether the player has tapped the screen or mouse.
        private bool WasTapped()
        {
          #if UNITY_EDITOR

            return Input.GetMouseButtonDown(0);

          #else

            return Input.touchCount > 0 &&
                   Input.GetTouch(0).phase == TouchPhase.Began;

          #endif
        }


        // Starts all gameplay systems and begins the game after the first tap.
        private void StartGame()
        {
            if (_gameStarted)
                return;


            _gameStarted = true;
            if (pauseButton != null)
            {
                pauseButton.SetActive(true);
            }

            Time.timeScale = 1f;

            if (tapToStartUI != null)
            {
                tapToStartUI.SetActive(false);
            }

            if (gameStatistics != null)
            {
                gameStatistics.ResetStatistics();

                gameStatistics.StartStatistics();
            }

            if (difficultyManager != null)
            {
                difficultyManager.StartDifficulty();
            }

            if (worldMovement != null)
            {
                worldMovement.StartMovement();
            }

            if (pulseController != null)
            {
                pulseController.SetInputEnabled(false);
            }

            _pulseUnlockCoroutine = StartCoroutine( EnablePulseAfterDelay());
        }

        // Waits for the unlock delay and then enables pulse input.
        private IEnumerator EnablePulseAfterDelay()
        {
            yield return new WaitForSecondsRealtime( pulseUnlockDelay);

            if (!_gameStarted)
                yield break;

            if (pulseController != null)
            {
                pulseController.SetInputEnabled(true);
            }


            _pulseUnlockCoroutine = null;
        }


        // Stops the pulse unlock coroutine and restores normal game time when destroyed.
        private void OnDestroy()
        {
            if (_pulseUnlockCoroutine != null)
            {
                StopCoroutine(_pulseUnlockCoroutine);
            }

            Time.timeScale = 1f;
        }
    }
}