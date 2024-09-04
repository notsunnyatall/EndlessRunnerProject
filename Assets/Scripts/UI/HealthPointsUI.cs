using EndlessRunner.Attributes;
using TMPro;
using UnityEngine;

namespace EndlessRunner.UI
{
    public class HealthPointsUI : MonoBehaviour
    {
        [SerializeField] Health health;
        [SerializeField] TMP_Text healthPointsText;

        void Update()
        {
            healthPointsText.text = $"HP:{health.GetHealthPoints()}";
        }
    }
}