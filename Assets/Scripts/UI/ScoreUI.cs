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
            scorer = FindObjectOfType<Scorer>();
            scoreText = GetComponent<TMP_Text>();
        }

        void Update()
        {
            scoreText.text = $"Score:{Mathf.FloorToInt(scorer.GetScore())}";
        }
    }
}