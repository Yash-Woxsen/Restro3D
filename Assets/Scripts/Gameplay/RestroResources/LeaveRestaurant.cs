using Gameplay.RestroResources.TableSystem.TableOperations;
using System.Collections;
using UnityEngine;

namespace Gameplay.RestroResources
{
    public class LeaveRestaurant : MonoBehaviour
    {
        Customer.Customer _customer;
        EatFood _eatFood;
        public event System.Action OnCustomerOpsFinished;
        void OnEnable()
        {
            _customer = GetComponent<Customer.Customer>();
            _eatFood = GetComponent<EatFood>();
            _eatFood.OnFoodEaten += ResetCustomer;
        }
        public void ResetCustomer()
        {
            StartCoroutine(WalkOut());
        }
        void ClearTable()
        {
            _customer.GetTable()?.VacateTheTable();
            _customer.customerPool.tableManager.TableVacatedByCustomer();
            _customer.SetTable(null);
        }

        private IEnumerator WalkOut(float speed = 2f)
        {
            ClearTable();
            Vector3 targetLocalPos = Vector3.zero;

            while (Vector3.Distance(transform.localPosition, targetLocalPos) > 0.01f)
            {
                // Face toward the target
                Vector3 direction = (targetLocalPos - transform.localPosition).normalized;
                if (direction.sqrMagnitude > 0.001f)
                    transform.localRotation = Quaternion.LookRotation(direction, Vector3.up);

                // Move toward the target
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetLocalPos, speed * Time.deltaTime);

                yield return null;
            }

            transform.localPosition = targetLocalPos;
            OnCustomerOpsFinished?.Invoke();
        }

    }
}
