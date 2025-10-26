using System.Collections.Generic;
using UnityEngine;

public class BassicEnemy : Enemy
{
    [SerializeField] private List<DropItem> dropItems;
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
        GameObject dropItem = GetRandomDropItem();
        if (dropItem != null)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity); 
        }
        Destroy(gameObject);
    }



    private GameObject GetRandomDropItem()
    {
        float randomValue = Random.value; 
        float cumulativeRate = 0f;

        foreach (var item in dropItems)
        {
            cumulativeRate += item.dropRate;
            if (randomValue <= cumulativeRate)
            {
                return item.itemPrefab;
            }
        }
        return null;
    }

}

[System.Serializable]
public struct DropItem
{
    public GameObject itemPrefab; // Prefab của vật phẩm
    [Range(0, 1)] public float dropRate; // Tỷ lệ rơi (0.0 - 1.0)
}

