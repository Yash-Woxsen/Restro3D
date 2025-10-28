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
                // Calculate normalized progress (0 to 1)
                float t = elapsed / duration;

                // Smoothly interpolate position
                transform.position = Vector3.Lerp(startPos, endPos, t);

                elapsed += Time.deltaTime;
                yield return null; // wait for next frame
            }

            // Snap exactly to the target position
            transform.position = endPos;

            OnJoiningTheQueue?.Invoke();
            _customer.InvokeFunctionToInvokeThisOnReachingTheQueuePosition();
        }
    }
}