using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public float MoveSpeed = 2f;
    public float ChaseRange = 8f;
    public float StopDistance = 1.1f;
    public Transform target;
    public Transform visualRoot;

    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (!target) { _rb.linearVelocity = Vector2.zero; return; }
        float dist = Vector2.Distance(transform.position, target.position);
        if (dist > ChaseRange) { _rb.linearVelocity = Vector2.zero; return; }

        if (dist <= StopDistance)
            _rb.linearVelocity = Vector2.zero;
        else
        {
            Vector2 dir = ((Vector2)(target.position - transform.position)).normalized;
            _rb.linearVelocity = dir * MoveSpeed;

            if (visualRoot && Mathf.Abs(dir.x) > 0.01f)
            {
                var s = visualRoot.localScale;
                s.x = dir.x >= 0 ? Mathf.Abs(s.x) : -Mathf.Abs(s.x);
                visualRoot.localScale = s;
            }
        }
    }
    public void Stop() => _rb.linearVelocity = Vector2.zero;
    public float CurrentSpeed => _rb.linearVelocity.magnitude;
}
