using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHP = 100f;
    [SerializeField] private float currentHP;
    public UnityEvent OnDeath;

    private void Awake()
    {
        currentHP = maxHP;
    }
    public void SetMax(float hp)
    {
        maxHP = hp;
        currentHP = maxHP;
    }
    public void Take(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            currentHP = 0;
            OnDeath?.Invoke();
        }
    }
    public void TakeDamage(int amount)
    {
        Take((float)amount);
    }
}
