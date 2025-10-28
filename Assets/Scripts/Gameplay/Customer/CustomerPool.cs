using Gameplay.RestroResources.QueueSystem;
using Gameplay.RestroResources.TableSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Customer
{
    public class CustomerPool : MonoBehaviour
    {

        [Header("Pool Settings")]
        public GameObject customerPrefab;
        public int initialPoolSize = 10;

        private Queue<GameObject> pool = new Queue<GameObject>();

        public QueueManager queueManager;

        public TableManager tableManager;

        public event System.Action InvokeThisOnOrderingCompletedAndGointToTable;

        private void Awake()
        {
            InstantiateThePoolOfCustomers();
        }

        void InstantiateThePoolOfCustomers()
        {
            // Initialize pool
            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject customer = Instantiate(customerPrefab, transform);
                customer.name = "Customer_" + (i + 1);  // Sequential naming
                customer.SetActive(false);
                pool.Enqueue(customer);
            }
        }

        public Customer GetANewCustomer()
        {
            if (pool.Count == 0)
            {
                Debug.LogWarning("Pool is empty! Consider increasing initialPoolSize or handling this case.");
                return null;
            }

            GameObject customerGO = pool.Dequeue();
            customerGO.SetActive(true);

            // Assuming your Customer script is attached to the GameObject
            Customer customerComponent = customerGO.GetComponent<Customer>();
            return customerComponent;
        }

        public void ReturnCustomerToPool(GameObject customer)
        {
            customer.SetActive(false);
            pool.Enqueue(customer);
        }

        public void CustomerOrderCompletedAndGoingToTable()
        {
            InvokeThisOnOrderingCompletedAndGointToTable?.Invoke();
        }

    }
}