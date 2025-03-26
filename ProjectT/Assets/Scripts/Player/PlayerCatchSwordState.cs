using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    private GameObject sword;
    public PlayerCatchSwordState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        sword = player.sword;

        player.fx.PlayDustFX();
        player.fx.ScreenShake(player.fx.shakeSwordPower);

        if (player.transform.position.x > sword.transform.position.x && player.facingDir == 1)
            player.Flip();
        else if (player.transform.position.x < sword.transform.position.x && player.facingDir == -1)
            player.Flip();

        rb.linearVelocity = new Vector2(player.swordReturnImpact * -player.facingDir, rb.linearVelocity.y);

        
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .1f);

    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}

