using System.Collections;
using UnityEngine;

public class EnemyPatroling : IState
{
        private Enemy enemy;

        public EnemyPatroling(Enemy enemy)
        {
                this.enemy = enemy;
        }

        public void Enter()
        {
                Transform patrolPoints = CreateSurfaceOfMoving(enemy.mapLimit);
                StartPatrol(patrolPoints);
        }

        public void Exit()
        {
                throw new System.NotImplementedException();
        }

        public void HandleInput()
        {
                throw new System.NotImplementedException();
        }

        public void OnAnimationEnterEvent()
        {
                throw new System.NotImplementedException();
        }

        public void OnAnimationExitEvent()
        {
                throw new System.NotImplementedException();
        }

        public void OnAnimationTransitionEvent()
        {
                throw new System.NotImplementedException();
        }

        public void OnTriggerEnter(Collider collider)
        {
                throw new System.NotImplementedException();
        }

        public void OnTriggerExit(Collider collider)
        {
                throw new System.NotImplementedException();
        }

        public void PhysicsUpdate()
        {
                throw new System.NotImplementedException();
        }

        public void Update()
        {
                throw new System.NotImplementedException();
        }

        private void StartPatrol(Transform patrolPoints)
        {
                enemy.StartCoroutine(Patrol(patrolPoints));
        }

        private bool CompaireABSFloat(float nb1, float nb2)
        {
                return Mathf.Abs(RoundFloat(nb1, 2)) == Mathf.Abs(RoundFloat(nb2, 2));
        }

        protected float RoundFloat(float value, int nb)
        {
                float power = Mathf.Pow(10, nb);
                return Mathf.Round(value * power) * (1 / power);
        }

        private IEnumerator Patrol(Transform patrolPoints)
        {
                int i = 0;

                if (patrolPoints.childCount == 0)
                {
                        Debug.Log($" PATROL Points IS EMPTY  ...");

                        yield break;
                }
                if (patrolPoints.childCount == 1)
                {
                        Debug.Log($" Only One Patrol Point :/ ...");
                }
                else
                {
                        float verticalVelocity = 0;
                        Vector3 FinalDestination;
                        while (true)
                        {
                                i = i % patrolPoints.childCount;


                                // set the Final Destination
                                FinalDestination = patrolPoints.GetChild(i).position;
                                Vector3 Direction = FinalDestination - enemy.transform.position;
                                // we dont move to the next Patrol Point until we reach the Final
                                // Destination
                                float timeElapsed = 0, lerpDuration = 2;

                                Quaternion targetRotation = Quaternion.LookRotation(Direction);
                                Transform PartToRotate = enemy.transform.GetChild(0);

                                yield return new WaitUntil(() =>
                                {
                                        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation,
                                                    targetRotation,
                                                     timeElapsed / lerpDuration
                                                    )
                                                    .eulerAngles;
                                        //partToRotate.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
                                        timeElapsed += Time.deltaTime;
                                        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

                                        return CompaireABSFloat(PartToRotate.rotation.y, targetRotation.y);
                                });

                                yield return new WaitUntil(() =>
                                {
                                        verticalVelocity += (-15 * 4) * Time.deltaTime;
                                        if (verticalVelocity < -100) verticalVelocity = -100;
                                        enemy.Controller.Move(new Vector3(0.0f, -2, 0.0f));
                                        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, FinalDestination, Time.deltaTime * enemy.speed * 5);
                                        // Walk Forward
                                        enemy.animator.SetBool("Run Forward", true);
                                        return (FinalDestination - enemy.transform.position).magnitude <= 5f;
                                });
                                // we can make him look around him for a while then fo to the next
                                // point ( code here )
                                enemy.animator.SetBool("Run Forward", false);

                                yield return new WaitForSeconds(2f);
                                i++;
                        }
                }
        }

        private Transform CreateSurfaceOfMoving(Transform transformHolder)
        {
                Vector3 TopLeft = transformHolder.Find("TopLeft").position;
                Vector3 BotRight = transformHolder.Find("BotRight").position;

                Transform parent = new GameObject("patrolPoints").transform;
                for (int i = 0; i < 20; i++)
                {
                        float X = Random.Range(TopLeft.x, BotRight.x);
                        float Z = Random.Range(TopLeft.z, BotRight.z);

                        Vector3 randomPos = new Vector3(X, enemy.transform.position.y, Z);

                        GameObject obj = new GameObject($"point_{i}");
                        obj.transform.position = randomPos;
                        obj.transform.SetParent(parent);
                }
                return parent;
        }
}