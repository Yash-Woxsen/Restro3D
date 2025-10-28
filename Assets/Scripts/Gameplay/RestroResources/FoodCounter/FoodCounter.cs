using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.RestroResources.FoodCounter
{
    public class FoodCounter : MonoBehaviour
    {
        public float orderProcessingTime = 2f;
        public event Action OnOrderProcessed;
        IEnumerator ProcessOrder(Customer.Customer customer)
        {
            yield return new WaitForSeconds(orderProcessingTime);
            OnOrderProcessed?.Invoke();
        }

        public void GiveOrder(Customer.Customer customer)
        {
            StartCoroutine(ProcessOrder(customer));
        }
    }
}