using UnityEngine;

public class MiniEnemy : Enemy
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.takeDame(enterDamege);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.takeDame(stayDamage);
        }
    }
}
