using EndlessRunner.Core;
using TMPro;
using UnityEngine;

namespace EndlessRunner.UI
{
    public class ScoreUI : MonoBehaviour
    {
        Scorer scorer;
        TMP_Text scoreText;

        void Awake()
        {
            scoreText = GetComponent<TMP_Text>();
        }

        void Start()
        {
            scorer = FindObjectOfType<Scorer>();
        }

        void Update()
        {
            scoreText.text = $"Score:{Mathf.FloorToInt(scorer.GetScore())}";
        }
    }
}