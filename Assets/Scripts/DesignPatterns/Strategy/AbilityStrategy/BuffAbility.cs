using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffAbility : IAbilityStrategy
{
    public PlayerStateMachine PlayerStateMachine { get; }
    private float currentBuffTime = 0;
    private readonly Action applyBuff;
    private readonly Action revertBuff;
    private static Dictionary<Type, BuffAbility> activeBuffs = new Dictionary<Type, BuffAbility>();

    public BuffAbility(PlayerStateMachine playerStateMachine, float buffTime, Action applyBuff, Action revertBuff)
    {
        this.PlayerStateMachine = playerStateMachine;
        this.applyBuff = applyBuff;
        this.revertBuff = revertBuff;
        currentBuffTime += buffTime;
    }

    public void ExtendTime(float buffTime)
    {
        currentBuffTime += buffTime;
    }

    public void Use()
    {
        var buffType = this.GetType();
        if (activeBuffs.ContainsKey(buffType))
        {
            activeBuffs[buffType].ExtendTime(currentBuffTime);
            return;
        }
        
        applyBuff();
        activeBuffs[buffType] = this;
        PlayerStateMachine.StartCoroutine(BuffCoroutine());
    }

    private IEnumerator BuffCoroutine()
    {
        yield return new WaitWhile(() =>
        {
            currentBuffTime -= Time.deltaTime;
            return currentBuffTime > 0;
        });
        currentBuffTime = 0;
        activeBuffs.Remove(this.GetType());
        revertBuff();
    }
}
