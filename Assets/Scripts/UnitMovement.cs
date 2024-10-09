using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public float baseSpeed;
    public float _currentSpeed;
    private float _timeSlowed;
    private int _waypointIndex = 0;
    private Transform _target;
    private bool _isSlowed;
    public float _speedMultiplier;
    public Transform body;
    private UnitHealth _unitHealth;
    
    public delegate void OnReachEndDelegate(bool isBoss);
    public static event OnReachEndDelegate OnReachEnd;

    private void Start()
    {
        _target = Waypoints.Points[0];
        transform.LookAt(_target);
        _unitHealth = GetComponent<UnitHealth>();
        _currentSpeed = baseSpeed;
        _speedMultiplier = 1.0f;
    }

    public void Slow()
    {
        _isSlowed = true;
        _timeSlowed = 0;
    }

    private void Update()
    {
        Vector3 dir = _target.position - transform.position;
        if (_isSlowed)
        {
            _timeSlowed += Time.deltaTime;
            if (_speedMultiplier == 1.0f)
            {
                _speedMultiplier = 0.6f;
            }
        }

        if (_timeSlowed >= 3)
        {
            _isSlowed = false;
            _speedMultiplier = 1;
        }
        transform.Translate(dir.normalized * (_currentSpeed * _speedMultiplier * Time.deltaTime), Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (_waypointIndex >= Waypoints.Points.Length - 1)
        {
            OnReachEnd?.Invoke(_unitHealth.boss);
            Destroy(gameObject);
            return;
        }
        _waypointIndex++;
        _target = Waypoints.Points[_waypointIndex];
        body.LookAt(_target);
    }
}
