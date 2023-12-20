using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : EnemyState
{
    private EnemySkeleton enemy;

    public SkeletonAttack(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.deltaTime;
    }

    public override void Update()
    {
        base.Update();

        enemy.ZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
