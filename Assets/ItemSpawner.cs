using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
        public List<Item> items = new List<Item>();
        public Transform mapLimit;
        public int maxNumber;
        public static int currentNb = 0;

        private void Awake()
        {
                StartCoroutine(CreateItems());
        }

        private Item SelectRandomItem()
        {
                return items[Random.Range(0, items.Count)];
        }

        private void CrateItemsAtRandomPosition(int nbOfItems)
        {
                Vector3 TopLeft = mapLimit.Find("TopLeft").position;
                Vector3 BotRight = mapLimit.Find("BotRight").position;

                for (int i = 0; i < nbOfItems; i++)
                {
                        float X = Random.Range(TopLeft.x, BotRight.x);
                        float Z = Random.Range(TopLeft.z, BotRight.z);

                        Vector3 randomPos = new Vector3(X, mapLimit.position.y, Z);

                        Item item = SelectRandomItem();
                        Instantiate(item.prefab, randomPos, Quaternion.identity, transform);
                        currentNb++;
                }
        }

        private IEnumerator CreateItems()
        {
                while (true)
                {
                        yield return new WaitUntil(() =>
                        {
                                return (currentNb <= maxNumber);
                        });
                        CrateItemsAtRandomPosition(10);
                        yield return new WaitForSeconds(10);
                }
        }
}