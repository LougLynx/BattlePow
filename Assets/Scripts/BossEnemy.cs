using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedBulletNormal = 20f;
    [SerializeField] private float speedBulletCircle = 20f;
    [SerializeField] private float hpValue = 10f;
    [SerializeField] private GameObject miniEnemy;
    [SerializeField] private float skillCoolDown = 5f;
    private float nextSkillTime = 5f;

    protected override void Update()
    {
        base.Update();
        if (Time.time >= nextSkillTime)
        {
            UseSkill();
        }
    }

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

    private void BanDanThuong()
    {
        if(player != null)
        {
            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMoveDirection(directionToPlayer * speedBulletNormal);
        }
    }

    private void BanDanTron()
    {
        const int bulletCount = 12;
        float angleStep = 360f / bulletCount;
        for(int i = 0; i < 12; i++)
        {
            float angle = i * angleStep;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            GameObject bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMoveDirection(bulletDirection * speedBulletCircle);
        }
    }

    private void HoiMau(float hpAmount)
    {
        currentHp = Mathf.Min(currentHp + hpAmount, maxHp);
        updateHpBar();
    }

    private void CreateMiniEnemy()
    {
        Instantiate(miniEnemy, transform.position, Quaternion.identity);
    }

    private void DichChuyen()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
        }
    }

    private void RandomSkill()
    {
        int ranDom = Random.Range(0, 4);
        switch (ranDom)
        {
            case 0:
                BanDanThuong();
                break;
            case 1:
                BanDanTron();
                break;
            case 2:
                HoiMau(hpValue);
                break;
            case 3:
                CreateMiniEnemy();
                break;
        }
    }

    private void UseSkill()
    {
        nextSkillTime = Time.time + skillCoolDown;
        RandomSkill();
    }
}
