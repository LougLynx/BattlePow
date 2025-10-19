using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject projectilePrefab;
    public LayerMask projectileHitMask;
    public int damage = 8;
    public float cooldown = 1.2f;
    public float projectileSpeed = 8f;
    float _nextTime;

    public void Setup(int dmg, float cd, float range, float projSpeed)
    {
        damage = dmg; cooldown = cd; projectileSpeed = projSpeed;
    }

    public bool TryAttack(Transform target)
    {
        if (!target || Time.time < _nextTime || !projectilePrefab) return false;
        Vector3 from = shootPoint ? shootPoint.position : transform.position;
        Vector2 dir = (target.position - from).normalized;
        var go = Instantiate(projectilePrefab, from, Quaternion.identity);
        var p = go.GetComponent<Projectile>();
        if (!p) p = go.AddComponent<Projectile>();
        p.Launch(dir, damage, projectileSpeed, projectileHitMask);
        _nextTime = Time.time + cooldown;
        return true;
    }
}
