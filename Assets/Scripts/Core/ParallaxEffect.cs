using UnityEngine;

namespace EndlessRunner.Core
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] ParallaxItem[] parallaxItems;
        GameSettings settings;

        [System.Serializable]
        struct ParallaxItem
        {
            public MeshRenderer renderer;
            public float speedMultiplier;
        }

        void Awake()
        {
            settings = FindObjectOfType<GameSettings>();
        }

        void Update()
        {
            float speed = settings.GetGameSpeed() / transform.localScale.x;

            foreach(var item in parallaxItems)
            {
                Material material = item.renderer.material;
                float totalSpeed = item.speedMultiplier * speed;
                material.mainTextureOffset += Vector2.right * totalSpeed * Time.deltaTime;
            }
        }
    }
}