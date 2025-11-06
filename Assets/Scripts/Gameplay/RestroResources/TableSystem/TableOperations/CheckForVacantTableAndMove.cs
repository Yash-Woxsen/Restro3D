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
            if (_vacantTable == null)
            {
                return;
            }
            _vacantTable.ReserveTheTable();
            _customer.SetTable(_vacantTable);


            StartCoroutine(MoveCustomerToTable(_vacantTable, timeTakenToReachTheTable));
        }

        private IEnumerator MoveCustomerToTable(Table vacantTable, float duration)
        {
            _customer.GetCurrentQueueSlot()?.VacateTheSlot();
            _customer.SetCurrentQueueSlot(null);

            isCheckedForVacantTableAfterOrdering = false;

            // Tell the customer behind to move ahead and order
            _customer.customerPool.CustomerOrderCompletedAndGoingToTable();

            Vector3 startPos = transform.position;
            Vector3 endPos = vacantTable.transform.position;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float t = elapsed / duration;

                // Move smoothly
                transform.position = Vector3.Lerp(startPos, endPos, t);

                // Face toward the target while walking
                Vector3 direction = (endPos - transform.position).normalized;
                if (direction.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Snap exactly to final position
            transform.position = endPos;

            // ✅ Face opposite to table's forward direction
            Quaternion oppositeToTable = Quaternion.LookRotation(-vacantTable.transform.forward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, oppositeToTable, 1f);

            OnCustomerSeated?.Invoke();
        }


    }
}