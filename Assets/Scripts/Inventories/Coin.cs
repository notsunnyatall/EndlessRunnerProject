using UnityEngine;

namespace EndlessRunner.Inventories
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] int currencyAmount = 1;

        void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Purse purse))
            {
                purse.Deposit(currencyAmount);

                Destroy(gameObject);
            }
        }
    }
}
