using UnityEngine;

namespace EndlessRunner.Core

{
    public class GameSettings : MonoBehaviour
    {   
        [SerializeField] float initalSpeed = 1;
        [SerializeField] float speedMultiplier = 0.3f;
        [SerializeField] float maxSpeed = 10;
        float speed = 0;

        public float GetGameSpeed()
        {
            return speed;
        }

        void Awake()
        {
            speed = initalSpeed;
        }

        void Update()
        {
            speed = Mathf.Min(maxSpeed, speed + (speedMultiplier * Time.deltaTime));
        }
    }
}