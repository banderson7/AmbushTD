using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    public bool slows;

    public float speed = 10f;
    public int damage;

    public void Shoot(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;
        var distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        
    }

    private void HitTarget()
    {
        var targetHealth = _target.GetComponent<UnitHealth>();
        targetHealth.TakeDamage(damage, transform.position);
        if (slows)
        {
            var targetMovement = _target.GetComponent <UnitMovement>();
            targetMovement.Slow();
        }
        Destroy(gameObject);
    }
}
