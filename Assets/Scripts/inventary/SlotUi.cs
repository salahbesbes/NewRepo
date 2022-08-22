using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUi : MonoBehaviour
{
        private Image icon;
        private Transform iconTransfort;
        private Transform closeButton;
        public Item item = null;
        public Button CloseBtn;
        public TextMeshProUGUI Count;
        public Transform parent;
        private InventoryUI inventoryUI;

        public void InitSlot()
        {
                iconTransfort = transform.GetChild(0).GetChild(0);
                icon = iconTransfort.GetComponent<Image>();
                closeButton = transform.GetChild(1);
                inventoryUI = transform.parent.parent.GetComponent<InventoryUI>();
                Count = transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        public void addItem(Item itemParam, bool closeIcon = true)
        {
                item = itemParam;
                icon.sprite = itemParam.icon;
                transform.name = $"{itemParam.nameItem}";
                closeButton.gameObject.SetActive(closeIcon);
                iconTransfort.gameObject.SetActive(true);
        }

        public void clearSlot()
        {
                item = null;
                icon.sprite = null;
                transform.name = "cleared";
                closeButton.gameObject.SetActive(false);
                iconTransfort.gameObject.SetActive(false);
                Count.text = "";
        }

        public void useItem()
        {
                if (item == null) return;

                item.Use();
        }

        public void removeThisItem()
        {

                inventoryUI.inventory.remove(item);
                inventoryUI.updateUIInventory(inventoryUI.inventory);
        }

        public void UpdateOccurence(int occurence)
        {
                Count.text = $"{occurence}";
        }

        public void Craft()
        {
        }
}