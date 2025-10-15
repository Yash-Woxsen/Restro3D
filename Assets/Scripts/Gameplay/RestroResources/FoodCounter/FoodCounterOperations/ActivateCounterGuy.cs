using Gameplay.Customer;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.RestroResources.FoodCounter.FoodCounterOperations
{
    public class ActivateCounterGuy : MonoBehaviour
    {
        public CustomerPool customerPool;
        public GameObject counterGuy;
        Camera mainCam;

        private void Start()
        {
            mainCam = Camera.main;
        }

        void Update()
        {
            // Mouse click (left button)
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                CheckRaycast(mousePosition);
            }

            // Touch input (first finger)
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                CheckRaycast(touchPosition);
            }
        }

        private void CheckRaycast(Vector2 screenPosition)
        {
            Ray ray = mainCam.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("ActivateCustomerCounter"))
                {
                    Activate();
                }
            }

        }


        public void Activate()
        {
            counterGuy.SetActive(true);
            customerPool.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}