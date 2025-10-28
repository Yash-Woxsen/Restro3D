using System.Collections;
using UnityEngine;
namespace Gameplay.RestroResources.TableSystem.TableOperations
{
    public class EatFood : MonoBehaviour
    {
        CheckForVacantTableAndMove _checkForVacantTableAndMove;
        public event System.Action OnFoodEaten;

        void OnEnable()
        {
            _checkForVacantTableAndMove = GetComponent<CheckForVacantTableAndMove>();
            _checkForVacantTableAndMove.OnCustomerSeated += StartEatingFood;
        }
        IEnumerator EatTheFood()
        {
            yield return new WaitForSeconds(5f);
            OnFoodEaten?.Invoke();
            _checkForVacantTableAndMove.OnCustomerSeated -= StartEatingFood;
        }

        public void StartEatingFood()
        {
            StartCoroutine(EatTheFood());
        }
    }
}