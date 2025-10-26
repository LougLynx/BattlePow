using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 25f;
	[SerializeField] private float timeDestroy = 0.5f;
	[SerializeField] private float damege = 10f;
	[SerializeField] GameObject bloodPrefabs;
    protected Player player;


    private bool isBulletLevel2; 

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        isBulletLevel2 = CompareTag("BulletLevel2"); // Kiểm tra tag của đạn
        Destroy(gameObject, timeDestroy);
    }
    // Update is called once per frame
    void Update()
	{
		MoveBullet();
	}
	void MoveBullet()
	{
		transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            DealDamage(collider, player.attack + damege);
        }
        else if (collider.CompareTag("EnemyExplosion"))
        {
            float finalDamage = isBulletLevel2 ? (player.attack / 2 + damege / 2) : (damege + player.attack);
            DealDamage(collider, finalDamage);
        }
        else if (collider.CompareTag("EnemyEnergy"))
        {
            float finalDamage = isBulletLevel2 ? ((damege + player.attack) * 1.5f) : (damege + player.attack);
            DealDamage(collider, finalDamage);
        }
    }

    private void DealDamage(Collider2D collider, float damageAmount)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.takeDame(damageAmount);
            GameObject blood = Instantiate(bloodPrefabs, transform.position, Quaternion.identity);
            Destroy(blood, 1f);
        }
        Destroy(gameObject);
    }
}
