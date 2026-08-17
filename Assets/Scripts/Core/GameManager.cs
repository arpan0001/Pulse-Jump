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


        // Creates the GameManager as a singleton and sets the initial game state.
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            CurrentState = GameState.MainMenu;
        }


        // Starts the game and begins moving the world.
        public void StartGame()
        {
            CurrentState = GameState.Playing;
            worldMovement.StartMovement();
        }


        // Pauses the game and stops the world movement.
        public void PauseGame()
        {
            if (CurrentState != GameState.Playing)
                return;


            CurrentState =  GameState.Paused;

            worldMovement.StopMovement();

            Time.timeScale = 0f;
        }


        // Resumes the paused game and starts the world movement again.
        public void ResumeGame()
        {
            if (CurrentState !=  GameState.Paused)
                return;

            Time.timeScale = 1f;

            CurrentState =  GameState.Playing;

            worldMovement.StartMovement();
        }


        // Changes the game state to Game Over and stops the world movement.
        public void GameOver()
        {
            Time.timeScale = 1f;

            CurrentState = GameState.GameOver;

            worldMovement.StopMovement();
        }

        
        private void Start()
        {
            StartGame();
        }


        // Changes the game state to Won and stops the world movement.
        public void WinGame()
        {
            Time.timeScale = 1f;

            CurrentState = GameState.Won;

            worldMovement.StopMovement();
        }
    }
}