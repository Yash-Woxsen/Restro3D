using UnityEngine;
using System.Collections;

namespace Gameplay.Customer
{
    public class Customer : MonoBehaviour
    {
        public QueueSlot assignedSlot;
        private void OnEnable()
        {
            assignedSlot = QueueManager.instance.GetAvailableSlot();
            StartCoroutine(JoinQueue(assignedSlot.transform, 3f));
        }
        private IEnumerator JoinQueue(Transform slot, float speed)
        {
            assignedSlot.isOccupied = true;
            while (Vector3.Distance(transform.position, slot.position) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    slot.position,
                    speed * Time.deltaTime
                );
                yield return null; // wait until next frame
            }

            // Snap exactly to slot
            transform.position = slot.position;
            Debug.Log("Reached queue slot!");
        
        }
        public void MoveFwdToCounter()
        {
            if(reachedCounter)
            {
                BuyFood();
            }
            else
            {
                //move towards counter
            }
        }

        bool reachedCounter = false;
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("FoodCounter"))
            {
                reachedCounter = true;
            }
        }
    
        public void BuyFood()
        {
            // Buy food from counter and pay for it
        }
        public void CheckForVacantTable()
        {
            // Check for vacant table and move to it
        }
        public void SitAtTableAndEat()
        {
            //sit at table and eat food
        }
        public void PayTipAndRate()
        {
            // Pay tip and rate the service
        }
        public void ExitRestaurant()
        {
            //move towards exit
            gameObject.SetActive(false);
            transform.localPosition = Vector3.zero;
        }
        private void OnDisable()
        {
            assignedSlot.isOccupied = false;
        }
    }
}
