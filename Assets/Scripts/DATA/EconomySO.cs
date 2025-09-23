using UnityEngine;

[CreateAssetMenu(fileName = "EconomySO", menuName = "GameDATA/EconomySO")]
public class EconomySO : ScriptableObject
{
    public int MainMoney;
    [Header("Spending")]
    public int rentOfrestaurant;
    public int electricityAndMiscExpenses;
    public int costOfEachNewCounter;
    public int costOfEachNewTable;
    public int costOfEachNewFoodMachine;
    [Space(10)]
    public int salaryOfEachServent;
    public int bonusMultiplierFOrServents;

}
