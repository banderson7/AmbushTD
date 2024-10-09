using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "TD/Wave", order = 0)]
public class Wave : ScriptableObject
{
    public GameObject unit;
    public int unitsThisWave;
    public float timeBetweenSpawns;
}