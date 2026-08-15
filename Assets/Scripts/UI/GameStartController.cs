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


        // ==================================================
        // AWAKE
        // ==================================================

        private void Awake()
        {
            _gameStarted = false;

            // ----------------------------------------------
            // RESET TIME
            // ----------------------------------------------

            Time.timeScale = 1f;

            if (pauseButton != null)
            {
                pauseButton.SetActive(false);
            }
            // ----------------------------------------------
            // PAUSE GAME
            // ----------------------------------------------

            Time.timeScale = 0f;


            // ----------------------------------------------
            // STOP WORLD
            // ----------------------------------------------

            if (worldMovement != null)
            {
                worldMovement.StopMovement();
            }


            // ----------------------------------------------
            // STOP STATISTICS
            // ----------------------------------------------

            if (gameStatistics != null)
            {
                gameStatistics.StopStatistics();
            }


            // ----------------------------------------------
            // STOP DIFFICULTY
            // ----------------------------------------------

            if (difficultyManager != null)
            {
                difficultyManager.StopDifficulty();
            }


            // ----------------------------------------------
            // DISABLE PULSE
            // ----------------------------------------------

            if (pulseController != null)
            {
                pulseController.SetInputEnabled(false);
            }
        }


        // ==================================================
        // START
        // ==================================================

        private void Start()
        {
            // Make sure the game remains paused.
            Time.timeScale = 0f;


            // Show start UI.
            if (tapToStartUI != null)
            {
                tapToStartUI.SetActive(true);
            }
        }


        // ==================================================
        // UPDATE
        // ==================================================

        private void Update()
        {
            // Game has already started.
            if (_gameStarted)
                return;


            // Detect first tap.
            if (WasTapped())
            {
                StartGame();
            }
        }


        // ==================================================
        // INPUT
        // ==================================================

        private bool WasTapped()
        {
#if UNITY_EDITOR

            return Input.GetMouseButtonDown(0);

#else

            return Input.touchCount > 0 &&
                   Input.GetTouch(0).phase == TouchPhase.Began;

#endif
        }


        // ==================================================
        // START GAME
        // ==================================================

        private void StartGame()
        {
            if (_gameStarted)
                return;


            _gameStarted = true;
            if (pauseButton != null)
            {
                pauseButton.SetActive(true);
            }

            // ----------------------------------------------
            // RESUME GAME
            // ----------------------------------------------

            Time.timeScale = 1f;


            // ----------------------------------------------
            // HIDE TAP TO START
            // ----------------------------------------------

            if (tapToStartUI != null)
            {
                tapToStartUI.SetActive(false);
            }


            // ----------------------------------------------
            // RESET + START STATISTICS
            // ----------------------------------------------

            if (gameStatistics != null)
            {
                gameStatistics.ResetStatistics();

                gameStatistics.StartStatistics();
            }


            // ----------------------------------------------
            // START DIFFICULTY
            // ----------------------------------------------

            if (difficultyManager != null)
            {
                difficultyManager.StartDifficulty();
            }


            // ----------------------------------------------
            // START WORLD
            // ----------------------------------------------

            if (worldMovement != null)
            {
                worldMovement.StartMovement();
            }


            // ----------------------------------------------
            // KEEP PULSE DISABLED
            // ----------------------------------------------

            if (pulseController != null)
            {
                pulseController.SetInputEnabled(false);
            }


            // ----------------------------------------------
            // ENABLE PULSE AFTER 1 SECOND
            // ----------------------------------------------

            _pulseUnlockCoroutine =
                StartCoroutine(
                    EnablePulseAfterDelay());
        }


        // ==================================================
        // ENABLE PULSE AFTER DELAY
        // ==================================================

        private IEnumerator EnablePulseAfterDelay()
        {
            // Wait one second of actual gameplay time.
            yield return new WaitForSecondsRealtime(
                pulseUnlockDelay);


            // Safety check.
            if (!_gameStarted)
                yield break;


            // Enable pulse input.
            if (pulseController != null)
            {
                pulseController.SetInputEnabled(true);
            }


            _pulseUnlockCoroutine = null;
        }


        // ==================================================
        // CLEANUP
        // ==================================================

        private void OnDestroy()
        {
            if (_pulseUnlockCoroutine != null)
            {
                StopCoroutine(
                    _pulseUnlockCoroutine);
            }


            // Never leave another scene paused.
            Time.timeScale = 1f;
        }
    }
}