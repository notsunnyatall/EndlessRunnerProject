using EndlessRunner.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessRunner.UI
{
    public class ManaUI : MonoBehaviour
    {
        [SerializeField] Image foreground;
        Mana playerMana;

        void Awake()
        {
            playerMana = GameObject.FindWithTag("Player").GetComponent<Mana>();
        }

        void Update()
        {
            foreground.fillAmount = playerMana.GetManaFraction();
        }
    }
}