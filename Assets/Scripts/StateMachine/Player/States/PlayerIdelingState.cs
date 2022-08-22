using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerIdelingState : IState
{
        private Player player;

        public PlayerIdelingState(Player player)
        {
                this.player = player;
        }

        public Vector3 GetMovementInputDirection()
        {
                return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        }

        public void Enter()
        {
                player.Animator.SetBool("running", false);
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
        }

        public void Update()
        {
                Vector3 dir = GetMovementInputDirection();
                //player.verticalVelocity += player.Gravity * 4 * Time.deltaTime;
                //if (player.verticalVelocity < -100) player.verticalVelocity = -100;
                //player.Controller.Move(new Vector3(0.0f, player.verticalVelocity, 0.0f) * Time.deltaTime);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                        player.state.ChangeState(player.state.jumingState);
                        return;
                }
                if (dir != Vector3.zero)
                {
                        player.state.ChangeState(player.state.movingState);
                }
        }
}