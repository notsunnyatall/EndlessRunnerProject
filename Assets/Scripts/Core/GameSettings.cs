using UnityEngine;

namespace EndlessRunner.Core

{
    public class GameSettings : MonoBehaviour
    {   
        [SerializeField] float initalSpeed = 1;
        [SerializeField] float speedMultiplier = 0.3f;
        [SerializeField] float maxSpeed = 10;
        float speed = 0;
        float currentTime = 0;

        public float GetCurrentTime()
        {
            return currentTime;
        }

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
            currentTime += Time.deltaTime;
            speed = Mathf.Min(maxSpeed, speed + (speedMultiplier * Time.deltaTime));
        }
    }
}