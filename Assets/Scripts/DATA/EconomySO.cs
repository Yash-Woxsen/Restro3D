using UnityEngine;

namespace DATA
{
    [CreateAssetMenu(fileName = "EconomySO", menuName = "GameDATA/EconomySO")]
    public class EconomySO : ScriptableObject
    {
        public int mainMoney;
        [Header("Spending")]
        public int rentOfRestaurant;
        public int electricityAndMiscExpenses;
        public int costOfEachNewCounter;
        public int costOfEachNewTable;
        public int costOfEachNewFoodMachine;
        [Space(10)]
        public int salaryOfEachServent;
        public int bonusMultiplierFOrServents;

    }
}