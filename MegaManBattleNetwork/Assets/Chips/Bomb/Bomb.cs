using UnityEngine;

public class Bomb : MonoBehaviour
{
    public void Explode()
    {
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Explode");
    }
}
