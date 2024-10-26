using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class PlayerChipUI : MonoBehaviour
    {
        [SerializeField] private GameObject _chipQueuePanel;

        private void Start()
        {
            GameManager.RoundEnding += OnRoundEnding;
        }

        private void OnDestroy()
        {
            GameManager.RoundEnding -= OnRoundEnding;
        }

        public void CreateChipImages(List<ChipSO> chips)
        {
            if (chips == null || chips.Count == 0)
                return;

            var reversedChips = new List<ChipSO>(chips);
            reversedChips.Reverse();
            foreach (var chip in reversedChips)
            {
                GameObject newObject = new();
                Image newImage = newObject.AddComponent<Image>();
                newImage.sprite = chip.Sprite;
                newObject.transform.SetParent(_chipQueuePanel.transform);
                newImage.rectTransform.sizeDelta = new Vector2(1, 1);
                newImage.rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                newObject.SetActive(true);
            }
        }

        public void DestroyChipImage()
        {

            Destroy(_chipQueuePanel.transform.GetChild(_chipQueuePanel.transform.childCount - 1).gameObject);

        }

        private void OnRoundEnding()
        {
            foreach (Transform child in _chipQueuePanel.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
