using UnityEngine;
using System.Collections.Generic;

namespace Gameplay.Customer
{
    public class CustomerPool : MonoBehaviour
    {
        [Header("Pool Settings")]
        public GameObject customerPrefab;   // The customer prefab
        public int poolSize = 50;           // Initial pool size

        private List<GameObject> _pool;

        void Awake()
        {
            // Initialize the pool
            _pool = new List<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(customerPrefab, transform);
                obj.SetActive(false);
                _pool.Add(obj);
            }
        }

        // Get an inactive customer from the pool
        public GameObject GetCustomer()
        {
            foreach (var obj in _pool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            // No inactive customer found
            Debug.Log("No more customers available in the pool!");
            return null;
        }
        //public void UpdateCustomerPosition()
        //{
        //    foreach (var obj in pool)
        //    {
        //        if (obj.activeInHierarchy)
        //        {
        //            obj
        //        }
        //    }
        //}

        public void ResetPool()
        {
            foreach (var obj in _pool)
            {
                obj.SetActive(false);
                obj.transform.localPosition = Vector3.zero;
            }
        }
    }
}