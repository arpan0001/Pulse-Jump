using UnityEngine;
using PulseJump.Level;

namespace PulseJump.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState CurrentState { get; private set; }


        [Header("References")]

        [SerializeField]
        private WorldMovement worldMovement;


        private void Awake()
        {
            if (Instance != null &&
                Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            CurrentState =
                GameState.MainMenu;
        }


        public void StartGame()
        {
            CurrentState =
                GameState.Playing;

            worldMovement.StartMovement();
        }


        public void PauseGame()
        {
            if (CurrentState !=
                GameState.Playing)
                return;


            CurrentState =
                GameState.Paused;

            worldMovement.StopMovement();

            Time.timeScale = 0f;
        }


        public void ResumeGame()
        {
            if (CurrentState !=
                GameState.Paused)
                return;


            Time.timeScale = 1f;

            CurrentState =
                GameState.Playing;

            worldMovement.StartMovement();
        }


        public void GameOver()
        {
            Time.timeScale = 1f;

            CurrentState =
                GameState.GameOver;

            worldMovement.StopMovement();
        }


        public void WinGame()
        {
            Time.timeScale = 1f;

            CurrentState =
                GameState.Won;

            worldMovement.StopMovement();
        }
    }
}