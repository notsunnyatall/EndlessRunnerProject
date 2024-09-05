using UnityEngine;
using UnityEngine.Events;

namespace EndlessRunner.Inventories
{
    public class Purse : MonoBehaviour
    {
        [SerializeField] int initialBalance = 0;
        int balance = 0;

        public int GetBalance()
        {
            return balance;
        }

        public void Deposit(int amount)
        {
            balance += amount;
        }

        void Awake()
        {
            balance = initialBalance;
        }
    }
}