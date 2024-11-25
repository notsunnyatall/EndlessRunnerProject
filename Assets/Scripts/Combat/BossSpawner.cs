using System.Collections;
using UnityEngine;

namespace EndlessRunner.Combat
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] GameObject bossPrefab;
        [SerializeField] float timeToSpawn;

        void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(timeToSpawn);

            Instantiate(bossPrefab, transform.position, Quaternion.identity);
        }
    }
}