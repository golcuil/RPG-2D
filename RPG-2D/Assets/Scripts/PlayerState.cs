using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected Rigidbody2D rb;

    protected float xInput;
    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.playerAnimator.SetBool(animBoolName, true);
        rb = player.playerRb;
    }

    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        player.playerAnimator.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        player.playerAnimator.SetBool(animBoolName, false);
    }
}
