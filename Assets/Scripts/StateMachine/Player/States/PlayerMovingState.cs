using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovingState : IState
{
        public Player player;

        public PlayerMovingState(Player player)
        {
                this.player = player;
        }

        public void Enter()
        {
                player.Animator.SetBool("jump", false);
                player.Animator.SetBool("running", true);
        }

        public void Exit()
        {
        }

        public Vector3 GetMovementInputDirection()
        {
                return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        }

        public void Jump()
        {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                        player.state.ChangeState(player.state.jumingState);
                }
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
                if (collider.CompareTag("Craft"))
                {
                        // show ui Element
                        Player.UiMessage.Invoke("Stay near the Vet and Press F To Enter Crafting Shop");
                }
        }

        public void OnTriggerExit(Collider collider)
        {
        }

        public void MovementControll()
        {
                Vector3 dir = GetMovementInputDirection();
                player.verticalVelocity += player.Gravity * 4 * Time.deltaTime;
                if (player.verticalVelocity < -100) player.verticalVelocity = -100;

                if (player.Controller.isGrounded)
                {
                        player.verticalVelocity = -100;
                        Jump();
                }

                player.Controller.Move(dir.normalized * (player.speed * Time.deltaTime) + new Vector3(0.0f, player.verticalVelocity, 0.0f) * Time.deltaTime);
                if (dir != Vector3.zero)
                {
                        Quaternion Rota = Quaternion.LookRotation(dir.normalized, Vector3.up);
                        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, Rota, player.RotationSpeed);
                }
                else
                {
                        player.state.ChangeState(player.state.IdelingState);
                }
        }

        public void PhysicsUpdate()
        {
                MovementControll();
        }



        public void Update()
        { }

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

}