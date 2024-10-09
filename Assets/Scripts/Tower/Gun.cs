using UnityEngine;

[CreateAssetMenu(fileName = "TowerStats", menuName = "TD/TowerStats", order = 0)]
public class TowerStats : ScriptableObject
{
    public float range;
    public float fireRate;
    public GameObject body;
    public GameObject bullet;
    public int cost;
}