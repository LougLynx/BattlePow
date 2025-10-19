using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyMovement))]
public class EnemyBrain : MonoBehaviour
{
    public EnemyConfig config;
    public Transform player;
    public string playerTag = "Player";
    public Transform visualRoot;
    public SpumAnimationDriver animDriver;
    public MeleeAttack melee;
    public RangedAttack ranged;
    private Health _hp;
    private EnemyMovement _move;

    void Awake()
    {
        _hp = GetComponent<Health>();
        _move = GetComponent<EnemyMovement>();
        if (!animDriver) animDriver = GetComponent<SpumAnimationDriver>();
    }

    void Start()
    {
        if (!player)
        {
            var p = GameObject.FindGameObjectWithTag(playerTag);
            if (p) player = p.transform;
        }
        ApplyConfig();
        _move.target = player;
        _move.visualRoot = visualRoot;
        if (animDriver && !animDriver.visualRoot) animDriver.visualRoot = visualRoot;
        _hp.OnDeath.AddListener(() =>
        {
            _move.Stop();
            animDriver?.PlayDeath();
            Destroy(gameObject, 2f);
        });
        if (ranged && ranged.enabled && !ranged.shootPoint && visualRoot)
        {
            var sp = FindDeep(visualRoot, "R_Weapon") ?? FindDeep(visualRoot, "R.Weapon") ?? FindDeep(visualRoot, "Weapon");
            if (sp) ranged.shootPoint = sp;
        }
    }

    void Update()
    {
        animDriver?.TickMove(_move.CurrentSpeed);
        if (!player || config == null) return;
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > config.chaseRange) return;
        bool didAttack = false;
        if (config.attackType == AttackType.Melee && melee && dist <= config.attackRange + 0.1f)
            didAttack = melee.TryAttack(player, visualRoot);
        else if (config.attackType == AttackType.Ranged && ranged && dist <= config.attackRange * 1.1f)
            didAttack = ranged.TryAttack(player);
        if (didAttack) animDriver?.PlayAttack();
    }

    void ApplyConfig()
    {
        if (!config) { Debug.LogWarning($"[{name}] Missing EnemyConfig"); return; }
        _hp.SetMax(config.maxHP);
        _move.MoveSpeed = config.moveSpeed;
        _move.ChaseRange = config.chaseRange;
        _move.StopDistance = Mathf.Min(config.stopDistance, config.attackRange);
        if (melee) melee.enabled = (config.attackType == AttackType.Melee);
        if (ranged) ranged.enabled = (config.attackType == AttackType.Ranged);
        if (melee && melee.enabled)
            melee.Setup(config.damage, config.attackCooldown, config.attackRange);
        if (ranged && ranged.enabled)
            ranged.Setup(config.damage, config.attackCooldown, config.attackRange, 8f);
    }

    static Transform FindDeep(Transform root, string name)
    {
        foreach (Transform c in root)
        {
            if (c.name == name) return c;
            var r = FindDeep(c, name);
            if (r) return r;
        }
        return null;
    }
}