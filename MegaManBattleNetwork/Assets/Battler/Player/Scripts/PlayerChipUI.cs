using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerChipUI : MonoBehaviour
{
    [SerializeField] private GameObject _chipQueuePanel;

    private void Start()
    {
        BattleGrid.ChipSelectionStarting += OnChipSelectionStarting;
        ChipCommandSO.ChipExecuting += OnChipExecuting;
    }

    private void OnDestroy()
    {
        BattleGrid.ChipSelectionStarting -= OnChipSelectionStarting;
        ChipCommandSO.ChipExecuting -= OnChipExecuting;
    }

    public void CreateChipImages(List<ChipSO> chips)
    {
        if (chips.Count == 0)
            return;

        foreach (var chip in chips)
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

    private void OnChipExecuting(Battler battler, ChipCommandSO chipCommand)
    {
        if (battler is not Player)
            return;

        Destroy(_chipQueuePanel.transform.GetChild(0).gameObject);
    }

    private void OnChipSelectionStarting()
    {
        foreach (Transform child in _chipQueuePanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
