using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
        private Transform itemHolder;
        private SlotUi[] slots;
        public Inventory inventory;
        private Button closeButon;

        private void Update()
        {
        }

        private void Awake()
        {
                itemHolder = transform.GetChild(0);
                closeButon = transform.GetChild(1).GetComponent<Button>();
                slots = itemHolder.GetComponentsInChildren<SlotUi>();
                foreach (var child in slots)
                {
                        child.InitSlot();
                }
        }

        private void Start()
        {
                //Debug.Log($" start called  ");
        }

        private int calculateOccurence(Item item)
        {
                return inventory.Occurenceitems[item];
        }

        //int CheckIfExist(Item item)
        //{
        //        return inventory.items.Count(el => el != null && el.nameItem == item.nameItem);
        //}
        public void updateUIInventory(Inventory inventory)
        {
                if (itemHolder.gameObject.activeInHierarchy == false) return;

                // for each object in inventory.Occurenceitems.Count create a slot or update
                // existing one
                int i = 0;
                foreach (var itemToAdd in inventory.Occurenceitems)
                {
                        int occurence = calculateOccurence(itemToAdd.Key);
                        if (i < inventory.Occurenceitems.Count)
                        {
                                //SlotUi oldSlot = slots.Where(e => e.item != null).
                                //                FirstOrDefault(el => el.item.nameItem == itemToAdd.Key.nameItem);
                                //Debug.Log($"   {itemToAdd.Key.nameItem} occ {occurence} index {i}  Length {inventory.Occurenceitems.Count}");
                                //if (oldSlot == null)
                                //{
                                slots[i].addItem(itemToAdd.Key);
                                slots[i].UpdateOccurence(occurence);
                                //}
                                //else
                                //{
                                //        oldSlot.UpdateOccurence(occurence);
                                //}
                        }
                        i++;
                }

                // clear every slot greatest than inventory.Occurenceitems.Count
                for (int j = i; j < slots.Length; j++)
                {
                        slots[j].clearSlot();
                }
        }

        public void close()
        {
                itemHolder = transform.GetChild(0);
                itemHolder.gameObject.SetActive(false);
                closeButon.gameObject.SetActive(false);
                this.inventory = null;
        }

        public void open(Inventory inventory)
        {
                this.inventory = inventory;
                itemHolder = transform.GetChild(0);
                itemHolder.gameObject.SetActive(true);
                closeButon.gameObject.SetActive(true);

                updateUIInventory(inventory);
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
                //itemHolder = transform.GetChild(0);
                //slots = itemHolder.GetComponentsInChildren<SlotUi>();

                //foreach (var child in slots)
                //{
                //        child.InitSlot();
                //}
        }
}