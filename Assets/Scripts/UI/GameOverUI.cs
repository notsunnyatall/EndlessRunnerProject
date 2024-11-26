using EndlessRunner.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EndlessRunner.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] Button restartButton;
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
            restartButton.onClick.AddListener(ReloadScene);
        }

        void Start()
        {
            scorer = FindObjectOfType<Scorer>();
        }

        void ReloadScene()
        {
            scorer.ResetScore();
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}