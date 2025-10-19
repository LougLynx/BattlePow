using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 5;
    public float speed = 8f;
    public float lifeTime = 4f;
    public LayerMask hitMask;
    Vector2 _dir;

    public void Launch(Vector2 dir, int dmg, float spd, LayerMask mask)
    {
        _dir = dir.normalized;
        damage = dmg;
        speed = spd;
        hitMask = mask;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(_dir * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & hitMask) == 0) return;
        var health = other.GetComponent<Health>();
        if (health) health.Take(damage);
        Destroy(gameObject);
    }
}