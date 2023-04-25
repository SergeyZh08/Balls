using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Image _winWindow;
    [SerializeField] private Image _looseWindow;
    [SerializeField] private Image _pauseWindow;
    [SerializeField] private Image _blackScreen;

    public UnityEvent OnWin;

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

    public void Win()
    {
        _pauseWindow.gameObject.SetActive(false);

        if (Progress.Instance.CurrentLevel == Progress.Instance.LevelsCount)
        {
            Progress.Instance.SetCurrentLevel(1);
        }

        if (Progress.Instance.CurrentLevel == Progress.Instance.MaxOpenLevel)
        {
            Progress.Instance.SetMaxLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        Progress.Instance.SetCurrentLevel(SceneManager.GetActiveScene().buildIndex + 1);

        LoadManager.Save(Progress.Instance);

        _winWindow.gameObject.SetActive(true);
        OnWin.Invoke();
    }

    public void Loose()
    {
        _pauseWindow.gameObject.SetActive(false);
        _looseWindow.gameObject.SetActive(true);
    }

    public void GoToMenu()
    {
        StartCoroutine(StartAnimation(SceneManager.sceneCountInBuildSettings - 1));
    }

    public void NextLevel()
    {
        StartCoroutine(StartAnimation(Progress.Instance.CurrentLevel));
    }

    public void ReloadLevel()
    {
        StartCoroutine(StartAnimation(SceneManager.GetActiveScene().buildIndex));
    }

    public void SetPause(bool pause)
    {   
        Time.timeScale = pause ? 0 : 1;
    }

    private IEnumerator StartAnimation(int index)
    {
        _blackScreen.enabled = true;
        _blackScreen.color = new Color(_blackScreen.color.r, _blackScreen.color.g, _blackScreen.color.b, 0f);
        for (float f = 0; f < 1f; f += Time.deltaTime * 4)
        {
            _blackScreen.color = new Color(_blackScreen.color.r, _blackScreen.color.g, _blackScreen.color.b, f);
            yield return null;
        }
        _blackScreen.color = new Color(_blackScreen.color.r, _blackScreen.color.g, _blackScreen.color.b, 1f);

        SceneManager.LoadScene(index);
    }
}
