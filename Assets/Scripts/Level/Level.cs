using UnityEngine;

[System.Serializable]
public struct Task
{
    public ItemType ItemType;
    public int Number;
    public int Level;
}

public class Level : MonoBehaviour
{
    public static Level Instance;

    [field: SerializeField] public int NumberOfBalls { get; private set; } = 50;
    [field: SerializeField] public int MaxBallLevel { get; private set; } = 1;
    [field: SerializeField] public Task[] LevelTasks { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
