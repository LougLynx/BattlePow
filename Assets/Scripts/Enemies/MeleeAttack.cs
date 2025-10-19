using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Transform origin;
    public Vector2 offset = new Vector2(0.6f, 0f);
    public float radius = 0.6f;
    public LayerMask targetMask;
    public int damage = 10;
    public float cooldown = 1.0f;
    float _nextTime;

    public void Setup(int dmg, float cd, float range)
    {
        damage = dmg; cooldown = cd; radius = range * 0.5f;
    }

    public bool TryAttack(Transform target, Transform visualRoot = null)
    {
        if (Time.time < _nextTime || target == null) return false;
        Vector3 center = (origin ? origin.position : transform.position);
        if (visualRoot && visualRoot.localScale.x < 0) center += (Vector3)(new Vector2(-offset.x, offset.y));
        else center += (Vector3)offset;

        var hits = Physics2D.OverlapCircleAll(center, radius, targetMask);
        bool hit = false;
        foreach (var h in hits)
        {
            var health = h.GetComponent<Health>();
            if (health) { health.Take(damage); hit = true; }
        }
        _nextTime = Time.time + cooldown;
        return hit;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 center = (origin ? origin.position : transform.position) + (Vector3)offset;
        Gizmos.DrawWireSphere(center, radius);
    }
}