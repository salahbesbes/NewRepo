using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
        public PlayerStateMachine state;

        [HideInInspector]
        public CharacterController Controller;
        public float speed = 100f;

        [HideInInspector]
        public Animator Animator;

        [HideInInspector]
        public float Gravity = -10;
        public float JumpHeight = 20;

        [HideInInspector]
        public bool pressF = false;



        public static Action<string> UiMessage;



        public static Action CloseNotif;

        public Item spell1;
        public Item spell2;

        [HideInInspector]
        public float verticalVelocity = 0;

        [HideInInspector]
        public float RotationSpeed = 100;
        public Inventory inventory;
        public InventoryUI UiInventory;
        public Item apple;

        public Button openInv;
        public float FieldOfView = 90;

        [HideInInspector]
        public float lineOfSightRadius = 10;
        public HashSet<Collider> EnemiesInLineOfsight = new HashSet<Collider>();

        [HideInInspector]
        public Collider Target;
        public Transform mapLimit;

        public void Start()
        {
                Animator = GetComponent<Animator>();
                inventory.inventoryUI = UiInventory;
                Controller = GetComponent<CharacterController>();
                state = new PlayerStateMachine(this);
                state.ChangeState(state.movingState);


                openInv.onClick.AddListener(() => UiInventory.open(inventory));
        }
        public void collect(ICollectable collectable)
        {
                inventory.Add(collectable.item);
                UiInventory.updateUIInventory(inventory);
        }
        public void onremoveItem()
        {
                inventory.remove(apple);
                UiInventory.updateUIInventory(inventory);
        }

        private void OnTriggerEnter(Collider other)
        {
                state.OnTriggerEnter(other);
        }



        public void CastSpell1()
        {
                if (inventory.Occurenceitems.ContainsKey(spell1) == false)
                {
                        UiMessage.Invoke("need to craft a spell ");
                        return;
                }
                if (Target == null)
                {
                        UiMessage.Invoke("no target available");
                        return;
                }

                ParticleSystem spell = Instantiate(spell1.prefab, Target.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
                spell.GetComponent<AudioSource>().Play();

                inventory.remove(spell1);
                UiInventory.updateUIInventory(inventory);
        }

        public void CastSpell2()
        {
                if (inventory.Occurenceitems.ContainsKey(spell2) == false)
                {
                        UiMessage.Invoke("need to craft a spell ");
                        return;
                }
                if (Target == null)
                {
                        UiMessage.Invoke("no target available");
                        return;
                }
                ParticleSystem spell = Instantiate(spell2.prefab, Target.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
                spell.GetComponent<AudioSource>().Play();
                inventory.remove(spell2);
                UiInventory.updateUIInventory(inventory);
        }

        public void onAddItem()
        {
                inventory.Add(apple);
                UiInventory.updateUIInventory(inventory);
        }

        // Update is called once per frame
        private void Update()
        {
                state.HandleInput();
                state.Update();
                state.PhysicsUpdate();
        }

        private void OnDrawGizmos()
        {
        }
}