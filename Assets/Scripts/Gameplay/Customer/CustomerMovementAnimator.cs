using UnityEngine;

namespace Gameplay.Customer
{
    public class CustomerMovementAnimator : MonoBehaviour
    {
        public Animator customerAnimator;
        public RandomCustomerSelector randomCustomerSelector;
        void OnEnable()
        {
            customerAnimator = randomCustomerSelector.selectedCustomerMesh.GetComponent<Animator>();
        }
    }
}