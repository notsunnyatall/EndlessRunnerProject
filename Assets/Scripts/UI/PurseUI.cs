using EndlessRunner.Inventories;
using TMPro;
using UnityEngine;

namespace EndlessRunner.UI
{
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] Purse purse;
        TMP_Text balanceText;

        void Awake()
        {
            balanceText = GetComponent<TMP_Text>();
        }

        void Update()
        {
            balanceText.text = $"Coins:{purse.GetBalance()}";
        }
    }
}
