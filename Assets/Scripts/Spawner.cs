using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Money money;
    public void SpawnUnit(GameObject unitToSpawn, int level)
    {
        GameObject newUnit = Instantiate(unitToSpawn, transform.position, Quaternion.identity);
        var unitHealth = newUnit.GetComponent<UnitHealth>();
        unitHealth.Setup(money, level);
    }
}
