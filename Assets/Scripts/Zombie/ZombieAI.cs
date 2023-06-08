using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

enum ZombieState
{
    Idle=0,
    Walk=1,
    Dead=2,
    Attack=3
}
public class ZombieAI : MonoBehaviour
{
    // Start is called before the first frame update
    //Zombie Durum
    //Idle: Olduðu yerde durmak
    //Walk: Yürümek
    //Attack: Saldýrmak
    //Dead: Ölmek
    Animator animator;
    NavMeshAgent agent;
    ZombieState zombieState;
    GameObject playerObject;
    PlayerHealth playerHealth;
    ZombieHealth zombieHealth;

    void Start()
    {
        zombieHealth = GetComponent<ZombieHealth>();
        playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        zombieState = ZombieState.Idle;
        animator = GetComponent<Animator>(); //Bunun üstündeki Animator'ü al.
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (zombieHealth.GetHealth() <= 0)
        {
            SetState(ZombieState.Dead);
        }
       
        
        switch (zombieState)
        {
            case ZombieState.Dead:
                KillZombie();
                break;
            case ZombieState.Attack:
                Attack();
                break;
            case ZombieState.Walk:
                SearchForTarget();
                break;
            case ZombieState.Idle:
                SearchForTarget();
                break;
           
            
        }
    }

    private void Attack()
    {
        SetState(ZombieState.Attack);
        agent.isStopped = true;

    }

    
    void MakeAttack()
    {
        playerHealth.DeductHealth(10);
        SearchForTarget();
    }

   
    private void SearchForTarget()
    {
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (distance < 1.5f)
        {
            Attack();
        }
        else if(distance < 10)
        {
            MoveToPlayer();
        }
        else
        {
            SetState(ZombieState.Idle);
            agent.isStopped = true;
        }

        
    }

    
    private void SetState(ZombieState state)//State'ler arasý geçiþ için yapýlmýþ bir fonksiyon.
    {
        zombieState = state;//string deðerini alýyor.
        //Animator
        animator.SetInteger("state", (int)state);//state'in int deðerini al.
        //Hem class içinde hem de animator üzerinden set etmiþ oluyoruz.
    }

    
    private void MoveToPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(playerObject.transform.position);
        SetState(ZombieState.Walk);
    }

    
    private void KillZombie()
    {
        SetState(ZombieState.Dead);
        agent.isStopped = true;
        Destroy(gameObject, 5);
    }
}
