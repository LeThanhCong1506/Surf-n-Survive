using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonSprite : MonoBehaviour
{
    public Sprite normalSprite; // Sprite bình thường
    public Sprite pressedSprite; // Sprite khi nhấn
    private Button button;
    private Image buttonImage;
    private VolumeSettings m_volumeSettings;

    void Start()
    {
        m_volumeSettings = FindFirstObjectByType<VolumeSettings>();
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        if (gameObject.name.Contains("VFXButton"))
        {
            if (m_volumeSettings.GetVFXVolume() == 0)
            {
                buttonImage.sprite = pressedSprite;
            }
            else
            {
                buttonImage.sprite = normalSprite;
            }
        }

        if (gameObject.name.Contains("SoundButton"))
        {
            Debug.Log(m_volumeSettings.GetMusicVolume());
            if (m_volumeSettings.GetMusicVolume() == 0)
            {
                buttonImage.sprite = pressedSprite;
            }
            else
            {
                buttonImage.sprite = normalSprite;
            }
        }

        button.onClick.AddListener(ToggleSprite);

    }

    void ToggleSprite()
    {
        if (gameObject.name == "VFXButton")
        {
            if (buttonImage.sprite == normalSprite)
            {
                AudioManager.Instance.PlayButtonSound();
                buttonImage.sprite = pressedSprite;
                m_volumeSettings.MuteVFX();
            }
            else
            {
                buttonImage.sprite = normalSprite;
                m_volumeSettings.UnmuteVFX();
                AudioManager.Instance.PlayButtonSound();
            }
        }

        if (gameObject.name == "SoundButton")
        {
            if (buttonImage.sprite == normalSprite)
            {
                buttonImage.sprite = pressedSprite;
                m_volumeSettings.MuteMusic();
                AudioManager.Instance.PlayButtonSound();
            }
            else
            {
                buttonImage.sprite = normalSprite;
                m_volumeSettings.UnmuteMusic();
                AudioManager.Instance.PlayButtonSound();
            }
        }
    }
}
