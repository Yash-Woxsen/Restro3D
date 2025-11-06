using UnityEngine;

namespace Gameplay.EconomySystem
{
    public class MoneyDeduction : MonoBehaviour
    {
        public void DeductMoney(int amount)
        {
            if(DATA.EconomySO.Instance.mainMoney <= 0)
            {
                Debug.Log("Not enough money to deduct.");
                return;
            }
            DATA.EconomySO.Instance.DeductMoney(amount);
        }
    }
}