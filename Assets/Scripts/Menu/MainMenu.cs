using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button[] _levelsButton;
    [SerializeField] private Image _blackScreen;


    private void Start()
    {
        _blackScreen.enabled = false;

        Progress.Instance.SetCurrentLevel(0);
        int level = Progress.Instance.MaxOpenLevel;

        for(int i = 0; i < level; i++)
        {
            _levelsButton[i].interactable = true;
            _levelsButton[i].GetComponent<ButtonAnim>().enabled = true;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToStartScreen()
    {
        StartCoroutine(LoadLevelAnimation(0));
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(LoadLevelAnimation(level));
    }

    private IEnumerator LoadLevelAnimation(int level)
    {
        _blackScreen.enabled = true;
        _blackScreen.color = new Color(_blackScreen.color.r, _blackScreen.color.g, _blackScreen.color.b, 0f);
        for (float f = 0; f < 1f; f += Time.deltaTime * 4)
        {
            _blackScreen.color = new Color(_blackScreen.color.r, _blackScreen.color.g, _blackScreen.color.b, f);
            yield return null;
        }
        _blackScreen.color = new Color(_blackScreen.color.r, _blackScreen.color.g, _blackScreen.color.b, 1f);

        Progress.Instance.SetCurrentLevel(level);
        SceneManager.LoadScene(level);
    }
}
