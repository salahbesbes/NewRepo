using TMPro;
using UnityEngine;

public class UIEnemyCount : MonoBehaviour
{
        private TextMeshProUGUI textMeshProUGUI;

        private void Awake()
        {
                textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
                ParticalCollision.UpdateEnemiesCount += updateCount;
        }

        private void OnDisable()
        {
                ParticalCollision.UpdateEnemiesCount -= updateCount;
        }

        private void updateCount(int count)
        {
                textMeshProUGUI.text = $"{count} enemies Left";
        }
}