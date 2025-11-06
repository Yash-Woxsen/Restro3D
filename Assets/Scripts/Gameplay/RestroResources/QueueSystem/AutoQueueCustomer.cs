using Gameplay.Customer;
using UnityEngine;

namespace Gameplay.RestroResources.QueueSystem
{
    public class AutoQueueCustomer : MonoBehaviour
    {
        private CustomerPool customerPool;

        [Header("Spawn Settings")]
        public float initialDelay = 2f;
        public float invokingTime = 10f;

        private void OnEnable()
        {
            customerPool = GetComponent<CustomerPool>();
            if (customerPool == null)
            {
                Debug.LogError($"[AutoQueueCustomer] Missing CustomerPool on {name}");
                enabled = false;
                return;
            }

            StartAutoQueue();
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(EnableACustomer));
        }

        private void EnableACustomer()
        {
            customerPool.GetANewCustomer();
        }

        // 🧩 Starts the repeating invoke
        private void StartAutoQueue()
        {
            CancelInvoke(nameof(EnableACustomer));
            InvokeRepeating(nameof(EnableACustomer), initialDelay, invokingTime);
        }
        public void SetFrequency(float newTime)
        {
            invokingTime = newTime;
            StartAutoQueue();
        }
    }
}
