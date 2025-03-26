using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        if (Input.GetKeyDown(KeyCode.R) && player.skill.blackhole.blackholeUnlocked)
            stateMachine.ChangeState(player.blackhole);

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword() && !player.isBusy && player.skill.sword.swordUnlocked)
            stateMachine.ChangeState(player.aimSword);

        if (Input.GetKeyDown(KeyCode.Q)&&player.skill.parry.parryUnlocked)
            stateMachine.ChangeState(player.counterAttack);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);

        if (!player.IsGroundDetected() && !player.IsGroundDetected2())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKey(KeyCode.Space)&&(player.IsGroundDetected() || player.IsGroundDetected2()))
            stateMachine.ChangeState(player.jumpState);
    }

    private bool HasNoSword()
    {
        if(!player.sword)
        {
            return true;
        }

        player.sword.GetComponent<Sword_Skill_Controller>().ReturningSword();
        return false;
    }
}
