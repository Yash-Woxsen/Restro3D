using UnityEngine;

namespace Gameplay.RestroResources.QueueSystem
{
    public class QueueManager : MonoBehaviour
    {
        public QueueSlot[] queueSlots;

        private void Start()
        {
            queueSlots = GetComponentsInChildren<QueueSlot>();
        }

    }
}