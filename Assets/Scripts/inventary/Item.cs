using System;
using UnityEngine;
[CreateAssetMenu(fileName = "new Item ", menuName = "Inventory / Item")]
[Serializable]
public class Item : ScriptableObject
{
        public Sprite icon;
        public string nameItem;
        public GameObject prefab;
        public string id;

        private void OnEnable()
        {
                id = System.Guid.NewGuid().ToString();
        }

        public virtual void Collect()
        {
                Debug.Log($"{nameItem} Collected");
        }

        public virtual void Use()
        {
                Debug.Log($"{nameItem} is used");
        }
}