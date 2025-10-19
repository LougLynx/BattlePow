using UnityEngine;

public enum AttackType { Melee, Ranged }

[CreateAssetMenu(menuName = "Configs/Enemy", fileName = "EC_NewEnemy")]
public class EnemyConfig : ScriptableObject
{
    public float maxHP = 100f;
    public float moveSpeed = 2.2f;
    public float chaseRange = 8f;
    public float stopDistance = 1.1f;
    public AttackType attackType = AttackType.Melee;
    public int damage = 10;
    public float attackRange = 1.2f;
    public float attackCooldown = 1.0f;
}