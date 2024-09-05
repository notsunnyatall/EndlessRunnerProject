using UnityEngine;

namespace EndlessRunner.Obstacles
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] SpawnData[] obstaclesConfig;
        [SerializeField] float minSpawnDelay = 1;
        [SerializeField] float maxSpawnDelay = 4;
        float timeSinceSpawned = Mathf.Infinity;
        float spawnDelay = 0;

        [System.Serializable]
        struct SpawnData
        {
            public GameObject objectToSpawn;
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

                GameObject objectToSpawn = obstaclesConfig[randomIndex].objectToSpawn;

                Instantiate(objectToSpawn, transform.position + offset, Quaternion.identity);
            }
        }
    }
}
