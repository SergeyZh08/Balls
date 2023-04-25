using UnityEngine;
using UnityEngine.SceneManagement;

public class Progress : MonoBehaviour
{
    public static Progress Instance;

    public int MaxOpenLevel { get; private set; }
    public int CurrentLevel { get; private set; }
    public int LevelsCount { get; private set; }
    public bool IsSound { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ProgressData progressData = LoadManager.Load();
        if (progressData == null)
        {
            MaxOpenLevel = 1;
            CurrentLevel = 0;
            LevelsCount = SceneManager.sceneCountInBuildSettings - 2;
            IsSound = true;
        }
        else
        {
            MaxOpenLevel = progressData.MaxOpenLevel;
            CurrentLevel = progressData.CurrentLevel;
            LevelsCount = progressData.LevelsCount;
            IsSound = progressData.IsSound;
        }
    }

    public void SetCurrentSound(bool isSound)
    {
        IsSound = isSound;
    }

    public void SetCurrentLevel(int level)
    {
        CurrentLevel = level;
    }

    public void SetMaxLevel(int level)
    {
        MaxOpenLevel = level;
    }

    [ContextMenu("Delete")]
    public void Delete()
    {
        LoadManager.Delete();
    }
}
