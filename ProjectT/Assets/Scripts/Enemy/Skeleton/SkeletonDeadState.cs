using Unity.VisualScripting;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        enemy.cd.enabled = false;


        enemy.SetZeroVelocity();

        stateTimer = 3f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            enemy.DestroyEnemy(enemy.gameObject);
    }
}
