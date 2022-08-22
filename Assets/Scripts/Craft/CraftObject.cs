using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Object ", menuName = "Craft / Item")]
public class CraftObject : Item
{
        public List<Ingrediant> ingrediants = new List<Ingrediant>();

        public bool CanBeCrafted(Inventory inv)
        {
                foreach (Ingrediant ingrediant in ingrediants)
                {
                        if (inv.Occurenceitems.ContainsKey(ingrediant.item) == false) return false;

                        if (inv.Occurenceitems[ingrediant.item] < ingrediant.Count) return false;
                }

                return true;
        }

        public void Craft(Inventory inv)
        {
                foreach (Ingrediant ingrediant in ingrediants)
                {
                        for (int i = 0; i < ingrediant.Count; i++)
                        {
                                inv.remove(ingrediant.item);
                        }
                }
        }
}

[System.Serializable]
public class Ingrediant
{
        public Item item;

        [SerializeField]
        private int _count = 1;

        public int Count
        {
                get => _count; set
                {
                        if (value == 0)
                        {
                                Debug.Log($" count cant be 0 ");
                                value = 1;
                        }
                        _count = value;
                }
        }
}