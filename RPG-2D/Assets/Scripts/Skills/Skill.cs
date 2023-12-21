using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;
    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.Instance.player;
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(cooldownTimer < 0)
        {
            //Use skill
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }

        //Skill is on cooldown
        return false;
    }

    public virtual void UseSkill()
    {
        //Do some skill spesific things.
    }
}
