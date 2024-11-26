using System.Collections;
using UnityEngine;

namespace EndlessRunner.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        public Coroutine FadeOut(float time)
        {
            return StartCoroutine(FadeRoutine(1, time));
        }

        public Coroutine FadeIn(float time)
        {
            return StartCoroutine(FadeRoutine(0, time));
        }

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        IEnumerator FadeRoutine(float alphaTarget, float time)
        {
            while(!Mathf.Approximately(canvasGroup.alpha, alphaTarget))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, alphaTarget, Time.unscaledDeltaTime / time);
                yield return null;
            }
        }
    }
}
