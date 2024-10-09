using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MoneyUI : MonoBehaviour
    {
        public Text text;

        private void Awake()
        {
            Money.OnMoneyChange += UpdateMoneyDisplayText;
        }

        private void UpdateMoneyDisplayText(int newCurrent)
        {
            text.text = newCurrent.ToString();
        }

        private void OnDisable()
        {
            Money.OnMoneyChange -= UpdateMoneyDisplayText;
        }
    }
}