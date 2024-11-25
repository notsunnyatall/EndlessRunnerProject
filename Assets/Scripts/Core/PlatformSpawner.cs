using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EndlessRunner.Core
{
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] PlatformConfig initialPlatformConfig;
        [SerializeField] PlatformConfig[] platformSequence;
        PlatformConfig currentConfig;
        Platform currentPlatform;
        float currentTime;
  
        [System.Serializable]
        struct PlatformConfig
        {
            public Platform platform;
            public float spawnDistance;
            public float minSpawnTime;
            public float maxSpawnTime;
        }

        void Start()
        {
            currentConfig = initialPlatformConfig;
            currentPlatform = currentConfig.platform;
        }

        void Update()
        {
            currentTime += Time.deltaTime;

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
