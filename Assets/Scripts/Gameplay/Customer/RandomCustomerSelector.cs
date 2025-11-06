using UnityEngine;

namespace Gameplay.Customer
{
    public class RandomCustomerSelector : MonoBehaviour
    {
        public GameObject[] availableCustomerPrefabs;
        public GameObject selectedCustomerMesh;

        public CustomerMovementAnimator customerMovementAnimator;

        private void OnEnable()
        {
            SetCustomerOnEnable();
            customerMovementAnimator.gameObject.SetActive(true);
        }

        void SetCustomerOnEnable()
        {
            foreach(GameObject customer in availableCustomerPrefabs)
            {
                customer.SetActive(false);
            }
            int randomIndex = Random.Range(0, availableCustomerPrefabs.Length);
            availableCustomerPrefabs[randomIndex].SetActive(true);
            selectedCustomerMesh = availableCustomerPrefabs[randomIndex];
        }
        void ResetCustomerOnDisable()
        {
            foreach (GameObject customer in availableCustomerPrefabs)
            {
                customer.SetActive(false);
            }
        }

        private void OnDisable()
        {
            ResetCustomerOnDisable();
            customerMovementAnimator.gameObject.SetActive(false);
        }
    }
}