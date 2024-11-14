using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EndlessRunner.Core
{
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] PlatformConfig initialPlatformConfig;
        [SerializeField] PlatformConfig[] platformSequence;
        GameSettings gameSettings;
        PlatformConfig currentConfig;
        Platform currentPlatform;
  
        [System.Serializable]
        struct PlatformConfig
        {
            public Platform platform;
            public float spawnDistance;
            public float minSpawnTime;
            public float maxSpawnTime;
        }

        void Awake()
        {
            gameSettings = FindObjectOfType<GameSettings>();
        }

        void Start()
        {
            currentConfig = initialPlatformConfig;
            currentPlatform = currentConfig.platform;
        }

        void Update()
        {
            Vector3 platformPosition = currentPlatform.transform.position;
            float spawnDistance = currentConfig.spawnDistance;
            
            if(Vector3.Distance(transform.position, platformPosition) >= spawnDistance)
            {
                currentConfig = GetRandomPlatform();
                currentPlatform = Instantiate(currentConfig.platform, transform.position, Quaternion.identity);
            }
        }

        PlatformConfig GetRandomPlatform()
        {
            PlatformConfig[] validPlatforms = GetValidPlatforms().ToArray();
            return validPlatforms[Random.Range(0, validPlatforms.Length)];
        }

        IEnumerable<PlatformConfig> GetValidPlatforms()
        {
            foreach(var platformConfig in platformSequence)
            {
                float currentTime = gameSettings.GetCurrentTime();
                float minTime = platformConfig.minSpawnTime;
                float maxTime = platformConfig.maxSpawnTime;

                if(currentTime > minTime && currentTime <= maxTime)
                {
                    yield return platformConfig;
                }
            }
        }
    } 
}
