using System.Collections;
using UnityEngine;

namespace EndlessRunner.Combat
{
    public class BossActivator : MonoBehaviour
    {
        [SerializeField] GameObject boss;
        [SerializeField] float timeToActivate;

        void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        IEnumerator SpawnRoutine()
        {
            boss.SetActive(false);

            yield return new WaitForSeconds(timeToActivate);

            boss.SetActive(true);
        }
    }
}