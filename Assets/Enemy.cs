using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private Transform player;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(CustomUpdate());
    }



    // Update is called once per frame
    IEnumerator CustomUpdate()
    {
        while (true)
        {


            float distance = Vector3.Distance(transform.position, player.position);


            if (distance < 2f)
            {
                agent.enabled = false;
                player.GetComponent<PlayerStats>().Damage(5f);
                yield return new WaitForSeconds(1f);

            }
            else
            {
                agent.enabled = true;
                agent.SetDestination(player.position);

            }

            yield return null;

        }
    }
}
