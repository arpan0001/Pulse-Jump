using UnityEngine;
using UnityEngine.SceneManagement;

namespace PulseJump.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Gameplay Scene")]
        [SerializeField]
        private string gameplaySceneName = "Gameplay";


        public void Play()
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(
                gameplaySceneName);
        }


        public void Quit()
        {
#if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying =
                false;

#else

            Application.Quit();

#endif
        }
    }
}