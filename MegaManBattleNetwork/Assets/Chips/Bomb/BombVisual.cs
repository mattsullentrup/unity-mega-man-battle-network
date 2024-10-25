using UnityEngine;

public class BombVisual : MonoBehaviour
{
    public void EndExplosion()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
