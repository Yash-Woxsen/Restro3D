using UnityEngine;

namespace Gameplay.RestroResources.TableSystem
{
    public class TableManager : MonoBehaviour
    {
        public Table[] tables;

        public event System.Action OnTableVacatedByCustomer;

        private void Awake()
        {
            tables = GetComponentsInChildren<Table>();
        }
        public Table GetVacantTable()
        {
            foreach (var table in tables)
            {
                if (table.CheckTableStatus())
                {
                    return table;
                }
            }
            // If no vacant table is found, return null
            return null;
        }

        public void TableVacatedByCustomer()
        {
            OnTableVacatedByCustomer?.Invoke();
        }
    }
}