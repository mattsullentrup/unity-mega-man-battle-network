using System.Collections;
using System.Linq;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class PlayerMovement : MonoBehaviour
    {
        public Player Player { get; set; }
        [SerializeField] private float _moveCooldown = 0.1f;

        public void Move(Vector2Int direction)
        {
            var playerCell = Globals.WorldToCell2D(Player.transform.position);
            if (!IsValidPosition(playerCell + direction) || !Player.CanMove)
                return;

            var moveCommand = new MoveCommand(direction, Player);
            moveCommand.Execute();
            StartCoroutine(MoveCooldownRoutine());
        }

        private bool IsValidPosition(Vector2Int cell)
        {
            var result = Player.ValidRows.Any(list => list.Contains(cell));
            return result;
        }

        public IEnumerator TakeDamageRoutine()
        {
            // Player.CanMove = false;
            // StopCoroutine(MoveCooldownRoutine());
            StopAllCoroutines();
            yield return new WaitForSeconds(Player.DamageTakenMoveCooldown);
            Player.CanMove = true;
        }

        private IEnumerator MoveCooldownRoutine()
        {
            Player.CanMove = false;
            yield return new WaitForSeconds(_moveCooldown);
            Player.CanMove = true;
        }
    }
}
