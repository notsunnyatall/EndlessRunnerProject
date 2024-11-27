using System.Collections;
using EndlessRunner.Attributes;
using EndlessRunner.Control;
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

        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Teleport();
            }
        }

        IEnumerator LoadSceneRoutine()
        {
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            UpdatePlayerSequence(false);

            yield return new WaitForSeconds(fadeDelay);

            yield return fader.FadeOut(fadeOutTime);

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            yield return new WaitForSeconds(fadeWaitTime);

            UpdatePlayerSequence(true);

            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        void UpdatePlayerSequence(bool state)
        {
            GameObject player = GameObject.FindWithTag("Player");
            Health playerHealth = player.GetComponent<Health>();

            player.GetComponent<PlayerController>().enabled = state;
            
            if(!state)
            {
                playerHealth.SaveState();
            }
            else
            {
                playerHealth.LoadState();
            }
        }
    }
}