using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AI
{
    public class BotBrain : MonoBehaviour
    {
        public static List<BotBrain> AllBots = new List<BotBrain>();
        
        //TODO - players
        [SerializeField]
        private List<Transform> targets;

        [SerializeField]
        private NavMeshAgent agent;

        [SerializeField]
        private bool isRange = false;

        [SerializeField]
        private float attackDistance = 1;

        [SerializeField]
        private float lostDistance = 20;
        
        [SerializeField]
        private float sensorDistance = 10;
        [SerializeField]
        private float viewAngle = 60;

        [SerializeField]
        private float patroolRange = 3;

        private Coroutine corutine;
        private Transform currentTarget;
        
        private void Start()
        {
            agent.updateRotation = true;
        }

        private void OnDisable()
        {
            StopCoroutine(corutine);
            AllBots.Remove(this);
        }

        private void OnEnable()
        {
            corutine = StartCoroutine(Decision());
            AllBots.Add(this);
        }

        private IEnumerator Decision()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.25f);

                if (Time.timeScale <= 0.01f)
                    continue;

                if (currentTarget != null)
                {
                    var cureDist = Vector3.Distance(currentTarget.position, transform.position);

                    if (cureDist <= attackDistance)
                    {
                        agent.isStopped = true;
                        Attack();
                    }
                    else if (cureDist <= lostDistance)
                    {
                        agent.isStopped = false;
                        var needableDist = Vector3.Lerp(currentTarget.position, transform.position, attackDistance / cureDist);
                        agent.SetDestination(needableDist);
                    }
                    else
                        currentTarget = null;
                }

                if (currentTarget == null)
                {
                    float minDist = float.MaxValue;
                    Transform currTarget = null;
                    foreach (Transform target in targets)
                    {
                        var dist = Vector3.Distance(target.position, transform.position);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            currTarget = target;
                        }
                    }

                    if (minDist <= sensorDistance)
                    {
                        currentTarget = currTarget;
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, agent.destination) < 0.2f)
                        {
                            var dest = Random.insideUnitSphere * patroolRange;
                            dest.y = 0;
                            agent.isStopped = false;
                            agent.SetDestination(transform.position + dest);
                        }
                    }
                }
            }
        }

        private void Attack()
        {
            Debug.Log("Attack");
        }

        public void Update()
        {
            if (currentTarget != null && agent.isStopped)
            {
                transform.forward = Vector3.Lerp(transform.forward, currentTarget.position - transform.position, Time.deltaTime * 5);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, lostDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sensorDistance);
        }
#endif
    }
}
