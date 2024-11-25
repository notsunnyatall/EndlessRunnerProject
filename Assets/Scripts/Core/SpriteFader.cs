using System.Collections;
using UnityEngine;

namespace EndlessRunner.Core
{
    public class SpriteFader : MonoBehaviour
    {
        [SerializeField] float fadeTime = 5;
        [SerializeField] [Range(0,1)] float initialAlpha = 0;
        SpriteRenderer spriteRenderer;

        public void FadeIn()
        {
            StartCoroutine(FadeRoutine(1));
        }

        public void FadeOut()
        {
            StartCoroutine(FadeRoutine(0));
        }

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            SetAlpha(initialAlpha);
            FadeIn();
        }

        IEnumerator FadeRoutine(float alphaTarget)
        {
            GetComponent<Collider>().enabled = false;

            while(!Mathf.Approximately(spriteRenderer.color.a, alphaTarget))
            {
                float fadeAlpha = Mathf.MoveTowards(spriteRenderer.color.a, alphaTarget, Time.unscaledDeltaTime / fadeTime);

                SetAlpha(fadeAlpha);

                yield return null;
            }

            GetComponent<Collider>().enabled = true;
        }
        
        void SetAlpha(float alpha)
        {
            Color baseColor = spriteRenderer.color;
            spriteRenderer.color = new(baseColor.r, baseColor.g, baseColor.b, alpha);
        }
    }
}