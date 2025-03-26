using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            return;
        }

        if (xInput !=0 && player.facingDir != xInput)
            stateMachine.ChangeState(player.idleState);

        if(yInput < 0)
            rb.linearVelocity = new Vector2(0,rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y * 0.8f);


        if (player.IsGroundDetected() || !player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
