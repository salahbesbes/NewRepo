public class PlayerStateMachine : StateMachine
{
        public Player Player { get; }

        public PlayerMovingState movingState { get; }
        public PlayerCraftingState CraftingState { get; }
        public PlayerJumigState jumingState { get; }
        public PlayerIdelingState IdelingState { get; }

        public PlayerStateMachine(Player player)
        {
                Player = player;

                movingState = new PlayerMovingState(player);
                CraftingState = new PlayerCraftingState(player);
                jumingState = new PlayerJumigState(player);
                IdelingState = new PlayerIdelingState(player);
        }
}