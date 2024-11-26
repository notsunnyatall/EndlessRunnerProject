using UnityEngine;

namespace EndlessRunner.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObject;
        static bool spawned = false;

        void Awake()
        {
            if(!spawned)
            {
                DontDestroyOnLoad(Instantiate(persistentObject));
                spawned = true;
            }
        }
    }
}