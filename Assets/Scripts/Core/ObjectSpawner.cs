using System.Collections;
using UnityEngine;

namespace EndlessRunner.Core
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
            [Min(1)] public int[] numbers;
            public float delay;
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

                StartCoroutine(SpawnRoutine(obstaclesConfig[randomIndex]));
            }
        }

        IEnumerator SpawnRoutine(SpawnData spawnData)
        {
            int randomSpawnNumber = Random.Range(0, spawnData.numbers.Length);

            for(int i = 0; i < spawnData.numbers[randomSpawnNumber]; i++)
            {
                Vector3 offset = spawnData.offset;
                GameObject objectToSpawn = spawnData.objectToSpawn;

                Instantiate(objectToSpawn, transform.position + offset, Quaternion.identity);

                yield return new WaitForSeconds(spawnData.delay);
            }
        }
    }
}
