using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticalCollision : MonoBehaviour
{
        private ParticleSystem PSystem;
        private List<ParticleCollisionEvent> CollisionEvents;
        public static Action<int> UpdateEnemiesCount;

        private void Awake()
        {
                PSystem = GetComponent<ParticleSystem>();
                CollisionEvents = new List<ParticleCollisionEvent>();
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