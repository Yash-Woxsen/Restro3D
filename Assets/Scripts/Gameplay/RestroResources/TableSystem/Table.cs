using UnityEngine;

namespace Gameplay.RestroResources.TableSystem
{
    public class Table : MonoBehaviour
    {
        bool _isVacant = true;

        public bool CheckTableStatus() { return _isVacant; }
        public void ReserveTheTable() { _isVacant = false; }
        public void VacateTheTable() { _isVacant = true; }
    }
}
