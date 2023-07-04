using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossLocoMotion : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Transform playerTransform;
    Animator animator;

    // settings refresh rate of boss chasing
    public float maxTime=  1.0f;
    public float minDist = 5.0f;
    public float time = 0.0f;
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0.0f)
        {
            float distance = (playerTransform.position - agent.destination).sqrMagnitude;
            if(distance > minDist * minDist)
            {
                agent.destination = playerTransform.position;
            }
            time = maxTime;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
