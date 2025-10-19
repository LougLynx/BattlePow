using UnityEngine;

public class SpumAnimationDriver : MonoBehaviour
{
    public Transform visualRoot;
    public SPUM_Prefabs spum;
    public float moveThreshold = 0.05f;
    bool _isMoving;

    void Awake()
    {
        if (!spum && visualRoot) spum = visualRoot.GetComponentInChildren<SPUM_Prefabs>(true);
        if (!spum) spum = GetComponentInChildren<SPUM_Prefabs>(true);
    }

    void Start()
    {
        if (spum == null) { enabled = false; return; }
        if (spum.OverrideController == null) spum.OverrideControllerInit();
        if (!spum.allListsHaveItemsExist())
        {
            spum.PopulateAnimationLists();
            spum.OverrideControllerInit();
        }
        SafePlay(PlayerState.IDLE);
    }

    public void TickMove(float speed)
    {
        bool moving = speed > moveThreshold;
        if (moving == _isMoving) return;
        _isMoving = moving;
        SafePlay(moving ? PlayerState.MOVE : PlayerState.IDLE);
    }

    public void PlayAttack() => SafePlay(PlayerState.ATTACK);
    public void PlayDamaged() => SafePlay(PlayerState.DAMAGED);
    public void PlayDeath() => SafePlay(PlayerState.DEATH);

    void SafePlay(PlayerState st, int index = 0)
    {
        if (spum == null) return;
        int count = GetCount(st);
        if (count == 0)
        {
            if (st == PlayerState.MOVE || st == PlayerState.IDLE) st = PlayerState.IDLE;
            else st = PlayerState.OTHER;
            count = GetCount(st);
            index = 0;
        }
        spum.PlayAnimation(st, Mathf.Clamp(index, 0, Mathf.Max(0, count - 1)));
    }

    int GetCount(PlayerState st)
    {
        return st switch
        {
            PlayerState.IDLE => spum.IDLE_List?.Count ?? 0,
            PlayerState.MOVE => spum.MOVE_List?.Count ?? 0,
            PlayerState.ATTACK => spum.ATTACK_List?.Count ?? 0,
            PlayerState.DAMAGED => spum.DAMAGED_List?.Count ?? 0,
            PlayerState.DEBUFF => spum.DEBUFF_List?.Count ?? 0,
            PlayerState.DEATH => spum.DEATH_List?.Count ?? 0,
            _ => spum.OTHER_List?.Count ?? 0,
        };
    }
}