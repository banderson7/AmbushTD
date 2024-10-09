using System;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool isOpen;
    public bool towerNode;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        _renderer.material.color = Color.gray;
    }

    public bool IsTowerPlaceable()
    {
        return isOpen && towerNode;
    }

    public void SetTower(Tower tower)
    {
        isOpen = false;
        GameObject towerGO = Instantiate(tower.towerStats.body, transform.position + Vector3.up, Quaternion.identity);
        var newTower = towerGO.GetComponent<Tower>();
        newTower.Setup(tower);
    }

    public void Highlight()
    {
        _renderer.material.color = Color.white;
    }

    public void Unhighlight()
    {
        _renderer.material.color = Color.gray;
    }
}
