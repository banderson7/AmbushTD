using System.Collections;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public Camera cam;
    private Node _previousNode;
    public GameObject towerBase;
    private Tower _towerToPlace;
    private Money _money;

    private void Awake()
    {
        _money = GetComponent<Money>();
    }

    public void SelectTower(Tower tower)
    {
        if (_money.currentMoney < tower.towerStats.cost) return;
        
        StartCoroutine(SelectNode());
        _towerToPlace = tower;
    }

    private IEnumerator SelectNode()
    {
        while (true)
        {
            Node currentNode = MouseHovering();
            if (currentNode != _previousNode)
            {
                if (_previousNode != null)
                {
                    _previousNode.Unhighlight();
                }
                if (currentNode != null)
                {
                    if (currentNode.IsTowerPlaceable())
                    {
                        currentNode.Highlight();
                    }
                }
            }

            _previousNode = currentNode;
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                if (currentNode != null)
                {
                    currentNode.Unhighlight();
                    break;
                }
            }
            if (Input.GetMouseButtonDown(0) && currentNode != null)
            {
                if (currentNode.IsTowerPlaceable())
                {
                    currentNode.Unhighlight();
                    currentNode.SetTower(_towerToPlace);
                    _money.SpendMoney(_towerToPlace.GetComponent<Tower>().cost);
                    break;
                }
            }
            yield return null;
        }
    }
    
    private Node MouseHovering()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 35.0f);
        return hit.collider != null ? hit.collider.GetComponent<Node>() : null;
    }
}
