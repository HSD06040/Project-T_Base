using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);
 
        if (player.IsGroundDetected() || player.IsGroundDetected2())
            stateMachine.ChangeState(player.idleState);

        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed * 0.8f, rb.linearVelocity.y);
    }
}
