using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieStates
{

    GameObject followTarget;
    float attackRange = 2;

    private IDamageable damageableObject;

    int movementZHash = Animator.StringToHash("MovementZ");
    int isAttackingHash = Animator.StringToHash("isAttacking");


    public ZombieAttackState(GameObject _followTarget, ZombieComponent zombie, ZombieStateMachine zombieStateMachine) : base(zombie, zombieStateMachine)
    {
        followTarget = _followTarget;
        updateInterval = 2;

        damageableObject = followTarget.GetComponent<IDamageable>();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ownerZombie.zombieNavMesh.isStopped = true;
        ownerZombie.zombieNavMesh.ResetPath();
        ownerZombie.zombieAnimator.SetFloat(movementZHash, 0);
        ownerZombie.zombieAnimator.SetBool(isAttackingHash, true);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        damageableObject?.TakeDamage(ownerZombie.zombieDamage);
    }

    public override void Update()
    {
        //base.Update();
        ownerZombie.transform.LookAt(followTarget.transform.position, Vector3.up);

        float distanceBetween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);

        if (distanceBetween > attackRange)
        {
            stateMachine.ChangeState(ZombieStateType.Following);
        }
    }

    public override void Exit()
    {
        base.Exit();
        ownerZombie.zombieNavMesh.isStopped = false;
        ownerZombie.zombieAnimator.SetBool(isAttackingHash, false);
    }
}
