public class EnemyStateMachine : StateMachine
{
        public Enemy Player { get; }

        public EnemyPatroling enemyPatroling { get; }

        public EnemyStateMachine(Enemy enemy)
        {
                Player = enemy;

                enemyPatroling = new EnemyPatroling(enemy);
        }
}