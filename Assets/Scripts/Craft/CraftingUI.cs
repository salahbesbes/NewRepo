using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
        public CraftingPlace place;

        private Transform objectsHolder;
        public SlotUi CraftobjectPrefab;
        public SlotUi ingrediantPrefab;
        private Button CloseButton;
        private List<CraftObject> craftedObj = new List<CraftObject>();

        private void Awake()
        {
                objectsHolder = transform.GetChild(0);
                CloseButton = transform.GetChild(1).GetComponent<Button>();
                objectsHolder.gameObject.SetActive(false);
        }

        public void CreateCraftPlaceUI()
        {
                foreach (CraftObject obj in place.objects)
                {
                        SlotUi slot = Instantiate(CraftobjectPrefab, objectsHolder);
                        // add event to the button
                        slot.transform.Find("CraftButton").GetComponent<Button>().onClick.AddListener(() => place.craftObject(obj));
                        slot.InitSlot();
                        slot.addItem(obj, false);
                }
                foreach (Transform child in objectsHolder)
                {
                        Transform ingrediantParent = child.Find("Ingrediants");
                        SlotUi Slot = child.GetComponent<SlotUi>();

                        CraftObject CraftObject = (CraftObject)Slot.item;

                        ingrediantPrefab.transform.Find("+").gameObject.SetActive(true);
                        for (int i = 0; i < CraftObject.ingrediants.Count; i++)
                        {
                                Ingrediant ingrediant = CraftObject.ingrediants[i];
                                if (i == CraftObject.ingrediants.Count - 1)
                                {
                                        ingrediantPrefab.transform.Find("+").gameObject.SetActive(false);
                                }
                                SlotUi slot = Instantiate(ingrediantPrefab, ingrediantParent);
                                slot.InitSlot();
                                slot.addItem(ingrediant.item, false);
                                slot.UpdateOccurence(ingrediant.Count);
                        }
                }
        }

        public void update()
        {
                foreach (Transform child in objectsHolder)
                {
                        SlotUi Slot = child.GetComponent<SlotUi>();

                        TextMeshProUGUI CountTransform = Slot.transform.Find("Count").GetComponent<TextMeshProUGUI>();

                        if (place.inventory.Occurenceitems.ContainsKey(Slot.item))
                        {
                                CountTransform.text = $"{place.inventory.Occurenceitems[Slot.item]}";
                        }
                        else
                        {
                                CountTransform.text = "0";
                        }
                }
        }

        public void close()
        {
                objectsHolder = transform.GetChild(0);
                objectsHolder.gameObject.SetActive(false);
                CloseButton.gameObject.SetActive(false);
                Time.timeScale = 1;
        }

        public void open()
        {
                Time.timeScale = 0;
                objectsHolder = transform.GetChild(0);
                objectsHolder.gameObject.SetActive(true);
                CloseButton.gameObject.SetActive(true);
                if (objectsHolder.childCount == 0)
                {
                        CreateCraftPlaceUI();
                }
        }
}