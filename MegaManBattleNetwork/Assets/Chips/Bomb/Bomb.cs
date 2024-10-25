using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Animator _visualAnimator;

    public void Explode()
    {
        _visualAnimator.SetTrigger("Explode");
    }
}
