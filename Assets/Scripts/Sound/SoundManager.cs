using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public bool IsSound { get; private set; }

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private LevelSound _levelSound;
    [SerializeField] private float _volume = 0.5f;
    [SerializeField] private LevelStage _stage;
    private AudioClip _audioClip;

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

    private void Start()
    {
        for (int i = 0; i < _levelSound.Sounds.Length; i++)
        {
            Sound sound = _levelSound.Sounds[i];

            if (_stage == sound.LevelStage)
            {
                _audioClip = sound.AudioClip;
                break;
            }
        }

        IsSound = Progress.Instance.IsSound;
        AudioListener.pause = !IsSound;

        _audioSource.clip = _audioClip;
        _audioSource.loop = true;
        _audioSource.volume = _volume;
        _audioSource.Play();

        StartCoroutine(SetStartVolume());
    }

    public void ChangeSound()
    {
        IsSound = !IsSound;

        AudioListener.pause = !IsSound;

        Progress.Instance.SetCurrentSound(IsSound);
        LoadManager.Save(Progress.Instance);
    }

    private IEnumerator SetStartVolume()
    {
        float target = _audioSource.volume;

        for (float t = 0; t < target; t += Time.deltaTime / 4)
        {
            _audioSource.volume = t;
            yield return null;
        }

        _audioSource.volume = target;
    }
}
