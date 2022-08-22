using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
        public EnemyStateMachine state;
        public Animator animator;

        [HideInInspector]
        public Transform mapLimit;
        public float speed = 2;

        [HideInInspector]
        public CharacterController Controller;

        private void Start()
        {
                Controller = GetComponent<CharacterController>();
                animator = transform.GetChild(0).GetComponent<Animator>();
                state = new EnemyStateMachine(this);
                state.ChangeState(state.enemyPatroling);
        }

        public void Init(Transform mapLimit)
        {
                this.mapLimit = mapLimit;
        }

        public IEnumerator rotateTowardDirection(Transform partToRotate, Vector3 dir, float timeToSpentTurning = 2)
        {
                float timeElapsed = 0, lerpDuration = timeToSpentTurning;

                if (partToRotate == null) yield return null;

                Quaternion startRotation = partToRotate.rotation;

                Quaternion targetRotation = Quaternion.LookRotation(dir);

                while (timeElapsed < lerpDuration)
                {
                        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,
                                    targetRotation,
                                     timeElapsed / lerpDuration
                                    )
                                    .eulerAngles;
                        //partToRotate.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
                        timeElapsed += Time.deltaTime;
                        yield return new WaitForSeconds(0.5f);
                        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                }
        }
}