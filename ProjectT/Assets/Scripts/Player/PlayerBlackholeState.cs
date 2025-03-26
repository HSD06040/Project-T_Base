using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private float flyTime = .4f;
    private bool skillUsed;

    private float defaultGravity;
    public PlayerBlackholeState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        defaultGravity = player.rb.gravityScale;

        skillUsed = false;
        stateTimer = flyTime;
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();

        player.rb.gravityScale = defaultGravity;
        player.fx.MakeTransprent(false);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            rb.linearVelocity = new Vector2(0, 15f);
        if (stateTimer < 0)
        {
            rb.linearVelocity = new Vector2(0, -1f);

            if (!skillUsed)
            {
                if(player.skill.blackhole.CanUseSkill())
                skillUsed = true;
            }

        }

        if (player.skill.blackhole.SkillCompleted())
            stateMachine.ChangeState(player.airState);

    }
}
