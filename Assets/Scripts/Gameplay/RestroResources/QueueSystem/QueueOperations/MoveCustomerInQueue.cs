using System.Collections;
using UnityEngine;

namespace Gameplay.RestroResources.QueueSystem.QueueOperations
{
    public class MoveCustomerInQueue : MonoBehaviour
    {
        Gameplay.Customer.Customer _customer;

        private void OnEnable()
        {
            _customer = GetComponent<Gameplay.Customer.Customer>();
            _customer.InvokeThisOnReachingTheQueuePosition += CheckAndMoveToNextSlot;
        }

        void CheckAndMoveToNextSlot()
        {
            if (_customer.GetAheadQueueSlot() != null && _customer.GetAheadQueueSlot().CheckSlotStatus())
            {
                StartCoroutine(MoveCustomer(_customer.GetCurrentQueueSlot(), _customer.GetAheadQueueSlot(), 1));

                //Info Behind customer
            }

            else
            {
                _customer.InvokeThisOnReachingTheQueuePosition -= CheckAndMoveToNextSlot;
                _customer = null;
            }
        }
        IEnumerator MoveCustomer(QueueSlot currentSlot, QueueSlot aheadSlot, float duration)
        {
            _customer.GetCurrentQueueSlot().VacateTheSlot();
            _customer.SetCurrentQueueSlot(aheadSlot);

            float elapsed = 0f;

            while (elapsed < duration)
            {
                // Interpolate position based on elapsed time
                transform.position = Vector3.Lerp(currentSlot.transform.position, aheadSlot.transform.position, elapsed / duration);

                elapsed += Time.deltaTime;
                yield return null; // wait for next frame
            }

            // Snap exactly to the target position
            transform.position = aheadSlot.transform.position;

            _customer.InvokeFunctionToInvokeThisOnReachingTheQueuePosition();
        }
    }
}