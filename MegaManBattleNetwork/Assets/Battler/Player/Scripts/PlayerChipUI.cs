using System.Collections.Generic;
using UnityEngine;

public class PlayerChipUI : MonoBehaviour
{
    //     @onready var _chip_queue_container: HBoxContainer = %ChipQueueHBoxContainer


    // func _ready() -> void:
    // 	SignalBus.chip_selection_starting.connect(_on_chip_selection_starting)


    // func setup_chips(chips: Array[Chip]) -> void:
    // 	for chip in chips:
    // 		var texture_rect := TextureRect.new()
    // 		texture_rect.texture = chip.icon
    // 		texture_rect.expand_mode = TextureRect.EXPAND_FIT_WIDTH_PROPORTIONAL
    // 		texture_rect.stretch_mode = TextureRect.STRETCH_KEEP_CENTERED
    // 		_chip_queue_container.add_child(texture_rect)


    // func _on_chip_command_executed() -> void:
    // 	_chip_queue_container.get_child(-1).queue_free()


    // func _on_chip_selection_starting() -> void:
    // 	for child in _chip_queue_container.get_children():
    // 		child.queue_free()

    [SerializeField] private GameObject _chipQueuePanel;

    private void Start()
    {
        BattleGrid.ChipSelectionStarting += OnChipSelectionStarting;
    }

    private void OnDestroy()
    {
        BattleGrid.ChipSelectionStarting -= OnChipSelectionStarting;
    }

    public void SetupChips(List<ChipSO> chips)
    {
        return;
    }

    private void OnChipCommandExecuted()
    {

    }

    private void OnChipSelectionStarting()
    {
        return;
    }
}
