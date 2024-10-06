using EndlessRunner.Attributes;
using UnityEngine;

namespace EndlessRunner.Combat
{
    public class Killer : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Health health))
            {
                health.Kill();
            }
        }
    }
}