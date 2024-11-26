using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EndlessRunner.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad;
        [SerializeField] float fadeDelay = 3;
        [SerializeField] float fadeOutTime = 2;
        [SerializeField] float fadeInTime = 2;
        [SerializeField] float fadeWaitTime = 1;

        public void Teleport()
        {
            StartCoroutine(LoadSceneRoutine());
        }

        IEnumerator LoadSceneRoutine()
        {
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return new WaitForSeconds(fadeDelay);

            yield return fader.FadeOut(fadeOutTime);

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            yield return new WaitForSeconds(fadeWaitTime);

            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }
    }
}