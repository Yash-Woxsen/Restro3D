using UnityEngine;

namespace DATA
{
    [CreateAssetMenu(fileName = "EconomySO", menuName = "GameDATA/EconomySO")]
    public class EconomySO : ScriptableObject
    {

        private static EconomySO _instance;
        public static EconomySO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Resources.Load<EconomySO>("GameDATA/GameEconomy");
                return _instance;
            }
        }

        public void DeductMoney(int amount)
        {
            mainMoney -= amount;
        }
        public void AddMoney(int amount)
        {
            mainMoney += amount;
        }

        public void SetMainMoney(int amount)
        {
            mainMoney = amount;
        }   

 
        public int mainMoney;
        [Space(10)]
        [Header("Store Costs")]
        public int depositAmount;
        public int rentPerMonth;

        [Space(10)]
        [Header("Employee Costs")]
        public int salaryPerEmployee;
        public int salaryOfStoreManager;

        [Space(10)]
        [Header("Customer Related Costs")]
        public int costOfOneBurger;
        public int tipAmountPerCustomer;

        [Space(10)]
        [Header("Table Related Costs")]
        public int costOfBasicTable;
        public int firstUpgradeCostOfTable;
        public int secondUpgradeCostOfTable;
    }
}