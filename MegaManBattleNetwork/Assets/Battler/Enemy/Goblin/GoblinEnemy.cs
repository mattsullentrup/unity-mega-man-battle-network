using System.Collections;
using UnityEngine;

namespace MegaManBattleNetwork
{
    public class GoblinEnemy : Enemy, IMoveableEnemy, IBombAttacker
    {
        public float InstantiationDelay => _instantiationDelay;
        [SerializeField] private GameObject _bombPrefab;
        [SerializeField] private float _instantiationDelay = 2;
        private GameObject _bombSpawnPosition;

        public override void Start()
        {
            _bombSpawnPosition = transform.GetChild(1).gameObject;
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
            StartCoroutine(DelayBombThrowRoutine());
        }

        public IEnumerator DelayBombThrowRoutine()
        {
            yield return new WaitForSeconds(_instantiationDelay);
            var bomb = Instantiate(_bombPrefab);
            bomb.transform.position = _bombSpawnPosition.transform.position + new Vector3(0.5f, 0, 0);
            bomb.transform.localScale = new(-1, 1, 1);
        }
    }
}
