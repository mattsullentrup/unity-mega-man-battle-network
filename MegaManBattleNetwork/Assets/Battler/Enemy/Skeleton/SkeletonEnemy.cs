using System.Linq;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class SkeletonEnemy : Enemy, IMoveableEnemy
    {
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
            var playerXPos = Mathf.Abs(player.transform.position.x);
            if (playerXPos - transform.position.x > 2)
            {
                direction.x = -1;
            }

            ValidateNewPosition(direction);
            StartCoroutine(ActionRoutine());
        }
    }
}
