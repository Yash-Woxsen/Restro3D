using DATA;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UiDateHandler : MonoBehaviour
    {
        [SerializeField] TMP_Text moneyText;
        [TextArea(10, 30)] public string dataUpdateLogs = string.Empty;

        private void Update()
        {
            moneyText.text = "$ " + EconomySO.Instance.mainMoney.ToString("F0");
        }
    }
}