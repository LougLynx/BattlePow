using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
	[SerializeField] public float moveSpeed = 1.0f;

	protected Player player;

	[SerializeField] public float maxHp = 50f;
	public float currentHp;
	[SerializeField] private Image hpBar;

	[SerializeField] public float enterDamege = 10f;
	[SerializeField] public float stayDamage = 1f;

	[SerializeField] public TextMeshProUGUI diemText;
	private static float diem = 0f;
	[SerializeField] public float diemZB = 100f;

	protected virtual void Start()
	{
		player = FindAnyObjectByType<Player>();
		if (currentHp <= 0)
		{
			currentHp = maxHp;
		}

		updateHpBar();
		//enemyManager.Instance.LoadEnemies();

		if (diemText == null)
		{
			diemText = GameObject.Find("Diem").GetComponent<TextMeshProUGUI>();
		}

	}
	protected virtual void Update()
	{
		moveToPlayer();
	}

	protected void moveToPlayer()
	{
		if (player != null)
		{
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
			flipEnemy();
		}
	}

	protected void flipEnemy()
	{
		if (player != null)
		{
			transform.localScale = new Vector3(player.transform.position.x < transform.position.x ? -1 : 1, 1, 1);
		}
	}
	public virtual void takeDame(float damege)
	{
		currentHp -= damege;
		currentHp = Mathf.Max(currentHp, 0);
		updateHpBar();
		if (currentHp <= 0)
		{
			diem += diemZB;
			if (Time.timeScale == 0)
			{
				diemText.text = "";
			}
			else
			{
				diemText.text = diem.ToString();
			}
			die();
		}
	}

	protected virtual void die()
	{
		Destroy(gameObject);
	}
	public void updateHpBar()
	{
		if (hpBar != null)
		{
			hpBar.fillAmount = currentHp / maxHp;
		}
	}

	public void SetCurrentHp(float hp)
	{
		Debug.Log("Before update: currentHp = " + currentHp);
		currentHp = hp;
		updateHpBar();
		Debug.Log("After update: currentHp = " + currentHp);
	}



}
