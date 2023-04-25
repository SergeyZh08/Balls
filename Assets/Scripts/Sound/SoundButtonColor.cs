using UnityEngine;
using UnityEngine.UI;

public class SoundButtonColor : MonoBehaviour
{
    [SerializeField] private Image _image;
    void Start()
    {
        SetButtonColor();
    }

    public void SetButtonColor()
    {
        _image.color = SoundManager.Instance.IsSound ? Color.white : Color.gray;
    }
}
