using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerJumigState : IState
{
        private Player player;

        public PlayerJumigState(Player player)
        {
                this.player = player;
        }

        public void Enter()
        {
                player.Animator.SetBool("running", false);
                player.Animator.SetBool("jump", true);
                player.verticalVelocity = Mathf.Sqrt(player.JumpHeight * -2f * player.Gravity);
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                        return;
                }

                if (Input.GetMouseButtonDown(0))
                {
                        player.CastSpell1();
                }
                if (Input.GetMouseButtonDown(1))
                {
                        player.CastSpell2();
                }

                player.pressF = Input.GetKeyDown(KeyCode.F);
        }
        public void OnTriggerEnter(Collider collider)
        {
                Debug.Log($"collider {collider}");
                if (collider.CompareTag("Craft"))
                {
                        // show ui Element
                        //player.UiMessage.Invoke("Stay near the Vet and Press F To Enter Crafting Shop");
                }
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


        public void OnTriggerExit(Collider collider)
        {
                throw new System.NotImplementedException();
        }

        public void PhysicsUpdate()
        {
                MovementControll();
        }

        public void Update()
        {
        }

        public Vector3 GetMovementInputDirection()
        {
                return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        }

        public void MovementControll()
        {
                Vector3 dir = GetMovementInputDirection();
                if (player.Controller.isGrounded && player.verticalVelocity <= 0)
                {
                        player.verticalVelocity = -100;

                        player.state.ChangeState(player.state.movingState);
                }

                if (player.verticalVelocity < -100) player.verticalVelocity = -100;
                player.verticalVelocity += player.Gravity * 4 * Time.deltaTime;
                player.Controller.Move(dir.normalized * (player.speed * Time.deltaTime) + new Vector3(0.0f, player.verticalVelocity, 0.0f) * Time.deltaTime);
                if (dir != Vector3.zero)
                {
                        Quaternion Rota = Quaternion.LookRotation(dir.normalized, Vector3.up);
                        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, Rota, player.RotationSpeed);
                }
                else
                {
                        //player.Animator.SetBool("running", false);
                }
        }
}