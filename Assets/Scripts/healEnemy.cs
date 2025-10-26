using UnityEngine;

public class healEnemy : Enemy
{
    [SerializeField] private float heal = 20f;
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
            if (player != null)
            {
                player.takeDame(stayDamage);
            }
        }
    }
    protected override void die()
    {
        HealHp();
        base.die();
    }
    private void HealHp()
    {
        if (player != null)
        {
            player.Heal(heal);
        }
    }
}
