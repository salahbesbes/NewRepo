using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
        private float timer;
        public float deathtimer = 5;

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
                timer += Time.deltaTime;

                if (timer >= deathtimer)
                {
                        Destroy(gameObject);
                }
        }
}