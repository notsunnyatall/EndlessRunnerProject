using UnityEngine;

namespace EndlessRunner.Core
{
    public class DestroyOnHide : MonoBehaviour
    {
        [SerializeField] GameObject destroyTarget;

        void OnBecameInvisible()
        {
            Destroy(destroyTarget);
        }
    }
}