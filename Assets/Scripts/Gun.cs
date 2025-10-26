using System.Collections;
using TMPro;
using Unity.Jobs;
using UnityEngine;

public class Gun : MonoBehaviour
{
	private float rotateOffset = 180f;
	[SerializeField] private Transform firePos;
	[SerializeField] private GameObject bulletPrefabs;
	[SerializeField] private float shotDelay = 0.15f;
	private float nextShot;
	[SerializeField] private int maxAmmo = 24;
	[SerializeField] private int currentAmmo;
    private int defaultMaxAmmo;
    [SerializeField] private TextMeshProUGUI amoText;
	[SerializeField] private TextMeshProUGUI timeBuff;

	private bool isBuff = false;
	private float buffEndTime = 0f;
    [SerializeField] private int levelBuff = 1;

	[SerializeField] private AudioManager audioManager;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
        defaultMaxAmmo = maxAmmo;
        currentAmmo = maxAmmo;
		updateAmoText();
	}

	// Update is called once per frame
	void Update()
	{
		RotateGun();
		ReLoad();
        ShotByNumberBullet(levelBuff);
		updateTimeBuff();
	}

	void RotateGun()
	{
		if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.width)
		{
			return;
		}
		Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);
		if (angle < -90 || angle > 90)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			transform.localScale = new Vector3(1, -1, 1);
		}
	}
	void Shot()
	{
        if (Time.timeScale == 0f) return;
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > 0)
		{
			nextShot = Time.time + shotDelay;
			Instantiate(bulletPrefabs, firePos.position, firePos.rotation);
			currentAmmo--;
			updateAmoText();
			audioManager.PlayShot();
		}
	}

	void ShotByNumberBullet(int bullet)
	{
        if(levelBuff % 5 == 0)
		{
			bullet = 5;
        }else
		{
			bullet = levelBuff % 5;
        }
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > nextShot)
		{
			if(currentAmmo < bullet)
			{
				bullet = currentAmmo;
            }
			nextShot = Time.time + shotDelay;
            Quaternion leftRotation = firePos.rotation * Quaternion.Euler(0, 0, -15);
            Quaternion leftRotation2 = firePos.rotation * Quaternion.Euler(0, 0, -30);
            Quaternion rightRotation = firePos.rotation * Quaternion.Euler(0, 0, 15);
            Quaternion rightRotation2 = firePos.rotation * Quaternion.Euler(0, 0, 30);
            if (bullet == 1)
			{
                Instantiate(bulletPrefabs, firePos.position, firePos.rotation);
            } else if (bullet == 2)
			{
                Instantiate(bulletPrefabs, firePos.position, leftRotation);
                Instantiate(bulletPrefabs, firePos.position, rightRotation);
            }
            else if (bullet == 3)
            {
                Instantiate(bulletPrefabs, firePos.position, firePos.rotation);
                Instantiate(bulletPrefabs, firePos.position, leftRotation);
                Instantiate(bulletPrefabs, firePos.position, rightRotation);
            }
            else if (bullet == 4)
            {
                Instantiate(bulletPrefabs, firePos.position, leftRotation);
                Instantiate(bulletPrefabs, firePos.position, rightRotation); 
				Instantiate(bulletPrefabs, firePos.position, leftRotation2);
                Instantiate(bulletPrefabs, firePos.position, rightRotation2);
            }
            else if (bullet == 5)
            {
                Instantiate(bulletPrefabs, firePos.position, firePos.rotation);
                Instantiate(bulletPrefabs, firePos.position, leftRotation);
                Instantiate(bulletPrefabs, firePos.position, rightRotation);
                Instantiate(bulletPrefabs, firePos.position, leftRotation2);
                Instantiate(bulletPrefabs, firePos.position, rightRotation2);
            }
            currentAmmo -= bullet;
            updateAmoText();
            audioManager.PlayShot();
		}
	}
	private void updateTimeBuff()
	{
		if (timeBuff != null)
		{
			if (isBuff)
			{
				float timeLeft = buffEndTime - Time.time;
				timeBuff.text = Mathf.CeilToInt(timeLeft).ToString();
			}
			else
			{
				timeBuff.text = "";
			}
		}
	}

	public void ActivateBuff(int plusLevelBuff)
	{
		isBuff = true;
		if(levelBuff + plusLevelBuff >= 1)
		{
            levelBuff = levelBuff + plusLevelBuff;
        } else
		{
			levelBuff = 1;
        }
        buffEndTime = Time.time + 15f;
		StopAllCoroutines();
		StartCoroutine(DisableBuffAfterTime(15f));
	}

	private IEnumerator DisableBuffAfterTime(float duration)
	{
		yield return new WaitForSeconds(duration);
		isBuff = false;
	}

	void ReLoad()
	{
		if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
		{
			StartCoroutine(DelayReLoad(1f));
			audioManager.PlayReload();
		}
	}

	private IEnumerator DelayReLoad(float duration)
	{
		yield return new WaitForSeconds(duration);
		currentAmmo = maxAmmo;
		updateAmoText();
	}


	private void updateAmoText()
	{
		if (amoText != null)
		{
			if (currentAmmo > 0)
			{
				amoText.text = currentAmmo.ToString();
			}
			else
			{
				amoText.text = "0";
			}
		}
	}
    public void ActivateAmmoBuff()
    {
        StopCoroutine(ResetAmmoBuff()); 

        maxAmmo = 50; 
        currentAmmo = maxAmmo; 
        updateAmoText();

        StartCoroutine(ResetAmmoBuff());
    }
    private IEnumerator ResetAmmoBuff()
    {
        yield return new WaitForSeconds(15f);
        maxAmmo = defaultMaxAmmo; 
        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo; 
        }
        updateAmoText();
    }

}
