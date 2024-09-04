using EndlessRunner.Attributes;
using TMPro;
using UnityEngine;

namespace EndlessRunner.UI
{
    public class HealthPointsUI : MonoBehaviour
    {
        [SerializeField] Health health;
        TMP_Text healthPointsText;

        void Awake()
        {
            healthPointsText = GetComponent<TMP_Text>();
        }

        void Update()
        {
            healthPointsText.text = $"HP:{health.GetHealthPoints()}";
        }
    }
}