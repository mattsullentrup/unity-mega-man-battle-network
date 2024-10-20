using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player Player { get; set; }

    public void Move(Vector2Int direction)
    {
        if (!Player.CanMove) return;

        var moveCommand = new MoveCommand(direction, Player);
        moveCommand.Execute();
        Player.CanMove = false;
        Player.StartMoveCooldown();
    }
}
