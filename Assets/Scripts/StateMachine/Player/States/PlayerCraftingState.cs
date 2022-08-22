using UnityEngine;

public class PlayerCraftingState : IState
{
        public Player player;

        public PlayerCraftingState(Player player)
        {
                this.player = player;
        }

        public PlayerCraftingState()
        {
                Debug.Log($" enter walking state ");
        }

        public void Enter()
        {
                throw new System.NotImplementedException();
        }

        public void Update()
        {
                throw new System.NotImplementedException();
        }

        public void HandleInput()
        {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                        player.state.ChangeState(player.state.movingState);
                }
        }

        public void Exit()
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
}