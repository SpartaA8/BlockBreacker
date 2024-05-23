using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : BlockHandler
{
    private Animator animator;
    public event Action BossPhase2;    
    private int hitLayerIndex;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        hitLayerIndex = animator.GetLayerIndex("Hit Layer");
        currentHp = blockSO.hp;
    }

    public override void TakeDamage(int damage)
    {
        currentHp -= damage;

        animator.SetLayerWeight(hitLayerIndex, 1);
        animator.SetTrigger("BossHit");

        if (currentHp == 0)
        {
            Destroy(gameObject);
        }
        else if (currentHp == 30)
        {
            BossPhase2?.Invoke();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}