using UnityEngine;

namespace EndlessRunner.Core
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] float rotateSpeed = 2;

        void Update()
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.Self);
        }
    }
}