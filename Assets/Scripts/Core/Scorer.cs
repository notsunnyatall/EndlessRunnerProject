using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EndlessRunner.Core
{
    public class Scorer : MonoBehaviour
    {
        [SerializeField] int[] allowedScenes;
        GameSettings settings;
        float currentScore = 0;
        HashSet<int> allowedScenesLookup;

        public void ResetScore()
        {
            currentScore = 0;
        }

        public float GetScore()
        {
            return currentScore;
        }

        void Start()
        {
            settings = FindObjectOfType<GameSettings>();

            allowedScenesLookup = new HashSet<int>(allowedScenes);
        }

        void Update()
        {
            if (allowedScenesLookup.Contains(SceneManager.GetActiveScene().buildIndex))
            {
                currentScore += settings.GetGameSpeed() * Time.deltaTime;
            }
        }
    }
}
