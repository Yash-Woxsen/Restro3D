using DATA;
using UnityEngine;
namespace Gameplay.RestroResources.FoodCounter.FoodCounterOperations
{
    public class PayMoney : MonoBehaviour
    {
        public int amountToPay = 10;

        void OnEnable()
        {
            var OrderFood = GetComponent<OrderFood>();
            OrderFood.OnOrderTakenSuccessfully += PayMoneyToRestaurant;
        }
        void OnDisable()
        {
            var OrderFood = GetComponent<OrderFood>();
            OrderFood.OnOrderTakenSuccessfully -= PayMoneyToRestaurant;
        }
        public void PayMoneyToRestaurant()
        {
            // Implement payment logic here
            EconomySO.Instance.AddMoney(amountToPay);
        }
    }
}
