using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player Player { get; set; }

    public void Move(Vector2 input)
    {
        if (!Player.CanMove) return;

        var moveCommand = new MoveCommand(input, Player);
        moveCommand.Execute();
        Player.CanMove = false;
    }
}
