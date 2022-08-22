using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
        public List<Enemy> enemies = new List<Enemy>();
        public Transform mapLimit;
        public int maxNumber;
        public static int CurentEnemyNumbers = 0;

        private void Awake()
        {
                CreateEnemies();
        }

        private Enemy SelectRandomEnemy()
        {
                return enemies[Random.Range(0, enemies.Count)];
        }

        private void CreateEnemies()
        {
                Vector3 TopLeft = mapLimit.Find("TopLeft").position;
                Vector3 BotRight = mapLimit.Find("BotRight").position;

                for (int i = 0; i < maxNumber; i++)
                {
                        float X = Random.Range(TopLeft.x, BotRight.x);
                        float Z = Random.Range(TopLeft.z, BotRight.z);

                        Vector3 randomPos = new Vector3(X, 3, Z);

                        Enemy enemy = SelectRandomEnemy();
                        Enemy enemyObj = Instantiate(enemy, randomPos, Quaternion.identity, transform);
                        enemyObj.Init(mapLimit);
                        CurentEnemyNumbers++;
                }
        }
}