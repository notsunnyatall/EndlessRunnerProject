using UnityEngine;

namespace EndlessRunner.Core
{
    public class Platform : MonoBehaviour
    {
        GameSettings gameSettings;

        void Start()
        {
            gameSettings = FindObjectOfType<GameSettings>();
        }

        void Update()
        {
            transform.Translate(Vector2.left * gameSettings.GetGameSpeed() * Time.deltaTime);
        }
    }
}