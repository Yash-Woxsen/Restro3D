using Gameplay.RestroResources;
using UnityEngine;


namespace Gameplay.Customer
{
    public class ResetCustomer : MonoBehaviour
    {
        Customer _customer;
        LeaveRestaurant _leaveRestaurant;

        private void Start()
        {
            _customer = GetComponent<Customer>();
            _leaveRestaurant = GetComponent<LeaveRestaurant>();

            _leaveRestaurant.OnCustomerOpsFinished += ResetTheCustomer;

        }
        void ResetTheCustomer()
        {
            _customer.customerPool.ReturnCustomerToPool(this.gameObject);
        }
    }
}