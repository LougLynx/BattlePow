using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public float moveSpeed = 5f;
    public float attack = 1;
    public float maxHp = 100f;
    public float currentHp;
    private Rigidbody2D rb;
	private SpriteRenderer spriteRenderer;
	private Animator animator;



	[SerializeField] private Image hpBar;

	[SerializeField] private GameManager gameManager;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}
	void Start()
    {
        LoadDataFromManager();
        playerData.Instance.LoadPlayer();
       

        // Ensure currentHp is properly initialized
        if (currentHp <= 0)  
        {
            currentHp = maxHp;  
        }

        // Update the health bar
        updateHpBar();
    }

	void Update()
	{
		MovePlayer();
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			gameManager.PauseMenu();
		}
	}

	void MovePlayer()
	{
		Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		rb.linearVelocity = playerInput * moveSpeed;
		if (playerInput.x < 0)
		{
			spriteRenderer.flipX = true;
		}
		else if (playerInput.x > 0)
		{
			spriteRenderer.flipX = false;
		}
		if (playerInput != Vector2.zero)
		{
			animator.SetBool("isRun", true);
		}
		else
		{
			animator.SetBool("isRun", false);
		}
	}

	public void takeDame(float damege)
	{
		currentHp -= damege;
		currentHp = Mathf.Max(currentHp, 0);
		updateHpBar();
        playerData.Instance.SavePlayer();
        if (currentHp <= 0)
		{
			die();
		}
	}

	private void die()
	{
		//Destroy(gameObject);
		gameManager.OverMenu();
	}

	public void updateHpBar()
	{
		if (hpBar != null)
		{
			hpBar.fillAmount = currentHp / maxHp;
		}
	}
	public void Heal(float hp)
	{
		if(currentHp < maxHp)
		{
			currentHp += hp;
			currentHp = Mathf.Min(currentHp, maxHp);
			updateHpBar();
		}
        playerData.Instance.SavePlayer();
    }

    #region updatefunction
    public void UpgradeAttack(float amount)
    {
		goldManager.Instance.canSpend = true;
		goldManager.Instance.SpendGold(10);
        attack += amount;
        dataManager.Instance.gameData.attack = attack;
        dataManager.Instance.SaveGameData();
    }
    public void UpgradeMoveSpeed(float amount)
    {
        goldManager.Instance.canSpend = true;
        goldManager.Instance.SpendGold(10);
        moveSpeed += amount/10;
        dataManager.Instance.gameData.moveSpeed = moveSpeed;
        dataManager.Instance.SaveGameData();
    }
    public void UpgradeMaxHp(float amount)
    {
        goldManager.Instance.canSpend = true;
        goldManager.Instance.SpendGold(10);
        maxHp += amount;
        dataManager.Instance.gameData.maxHp = maxHp;
        dataManager.Instance.SaveGameData();
    }
    #endregion
    private void LoadDataFromManager()
    {
        GameData data = dataManager.Instance.gameData;
        attack = data.attack;
        moveSpeed = data.moveSpeed;
        maxHp = data.maxHp;
        //currentHp = data.maxHp;
    }

}
