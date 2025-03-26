using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(14, null);
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.StopSFX(14);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.moveSpeed, rb.linearVelocityY);

        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);
            
        
    }
}
