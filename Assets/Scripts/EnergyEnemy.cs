using UnityEngine;

public class EnergyEnemy : Enemy
{
    [SerializeField] private GameObject energyObj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.takeDame(enterDamege);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(player != null)
            {
                player.takeDame(stayDamage);
            }
        }
    }
    protected override void die()
    {
        if (gameObject != null) {
            GameObject energy = Instantiate(energyObj, transform.position, Quaternion.identity);
            Destroy(energy,5f);
        }
        base.die();
    }
}
