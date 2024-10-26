using System.Linq;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class SkeletonEnemy : Enemy, IMoveableEnemy
    {
        public override void Start()
        {
            _chipCommand.Delay = 0.5f;
            base.Start();
        }

        public void Move(Player player)
        {
            var direction = new Vector2Int();

            // Global vertical movement
            if (player.transform.position.y > transform.position.y)
            {
                direction.y = 1;
            }
            else if (player.transform.position.y < transform.position.y)
            {
                direction.y = -1;
            }
            else
            {
                direction.y = 0;
            }

            // Global horizontal movement
            if (Mathf.Abs(player.transform.position.x - transform.position.x) > 2)
            {
                direction.x = -1;
            }

            GetComponentInParent<EnemyManager>().ValidateNewPosition(direction, this);
            StartCoroutine(ActionRoutine());
        }
    }
}
