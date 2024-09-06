using EndlessRunner.Inventories;
using TMPro;
using UnityEngine;

namespace EndlessRunner.UI
{
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] TMP_Text balanceText;
        Purse playerPurse;

        void OnEnable()
        {
            playerPurse.purseUpdated.AddListener(Refresh);
        }

        void OnDisable()
        {
            playerPurse.purseUpdated.AddListener(Refresh);
        }

        void Awake()
        {
            playerPurse = GameObject.FindWithTag("Player").GetComponent<Purse>();
        }

        void Start()
        {
            Refresh();
        }

        void Refresh()
        {
            balanceText.text = $"${playerPurse.GetBalance()}";
        }
    }
}
