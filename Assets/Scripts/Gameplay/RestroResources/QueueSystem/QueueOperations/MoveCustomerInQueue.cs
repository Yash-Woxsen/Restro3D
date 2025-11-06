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
        private IEnumerator MoveCustomer(QueueSlot currentSlot, QueueSlot aheadSlot, float duration)
        {
            _customer.GetCurrentQueueSlot().VacateTheSlot();
            _customer.SetCurrentQueueSlot(aheadSlot);

            float elapsed = 0f;

            while (elapsed < duration)
            {
                float t = elapsed / duration;

                // Move smoothly between slots
                transform.position = Vector3.Lerp(currentSlot.transform.position, aheadSlot.transform.position, t);

                // Face the direction of movement
                Vector3 direction = (aheadSlot.transform.position - transform.position).normalized;
                if (direction.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Snap exactly to final position and rotation
            transform.position = aheadSlot.transform.position;
            Vector3 finalDir = (aheadSlot.transform.position - currentSlot.transform.position).normalized;
            if (finalDir.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(finalDir, Vector3.up);

            _customer.InvokeFunctionToInvokeThisOnReachingTheQueuePosition();
        }

    }
}