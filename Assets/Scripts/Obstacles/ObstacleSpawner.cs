using UnityEngine;
using UnityEngine.Pool;

namespace EndlessRunner.Obstacles
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] ObstacleConfig[] obstaclesConfig;
        [SerializeField] float minSpawnDelay = 1;
        [SerializeField] float maxSpawnDelay = 4;
        float timeSinceSpawned = Mathf.Infinity;
        float spawnDelay = 0;

        [System.Serializable]
        struct ObstacleConfig
        {
            public Obstacle obstacle;
            public Vector3 offset;
        }

        void Update()
        {
            timeSinceSpawned += Time.deltaTime;

            if(timeSinceSpawned > spawnDelay)
            {
                timeSinceSpawned = 0;

                spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

                int randomIndex = Random.Range(0, obstaclesConfig.Length);

                Vector3 offset = obstaclesConfig[randomIndex].offset;

                Obstacle obstacle = obstaclesConfig[randomIndex].obstacle;

                Instantiate(obstacle, transform.position + offset, Quaternion.identity);
            }
        }
    }
}
