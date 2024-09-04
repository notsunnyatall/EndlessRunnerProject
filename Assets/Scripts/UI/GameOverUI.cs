using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EndlessRunner.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] Button restartButton;

        void OnEnable()
        {
            Time.timeScale = 0;
        }

        void OnDisable()
        {
            Time.timeScale = 1;
        }

        void Awake()
        {
            restartButton.onClick.AddListener(ReloadScene);
        }

        void ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}