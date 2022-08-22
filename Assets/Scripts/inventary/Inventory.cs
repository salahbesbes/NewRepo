using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new iventory", menuName = "Inventory ")]
public class Inventory : ScriptableObject
{
        public readonly Dictionary<Item, int> Occurenceitems = new Dictionary<Item, int>();
        public InventoryUI inventoryUI = null;
        public List<Item> items = new List<Item>();

        public int space = 20;

        public bool Add(Item item)
        {
                // Check if out of space
                if (items.Count >= space)
                {
                        Debug.Log("Not enough room.");
                        return false;
                }
                items.Add(item);

                if (Occurenceitems.ContainsKey(item))
                {
                        Occurenceitems[item]++;
                }
                else
                {
                        Occurenceitems.Add(item, 1);
                }
                return true;
        }

        public bool remove(Item item)
        {
                if (items.Count <= 0)
                        return false;
                items.Remove(item);

                Occurenceitems[item]--;
                if (Occurenceitems[item] == 0)
                {
                        Occurenceitems.Remove(item);
                }

                return true;
        }

        private void OnEnable()
        {
                items.Clear();
                Occurenceitems.Clear();
        }
}