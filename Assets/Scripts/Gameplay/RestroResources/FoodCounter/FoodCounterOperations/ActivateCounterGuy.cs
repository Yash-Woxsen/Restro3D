using Gameplay.Customer;
using UnityEngine;

namespace Gameplay.RestroResources.FoodCounter.FoodCounterOperations
{
    public class ActivateCounterGuy : MonoBehaviour
    {
        public CustomerPool customerPool;
        public GameObject counterGuy;

        public void PopupUpgradeUi()
        {
            Activate();
        }


        void Activate()
        {
            counterGuy.SetActive(true);
            customerPool.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}