using UnityEngine;

public class ICollectable : MonoBehaviour
{
        public Item item;

        public void OnTriggerEnter(Collider other)
        {
                if (other.CompareTag("Player"))
                {
                        Player player = other.GetComponent<Player>();
                        player.collect(this);
                        Destroy(gameObject);
                        ItemSpawner.currentNb--;
                }
        }
}