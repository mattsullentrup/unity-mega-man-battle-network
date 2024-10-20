using UnityEngine;

public class BattleGrid : MonoBehaviour
{
    private Grid _grid;
    private static BattleGrid _instance;

    public Grid Grid => _grid;
    public static BattleGrid Instance => Instance;

    private void Awake()
    {
        _instance = this;
        _grid = GetComponent<Grid>();
    }
}
