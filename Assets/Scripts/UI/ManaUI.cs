using EndlessRunner.Attributes;
using UnityEngine;

namespace EndlessRunner.UI
{
    public class ManaUI : MonoBehaviour
    {
        [SerializeField] RectTransform foreground;
        Mana playerMana;

        void Awake()
        {
            playerMana = GameObject.FindWithTag("Player").GetComponent<Mana>();
        }

        void Update()
        {
            foreground.localScale = new Vector3(1, playerMana.GetManaFraction(), 1);
        }
    }
}