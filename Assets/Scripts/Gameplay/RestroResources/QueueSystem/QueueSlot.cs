using UnityEngine;

namespace Gameplay.RestroResources.QueueSystem
{
    public class QueueSlot : MonoBehaviour
    {
        bool _isVacant = true;
        public bool CheckSlotStatus() { return _isVacant; }
        public void ReserveTheSlot() { _isVacant = false; }
        public void VacateTheSlot() { _isVacant = true; }
    }
}