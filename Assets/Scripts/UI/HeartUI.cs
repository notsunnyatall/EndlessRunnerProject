using UnityEngine;

namespace EndlessRunner.UI
{
    public class HeartUI : MonoBehaviour
    {
        [SerializeField] GameObject foreGround;

        public void Fill()
        {
            foreGround.SetActive(true);
        }

        public void Empty()
        {
            foreGround.SetActive(false);
        }
    }
}