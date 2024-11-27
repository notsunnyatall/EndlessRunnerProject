using EndlessRunner.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EndlessRunner.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] Button restartButton;
        [SerializeField] Button mainMenuButton;
        Scorer scorer;

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
            restartButton.onClick.AddListener(() => LoadScene(1));
            mainMenuButton.onClick.AddListener(() => LoadScene(0));
        }

        void Start()
        {
            scorer = FindObjectOfType<Scorer>();
        }

        void LoadScene(int sceneIndex)
        {
            scorer.ResetScore();
            SceneManager.LoadScene(sceneIndex);
        }
    }
}