using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerStats towerStats;
    public Transform target;
    public Transform rotationBase;
    private float _fireCooldown = 0f;
    private const float TurnSpeed = 10.0f;
    public int cost;
    public Transform firePoint;
    
    private void Start()
    {
        InvokeRepeating("FindTarget", 0, 0.25f);
    }

    public void Setup(TowerStats stats)
    {
        towerStats = stats;
    }

    private void FindTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (!(distanceToEnemy < shortestDistance)) continue;
            
            shortestDistance = distanceToEnemy;
            nearestEnemy = enemy;
        }

        if (nearestEnemy != null && shortestDistance <= towerStats.range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotationBase.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        rotationBase.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (_fireCooldown <= 0)
        {
            Shoot();
            _fireCooldown = towerStats.fireRate;
        }

        _fireCooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(towerStats.bullet, firePoint.position, firePoint.rotation);
        var bullet = newBullet.GetComponent<Bullet>();
        
        if (bullet != null)
            bullet.Shoot(target);
    }
}
