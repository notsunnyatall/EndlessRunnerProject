using EndlessRunner.Attributes;
using UnityEngine;

namespace EndlessRunner.UI
{
    public class HealthPointsUI : MonoBehaviour
    {
        [SerializeField] HeartUI heartPrefab;
        [SerializeField] Transform heartContainer;
        Health playerHealth;

        void OnEnable()
        {
            playerHealth.onDamageTaken.AddListener(Refresh);
            playerHealth.onDie.AddListener(Refresh);
        }

        void OnDisable()
        {
            playerHealth.onDamageTaken.RemoveListener(Refresh);
            playerHealth.onDie.AddListener(Refresh);
        }

        void Awake()
        {
            playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        void Start()
        {
            DrawHearts();
        }

        void DrawHearts()
        {
            foreach(Transform heart in heartContainer)
            {
                Destroy(heart.gameObject);
            }

            for(int i = 0; i < playerHealth.GetMaxHealthPoints(); i++)
            {
                Instantiate(heartPrefab, heartContainer);
            }
        }

        void Refresh()
        {
            for(int i = 0; i < playerHealth.GetMaxHealthPoints(); i++)
            {
                HeartUI currentHeart = heartContainer.GetChild(i).GetComponent<HeartUI>();

                if(i < playerHealth.GetCurrentHealthPoints())
                {
                    currentHeart.Fill();
                }
                else
                {
                    currentHeart.Empty();
                }
            }
        }
    }
}