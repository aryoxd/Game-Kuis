using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;

    private bool muted = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("bgmMuted"))
        {
            PlayerPrefs.SetInt("bgmMuted", 0);
        }

        Load();
        UpdateButton();
        BackgroundMusic.SetMute(muted);
    }

    public void OnButtonPress()
    {
        muted = !muted;
        BackgroundMusic.SetMute(muted);
        Save();
        UpdateButton();
    }

    private void UpdateButton()
    {
        soundOnIcon.enabled = !muted;
        soundOffIcon.enabled = muted;
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("bgmMuted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("bgmMuted", muted ? 1 : 0);
    }
}
