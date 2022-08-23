using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineOfsghit : MonoBehaviour
{
        private Player player;
        private float lineOfSightRadius = 40;
        public LayerMask LineOfSightLayers;
        public float FieldOfView;
        void Start()
        {
                player = transform.parent.GetComponent<Player>();
                transform.GetComponent<CapsuleCollider>().radius = lineOfSightRadius;


        }

        public void Update()
        {
                GetAllEnemiesInRange();
                GetClosestEnemie();

        }

        private void GetClosestEnemie()
        {
                Dictionary<float, Collider> tmp = new Dictionary<float, Collider>();
                foreach (Collider enemy in player.EnemiesInLineOfsight)
                {
                        Vector3 direction = enemy.transform.position - player.transform.position;
                        float Distance = direction.magnitude;

                        tmp.Add(Distance, enemy);
                }

                var closestEnemy = tmp.OrderBy(el => el.Key).FirstOrDefault();
                player.Target = closestEnemy.Value;
        }

        public bool CheckLineOfSight(Collider Target)
        {
                Vector3 direction = (Target.transform.position - transform.position).normalized;
                float dotProduct = Vector3.Dot(transform.forward, direction);
                if (dotProduct >= Mathf.Cos(FieldOfView))
                {
                        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, lineOfSightRadius, LineOfSightLayers))
                        {
                                return true;
                        }
                }

                return false;
        }

        public void GetAllEnemiesInRange()
        {
                Collider[] targets = Physics.OverlapSphere(player.transform.position, lineOfSightRadius, LineOfSightLayers);
                player.EnemiesInLineOfsight.Clear();
                foreach (Collider collider in targets)
                {
                        if (collider.CompareTag("Enemy"))
                        {
                                if (CheckLineOfSight(collider))
                                {
                                        if (player.EnemiesInLineOfsight.Contains(collider)) { Debug.Log($" found duplicate"); continue; };

                                        player.EnemiesInLineOfsight.Add(collider);
                                }
                        }
                }
        }
}
