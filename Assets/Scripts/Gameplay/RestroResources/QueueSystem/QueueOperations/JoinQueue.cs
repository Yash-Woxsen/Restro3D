using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.RestroResources.QueueSystem.QueueOperations
{
    public class JoinQueue : MonoBehaviour
    {

        Gameplay.Customer.Customer _customer;
        public float timeTakenToJoinQueue;

        public event Action OnJoiningTheQueue;

        public void CheckAndGetLastSlotOfQueueAndJoinTheQueue()
        {
            _customer = gameObject.GetComponent<Gameplay.Customer.Customer>();
            var lastQueueSlot = _customer.customerPool.queueManager.queueSlots[_customer.customerPool.queueManager.queueSlots.Length - 1].CheckSlotStatus();
            if (lastQueueSlot)
            {
                StartCoroutine(JustJoinQueue(_customer.customerPool.queueManager.queueSlots[_customer.customerPool.queueManager.queueSlots.Length - 1], timeTakenToJoinQueue));
            }
            else
            {
                gameObject.SetActive(false);
                _customer = null;
            }
        }


        private IEnumerator JustJoinQueue(QueueSlot targetSlot, float duration)
        {
            _customer.SetCurrentQueueSlot(targetSlot);
            if (targetSlot == null)
                yield break;

            Vector3 startPos = transform.position;
            Vector3 endPos = targetSlot.transform.position;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float t = elapsed / duration;

                // Smooth position movement
                transform.position = Vector3.Lerp(startPos, endPos, t);

                // Face toward the target
                Vector3 direction = (endPos - transform.position).normalized;
                if (direction.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Snap exactly to the final position & rotation
            transform.position = endPos;

            Vector3 finalDir = (endPos - startPos).normalized;
            if (finalDir.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(finalDir, Vector3.up);

            OnJoiningTheQueue?.Invoke();
            _customer.InvokeFunctionToInvokeThisOnReachingTheQueuePosition();
        }
    }
}