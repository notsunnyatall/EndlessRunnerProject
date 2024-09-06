using UnityEngine;

namespace EndlessRunner.UI
{
    public class HeartUI : MonoBehaviour
    {
        [SerializeField] GameObject foreground;

        public void Fill()
        {
            foreground.SetActive(true);
        }

        public void Empty()
        {
            foreground.SetActive(false);
        }
    }
}