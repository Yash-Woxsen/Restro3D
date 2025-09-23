using UnityEngine;

namespace Gameplay.Customer
{
    public class QueueManager : MonoBehaviour
    {
        public static QueueManager instance;
        public QueueSlot[] queueSlots; // Array of queue slots

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            queueSlots = GetComponentsInChildren<QueueSlot>();
        }

        // Find the first available slot in the queue
        public QueueSlot GetAvailableSlot()
        {
            foreach (var slot in queueSlots)
            {
                if (!slot.isOccupied)
                {
                    return slot;
                }
            }
            return null; // No available slots
        }
    }
}