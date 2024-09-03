using UnityEngine;

namespace EndlessRunner.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] float speed = 5;
        [SerializeField] Vector2 moveDirection = Vector2.left;

        void Update()
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }

        void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}