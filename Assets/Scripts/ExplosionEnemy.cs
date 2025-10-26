using UnityEngine;

public class Explosion : Enemy
{
    [SerializeField] private GameObject explorePrefabs;
    
    private void CreateExplosion()
    {
        if (explorePrefabs != null) { 
        
        Instantiate(explorePrefabs, transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           CreateExplosion();
        }
    }
    protected override void die()
    {
        CreateExplosion() ;
        base.die();
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        if (player != null)
    //        {
    //            player.takeDame(stayDamage);
    //        }
    //    }
    //}
}
