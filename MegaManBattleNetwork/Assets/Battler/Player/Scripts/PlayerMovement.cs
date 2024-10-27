using System.Collections;
using System.Linq;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class PlayerMovement : MonoBehaviour
    {
        private Player _player;
        [SerializeField] private float _moveCooldown = 0.1f;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        public void Move(Vector2Int direction)
        {
            var playerCell = Globals.WorldToCell2D(_player.transform.position);
            if (!IsValidPosition(playerCell + direction) || !_player.CanMove)
                return;

            var moveCommand = new MoveCommand(direction, _player);
            moveCommand.Execute();
            StartCoroutine(MoveCooldownRoutine());
        }

        private bool IsValidPosition(Vector2Int cell)
        {
            var result = _player.ValidRows.Any(list => list.Contains(cell));
            return result;
        }

        public IEnumerator TakeDamageRoutine()
        {
            StopAllCoroutines();
            yield return new WaitForSeconds(_player.DamageTakenMoveCooldown);
            _player.CanMove = true;
        }

        private IEnumerator MoveCooldownRoutine()
        {
            _player.CanMove = false;
            yield return new WaitForSeconds(_moveCooldown);
            _player.CanMove = true;
        }
    }
}
