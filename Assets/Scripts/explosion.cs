using UnityEngine;

public class explosion : MonoBehaviour
{
    [SerializeField] private float damage =10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision.CompareTag("Player"))
        {
            player.takeDame(damage);
        }
        if (collision.CompareTag("Enemy"))
        {
            enemy.takeDame(damage);
        }
    }
    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
