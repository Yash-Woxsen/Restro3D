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


        public int mainMoney;


        public int depositAmount;
        public int rentPerMonth;


        public int salaryPerEmployee;
        public int salaryOfStoreManager;

        public int costOfOneBurger;
        public int tipAmountPerCustomer;

        public int costOfBasicTable;
        public int firstUpgradeCostOfTable;
        public int secondUpgradeCostOfTable;
    }
}