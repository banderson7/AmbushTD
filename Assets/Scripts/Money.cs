using UnityEngine;

public class Money : MonoBehaviour
{
    public int currentMoney;

    public delegate void OnMoneyChangeDelegate(int newAmount);
    public static event OnMoneyChangeDelegate OnMoneyChange;
    
    
    public void SpendMoney(int amount)
    {
        var newAmount = currentMoney - amount;
        if (newAmount <= 0) newAmount = 0;
        currentMoney = newAmount;
        OnMoneyChange?.Invoke(currentMoney);
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        OnMoneyChange?.Invoke(currentMoney);
    }
}
