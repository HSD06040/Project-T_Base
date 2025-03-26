using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.sword.DotActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .2f);
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        Vector2 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePositon.x && player.facingDir == 1)
            player.Flip();
        else if (player.transform.position.x < mousePositon.x && player.facingDir == -1)
            player.Flip();
    }
}
