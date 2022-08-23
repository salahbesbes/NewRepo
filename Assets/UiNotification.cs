using System.Collections;
using TMPro;
using UnityEngine;

public class UiNotification : MonoBehaviour
{
        private TextMeshProUGUI textComponent;
        private Coroutine eraseCoroutine;

        private void Awake()
        {
                textComponent = GetComponent<TextMeshProUGUI>();

        }

        private void OnEnable()
        {
                Player.UiMessage += showMessage;
                Player.CloseNotif += eraseMessage;
        }

        private void OnDestroy()
        {
                Player.UiMessage -= showMessage;
                Player.CloseNotif -= eraseMessage;
        }

        private void Start()
        {
                eraseMessage();
        }

        public void showMessage(string message)
        {
                transform.gameObject.SetActive(true);


                if (eraseCoroutine != null) StopCoroutine(eraseCoroutine);
                textComponent.text = message;
                eraseCoroutine = StartCoroutine(EraseMessageAfter(3));
        }

        private IEnumerator EraseMessageAfter(int sec)
        {
                yield return new WaitForSeconds(sec);
                eraseMessage();
        }

        private void eraseMessage()
        {
                textComponent.text = "";
                transform.gameObject.SetActive(false);
        }
}