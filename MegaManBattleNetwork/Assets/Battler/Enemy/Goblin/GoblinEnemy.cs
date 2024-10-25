using UnityEngine;

namespace MegaManBattleNetwork
{
    public class GoblinEnemy : Enemy, IMoveableEnemy, IBombAttacker
    {
    [SerializeField] private GameObject _bombPrefab;
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
            if (Mathf.Abs(player.transform.position.x - transform.position.x) > 3)
            {
                direction.x = -1;
            }
            else if (Mathf.Abs(player.transform.position.x - transform.position.x) < 3)
            {
                direction.x = 1;
            }

            GetComponentInParent<EnemyManager>().ValidateNewPosition(direction, this);
            StartCoroutine(ActionRoutine());
        }

        public void ThrowBomb()
        {
            var animator = GetComponent<Animator>();
            animator.SetTrigger("ThrowBomb");
        }

        public void CreateBomb()
        {
            var bomb = Instantiate(_bombPrefab);
            bomb.transform.position = transform.position + new Vector3(0.5f, 0, 0);
            bomb.transform.localScale = new(-1, 1, 1);
            // StartCoroutine(DamageDelayRoutine());
        }
    }
}
