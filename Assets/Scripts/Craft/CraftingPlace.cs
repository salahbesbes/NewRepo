using System.Collections.Generic;
using UnityEngine;

public class CraftingPlace : MonoBehaviour
{
        public List<CraftObject> objects = new List<CraftObject>();
        public CraftingUI craftingUi;
        public Inventory inventory = null;

        public void craftObject(CraftObject obj)
        {
                if (obj.CanBeCrafted(inventory))
                {
                        obj.Craft(inventory);
                        inventory.inventoryUI.updateUIInventory(inventory);
                        AddCraftedObjectToInventory(obj);
                        inventory.inventoryUI.updateUIInventory(inventory);
                        craftingUi.update();
                }
                else
                {
                        Debug.Log($" cant Craft {obj.nameItem}");
                }
        }

        public void AddCraftedObjectToInventory(CraftObject obj)
        {
                if (inventory == null)
                {
                        Debug.Log($"inventory is null");
                        return;
                }
                inventory.Add(obj);
        }

        private void OnTriggerExit(Collider other)
        {
                inventory = null;
        }

        private void OnTriggerEnter(Collider other)
        {
                if (other.CompareTag("Player"))
                {
                        Player player = other.GetComponent<Player>();
                        inventory = player.inventory;
                }
        }

        private void OnTriggerStay(Collider other)
        {
                if (other.CompareTag("Player"))
                {
                        Player player = other.GetComponent<Player>();
                        if (player.pressF)
                        {
                                craftingUi.open();
                                Player.CloseNotif.Invoke();
                        }
                }
        }
}