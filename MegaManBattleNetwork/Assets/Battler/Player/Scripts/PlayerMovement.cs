using System.Linq;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class PlayerMovement : MonoBehaviour
    {
        public Player Player { get; set; }

        public void Move(Vector2Int direction)
        {
            if (!Player.CanMove)
                return;

            var playerCell = Globals.WorldToCell2D(Player.transform.position);
            if (!IsValidPosition(playerCell + direction))
                return;

            var moveCommand = new MoveCommand(direction, Player);
            moveCommand.Execute();
        }

        private bool IsValidPosition(Vector2Int cell)
        {
            var result = Player.ValidRows.Any(list => list.Contains(cell));
            return result;
        }
    }
}
