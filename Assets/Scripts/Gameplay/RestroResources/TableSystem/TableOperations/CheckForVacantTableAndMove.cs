using Gameplay.RestroResources.FoodCounter.FoodCounterOperations;
using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.RestroResources.TableSystem.TableOperations
{
    public class CheckForVacantTableAndMove : MonoBehaviour
    {
        public float timeTakenToReachTheTable = 5f;
        Table _vacantTable;
        Customer.Customer _customer;
        OrderFood _orderFood;

        public event Action OnCustomerSeated;


        private void Start()
        {
            _customer = GetComponent<Customer.Customer>();
            _orderFood = GetComponent<OrderFood>();
            _orderFood.OnOrderTakenSuccessfully += CheckForVacantTable;
            _customer.customerPool.tableManager.OnTableVacatedByCustomer += IfTableVacatedByAnyCustomer;
        }

        bool isCheckedForVacantTableAfterOrdering = false;
        void IfTableVacatedByAnyCustomer()
        {
            if (isCheckedForVacantTableAfterOrdering == true)
            {
                CheckForVacantTable();
            }
            else
            {
                return;
            }
        }

        void CheckForVacantTable()
        {
            isCheckedForVacantTableAfterOrdering = true;


            _vacantTable = _customer.customerPool.tableManager.GetVacantTable();
            if(_vacantTable == null)
            {
                return;
            }
            _vacantTable.ReserveTheTable();
            _customer.SetTable(_vacantTable);


            StartCoroutine(MoveCustomerToTable(_vacantTable, timeTakenToReachTheTable));
        }

        IEnumerator MoveCustomerToTable(Table vacantTable, float duration)
        {
            _customer.GetCurrentQueueSlot()?.VacateTheSlot();
            _customer.SetCurrentQueueSlot(null);

            isCheckedForVacantTableAfterOrdering = false;

            //Tell customer behind to come ahead and order food due to high waiting time because table was not available
            _customer.customerPool.CustomerOrderCompletedAndGoingToTable();

            Vector3 startPos = transform.position;  // Store start once
            Vector3 endPos = vacantTable.transform.position;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float t = elapsed / duration;  // normalized time 0 -> 1
                transform.position = Vector3.Lerp(startPos, endPos, t);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos;  // Snap exactly to the end position

            OnCustomerSeated?.Invoke();
        }

    }
}