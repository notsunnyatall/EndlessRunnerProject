using UnityEngine;

namespace EndlessRunner.Core
{
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] PlatformConfig initialPlatform;
        [SerializeField] PlatformConfig[] platformSequence;
        PlatformConfig currentConfig;
        GameObject currentPlatform;
        
        [System.Serializable]
        struct PlatformConfig
        {
            public GameObject platform;
            public float spawnDistance;
        }

        void Start()
        {
            currentConfig = initialPlatform;
            currentPlatform = currentConfig.platform;
        }

        void Update()
        {
            Vector3 platformPosition = currentPlatform.transform.position;
            float spawnDistance = currentConfig.spawnDistance;

            if(Vector3.Distance(transform.position, platformPosition) >= spawnDistance)
            {
                int randomIndex = Random.Range(0, platformSequence.Length);

                currentConfig = platformSequence[randomIndex];

                currentPlatform = Instantiate(currentConfig.platform, transform.position, Quaternion.identity);
            }
        }
    }
}
