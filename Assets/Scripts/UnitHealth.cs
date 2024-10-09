using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public GameObject hitEffect;
    public int worth;
    private Money _money;
    public Image healthBar;
    public bool boss;

    private void Start()
    {
        healthBar.fillAmount = 1;
    }

    public void Setup(Money money, int level)
    {
        _money = money;
        maxHealth += level * 100;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount, Vector3 hitPos)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;
        healthBar.fillAmount = (float)currentHealth / maxHealth;
        GameObject effect = Instantiate(hitEffect, hitPos, Quaternion.identity);
        Destroy(effect, 2.0f);

        if (currentHealth <= 0)
        {
            if (boss)
            {
                var winScreen = GameObject.FindGameObjectWithTag("WinScreen");
                var winCanvas = winScreen.GetComponent<CanvasGroup>();
                winCanvas.alpha = 1;
                winCanvas.interactable = true;
                winCanvas.blocksRaycasts = true;
            }
            _money.AddMoney(worth);
            Destroy(gameObject);
        }
    }
}