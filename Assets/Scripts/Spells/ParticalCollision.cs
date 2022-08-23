using System;
using UnityEngine;

public class ParticalCollision : MonoBehaviour
{
        private ParticleSystem PSystem;
        public static Action<int> UpdateEnemiesCount;

        private void Awake()
        {
                PSystem = GetComponent<ParticleSystem>();
        }

        public void OnParticleCollision(GameObject other)
        {
                if (other.CompareTag("Enemy"))
                {
                        Destroy(other);
                        Spawner.CurentEnemyNumbers--;
                        UpdateEnemiesCount.Invoke(Spawner.CurentEnemyNumbers);

                        if (Spawner.CurentEnemyNumbers == 0)
                        {
                                Debug.LogWarning($" You win the game");
                                return;
                        }
                }
        }
}