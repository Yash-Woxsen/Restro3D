using System;
using UnityEngine;

namespace Gameplay.RestroResources.FoodCounter.FoodCounterOperations
{
    public class OrderFood : MonoBehaviour
    {
        public int foodAmount;
        Gameplay.Customer.Customer _customer;
        FoodCounter _foodCounter;

        public event Action OnOrderTakenSuccessfully;

        void Start()
        {
            _customer = GetComponentInParent<Gameplay.Customer.Customer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("FoodCounter"))
            {
                _foodCounter = other.GetComponent<FoodCounter>();
                _foodCounter.OnOrderProcessed += OrderTakenSuccessfully;
                if (_foodCounter != null && foodAmount > 0)
                {
                    _foodCounter.GiveOrder(_customer);
                }
            }
        }
        public void OrderTakenSuccessfully()
        {
            OnOrderTakenSuccessfully?.Invoke();
            _foodCounter.OnOrderProcessed -= OrderTakenSuccessfully;
        }
    }
}