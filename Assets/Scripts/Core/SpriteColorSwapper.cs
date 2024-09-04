using System.Collections;
using UnityEngine;

namespace EndlessRunner.Core
{
    public class SpriteColorSwapper : MonoBehaviour
    {
        [SerializeField] float swapDuration = 1;
        SpriteRenderer spriteRenderer;
        Color initialColor;

        // Called in Unity Events
        public void SwapColor(string colorHtml)
        {
            StartCoroutine(SwapColorRoutine(colorHtml));
        }

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            initialColor = spriteRenderer.color;
        }

        IEnumerator SwapColorRoutine(string colorHtml)
        {
            if(ColorUtility.TryParseHtmlString("#" + colorHtml, out Color color))
            {
                spriteRenderer.color = color;
                yield return new WaitForSeconds(swapDuration);
                spriteRenderer.color = initialColor; 
            }
            else
            {
                Debug.LogError($"Color with html string {colorHtml} not found");
                yield break;
            }
        }
    }
}