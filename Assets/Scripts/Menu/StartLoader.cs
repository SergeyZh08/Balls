using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLoader : MonoBehaviour
{
    [SerializeField] private Image _image;
    public void Load()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        _image.enabled = true;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
        for (float f = 0; f < 1f; f += Time.deltaTime * 4)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, f);
            yield return null;
        }
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);

        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }
}
