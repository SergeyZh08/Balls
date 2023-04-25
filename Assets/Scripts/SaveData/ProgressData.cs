[System.Serializable]
public class ProgressData
{
    public int MaxOpenLevel;
    public int CurrentLevel;
    public int LevelsCount;
    public bool IsSound;

    public ProgressData(Progress progress)
    {
        MaxOpenLevel = progress.MaxOpenLevel;
        CurrentLevel = progress.CurrentLevel;
        LevelsCount = progress.LevelsCount;
        IsSound = progress.IsSound;
    }
}
