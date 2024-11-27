using UnityEngine;
using UnityEngine.UI;

namespace EndlessRunner.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] Button quitButton;

        void Awake()
        {
            quitButton.onClick.AddListener(Quit);
        }

        void Quit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}