using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieComponent : MonoBehaviour
{
    public float zombieDamage = 5;

    public NavMeshAgent zombieNavMesh;
    public Animator zombieAnimator;
    public ZombieStateMachine stateMachine;

    public GameObject followTarget;

    private void Awake()
    {
        zombieNavMesh = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        stateMachine = GetComponent<ZombieStateMachine>();
    }

    private void Start()
    {
        Initialize(followTarget);
    }

    public void Initialize(GameObject _followTarget)
    {
        followTarget = _followTarget;

        ZombieIdleState idleState = new ZombieIdleState(this, stateMachine);
        ZombieFollowState followState = new ZombieFollowState(followTarget, this, stateMachine);
        ZombieAttackState attackState = new ZombieAttackState(followTarget, this, stateMachine);
        ZombieDeadState deadState = new ZombieDeadState(this, stateMachine);
        stateMachine.AddState(ZombieStateType.Idling, idleState);
        stateMachine.AddState(ZombieStateType.Following, followState);
        stateMachine.AddState(ZombieStateType.Attacking, attackState);
        stateMachine.AddState(ZombieStateType.isDead, deadState);

        stateMachine.Initialize(ZombieStateType.Following);
    }

    void DisableNavMesh()
    {
        zombieNavMesh.enabled = false;
    }

    public void Remove()
    {
        DisableNavMesh();
        Destroy(gameObject);
    }

    private void CheckGround()
    {

    }
}
