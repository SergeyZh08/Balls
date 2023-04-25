using UnityEngine;

public enum LevelStage
{ 
    MainMenu,
    Sea,
    Autumn,
    Country,
    Egypt,
    Square,
    Tropical,
    LastStage
}

[System.Serializable]
public struct Sound
{
    public AudioClip AudioClip;
    public LevelStage LevelStage;
}

[CreateAssetMenu]
public class LevelSound : ScriptableObject
{
    public Sound[] Sounds;
}
