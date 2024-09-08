using UnityEngine;

namespace EndlessRunner.Core
{
    public class Scorer : MonoBehaviour
    {
        GameSettings settings;
        float currentScore = 0;

        public float GetScore()
        {
            return currentScore;
        }

        void Awake()
        {
            settings = FindObjectOfType<GameSettings>();
        }

        void Update()
        {   
            currentScore += settings.GetGameSpeed() * Time.deltaTime;
        }
    }
}
