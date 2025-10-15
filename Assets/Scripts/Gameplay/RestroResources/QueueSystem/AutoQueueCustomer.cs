using Gameplay.Customer;
using UnityEngine;

namespace Gameplay.RestroResources.QueueSystem
{
    public class AutoQueueCustomer : MonoBehaviour
    {
        CustomerPool customerPool;
        private void OnEnable()
        {
            customerPool = GetComponent<CustomerPool>();
            InvokeRepeating(nameof(EnableACustomer), 2f, 10f);
        }

        void EnableACustomer()
        {
            var newCustomer = customerPool.GetANewCustomer();
        }
    }
}