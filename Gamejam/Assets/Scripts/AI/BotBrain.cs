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
        [SerializeField]
        private NavMeshAgent agent;

        [SerializeField]
        private float lostDistance = 20;
        
        [SerializeField]
        private float sensorDistance = 10;

        [SerializeField]
        private float viewAngle = 60;

        [SerializeField]
        private float patroolRange = 3;

        private Coroutine corutine;
        private Character currentTarget;

        private Character self;
        private WeaponController weaponController;

        private float lastPatroolTime;
        
        private void Start()
        {
            self = GetComponent<Character>();
            weaponController = self.WeaponController;
            agent.updateRotation = true;
        }

        private void OnDisable()
        {
            StopCoroutine(corutine);
        }

        private void OnEnable()
        {
            corutine = StartCoroutine(Decision());
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
                    var cureDist = Vector3.Distance(currentTarget.transform.position, transform.position);

                    if (cureDist <= weaponController.Weapon.Range)
                    {
                        agent.isStopped = true;
                        weaponController.UseAllowed = true;
                    }
                    else if (cureDist <= lostDistance)
                    {
                        agent.isStopped = false;
                        var needableDist = Vector3.Lerp(currentTarget.transform.position, transform.position,
                            weaponController.Weapon.Range / cureDist);
                        agent.SetDestination(needableDist);
                        weaponController.UseAllowed = false;
                    }
                    else
                        currentTarget = null;
                }

                if (currentTarget == null)
                {
                    weaponController.UseAllowed = false;
                    float minDist = float.MaxValue;
                    Character currTarget = null;
                    if (Character.Characters.ContainsKey(Character.CharType.Player))
                    {
                        var targets = Character.Characters[Character.CharType.Player];

                        foreach (var target in targets)
                        {
                            var dist = Vector3.Distance(target.transform.position, transform.position);
                            if (dist < minDist /* && Vector3.Angle(transform.forward, (target.transform.position - transform.position).normalized) <=
                                viewAngle*/)
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
                                agent.isStopped = true;
                            }

                            if (Time.time - lastPatroolTime > 5)
                            {
                                var dest = Random.insideUnitSphere * patroolRange;
                                dest.y = 0;
                                agent.isStopped = false;
                                agent.SetDestination(transform.position + dest);
                                lastPatroolTime = Time.time;
                            }
                        }
                    }
                    else
                    {
                        if (Time.time - lastPatroolTime > 3)
                        {
                            if (Vector3.Distance(transform.position, agent.destination) < 0.2f)
                            {
                                var dest = Random.insideUnitSphere * patroolRange;
                                dest.y = 0;
                                agent.isStopped = false;
                                agent.SetDestination(transform.position + dest);
                                lastPatroolTime = Time.time;
                            }
                        }
                    }
                }
            }
        }

        public void Update()
        {
            if (currentTarget != null && agent.isStopped)
            {
                transform.forward = Vector3.Lerp(transform.forward, currentTarget.transform.position - transform.position, Time.deltaTime * 5);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (weaponController != null && weaponController.Weapon != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, weaponController.Weapon.Range);
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, lostDistance);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sensorDistance);
        }
#endif
    }
}
