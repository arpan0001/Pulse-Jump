using PulseJump.Audio;
using UnityEngine;

namespace PulseJump.UI
{
    public class UIButtonSound : MonoBehaviour
    {
        [SerializeField]
        private AudioManager audioManager;


        public void PlayClick()
        {
            if (audioManager != null)
            {
                audioManager.PlayButtonClick();
            }
        }
    }
}