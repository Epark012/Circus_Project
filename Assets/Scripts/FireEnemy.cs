using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireEnemy : Interacable
{
    //NavMesh 
    NavMeshAgent agent;
    bool isVulnerable = false;

    [SerializeField]
    private Transform target;

    //particle array

    [Header("Particle Systems")]
    [Tooltip("Particle Array for particles in child")]
    [SerializeField]
    private ParticleSystem[] particleSystemArray;

    private MeshRenderer mesh;

    [SerializeField]
    private ParticleSystem deathFX;
    
    [SerializeField]
    private ParticleSystem hammerStateDeathFX;
    
    //sound

    [Header("Sound Section")]
    [Tooltip("Destroy sound effect")]
    [SerializeField]
    private AudioSource a1;
    
    [Tooltip("IDLE sound effect")]
    private AudioSource a2;

    [Header("AI Section")]
    [Tooltip("distance to trigger attack state")]
    [SerializeField]
    private float attackDistance = 1.0f;

    [Tooltip("Idle state of fire enemy")]
    public FireEnemyState fireEnemyState = FireEnemyState.Idle;


    [Tooltip("States of Colour")]
    [SerializeField]
    Color idleColor;
    [Tooltip("States of Colour")]
    [SerializeField]
    Color attackColor;
    [Tooltip("States of Colour")]
    [SerializeField]
    Color hammerTimeColor;

    [SerializeField]
    private Transform[] roamingPositions;
    [SerializeField]
    private int posIndex = 0;

    public enum FireEnemyState
    {
        Idle,
        attackDistance,
        HammerState
    }

    private void Start()
    {
        target = FindObjectOfType<Player>().transform;
        gameObject.GetComponent<Transform>().eulerAngles = new Vector3(-90, 0, 0);
        agent = GetComponent<NavMeshAgent>();
        particleSystemArray = GetComponentsInChildren<ParticleSystem>();
        mesh = GetComponentInChildren<MeshRenderer>();
        //animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        ChangeState();
        DistanceToTarget(target);
    }


    private void ChangeState()
    {
        switch (fireEnemyState)
        {
            case FireEnemyState.Idle:
                {
                    agent.isStopped = false;
                    ChangeColour(idleColor);
                    DoPatrolling();
                }
                break;
            case FireEnemyState.attackDistance:
                {
                    ChangeColour(attackColor);
                    agent.SetDestination(target.position);
                }
                break;
            case FireEnemyState.HammerState:
                {
                    ChangeColour(hammerTimeColor);
                }
                break;
        }
    }

    private void DistanceToTarget(Transform target)
    {
        if(Vector3.Distance(transform.position, target.transform.position) < attackDistance && this.fireEnemyState != FireEnemyState.HammerState)
        {
            this.fireEnemyState = FireEnemyState.attackDistance;
        }
    }
    private void ChangeColour(Color color)
    {
        if (mesh.material.color == color)
            return;
        //Change Body Colour
        mesh.material.color = color;
        //Change Emmision Speed
        foreach (var particle in particleSystemArray)
        {
            //Particle colour change
            particle.GetComponent<ParticleSystemRenderer>().material.color = color;
        }
    }
    private void DoPatrolling()
    {
        for (int i = 0; i < roamingPositions.Length - 1; i++)
        {
            agent.SetDestination(roamingPositions[posIndex].position);

            if (Vector3.Distance(this.transform.position, roamingPositions[posIndex].position) < 0.2)
            {
                if (posIndex == roamingPositions.Length - 1)
                {
                    posIndex = 0;
                }
                else
                {
                    posIndex++;
                }
            }
        }
    }

    public override void OnHammerTime()
    {
        fireEnemyState = FireEnemyState.HammerState;
        agent.isStopped = true;
    }

    public override void OffHammerTime()
    {
        fireEnemyState = FireEnemyState.Idle;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if(other.gameObject.tag == "Player")
        {
            animator.SetBool("isTriggered", true);
        }*/
    }
    
}
