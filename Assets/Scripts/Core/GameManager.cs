using UnityEngine;

namespace PulseJump.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState State { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            State = GameState.MainMenu;
        }

        public void StartGame()
        {
            State = GameState.Playing;
        }

        public void PauseGame()
        {
            if (State != GameState.Playing)
                return;

            State = GameState.Paused;

            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            if (State != GameState.Paused)
                return;

            Time.timeScale = 1f;

            State = GameState.Playing;
        }

        public void GameOver()
        {
            if (State == GameState.GameOver)
                return;

            Time.timeScale = 1f;

            State = GameState.GameOver;
        }

        public void WinGame()
        {
            if (State == GameState.Won)
                return;

            Time.timeScale = 1f;

            State = GameState.Won;
        }
    }
}