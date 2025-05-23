using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the behavior of UI buttons that toggle sound and VFX settings.
/// Updates the button's sprite based on the current volume state and handles user interactions to toggle these states.
/// </summary>
public class ChangeButtonSprite : MonoBehaviour
{
    [SerializeField] private Sprite m_normalSprite;
    [SerializeField] private Sprite m_pressedSprite;

    private Button m_button;
    private Image m_buttonImage;
    private VolumeSettings m_volumeSettings;
    private bool m_isVFXButton;
    private bool m_isSoundButton;

    void Start()
    {
        m_volumeSettings = FindFirstObjectByType<VolumeSettings>();
        m_button = GetComponent<Button>();
        m_buttonImage = GetComponent<Image>();

        string buttonName = gameObject.name;
        m_isVFXButton = buttonName.Contains("VFXButton");
        m_isSoundButton = buttonName.Contains("SoundButton");

        UpdateButtonSprite();

        m_button.onClick.AddListener(ToggleSprite);
    }

    void Update()
    {
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        if (m_isVFXButton)
        {
            m_buttonImage.sprite = m_volumeSettings.GetVFXVolume() > 0 ? m_normalSprite : m_pressedSprite;
        }
        else if (m_isSoundButton)
        {
            m_buttonImage.sprite = m_volumeSettings.GetMusicVolume() > 0 ? m_normalSprite : m_pressedSprite;
        }
    }

    void ToggleSprite()
    {
        if (m_isVFXButton)
        {
            bool isMuted = m_buttonImage.sprite == m_normalSprite;
            m_buttonImage.sprite = isMuted ? m_pressedSprite : m_normalSprite;

            if (isMuted)
            {
                AudioManager.Instance.PlayButtonSound();
                m_volumeSettings.MuteVFX();
            }
            else
            {
                m_volumeSettings.UnmuteVFX();
                AudioManager.Instance.PlayButtonSound();
            }
        }
        else if (m_isSoundButton)
        {
            bool isMuted = m_buttonImage.sprite == m_normalSprite;
            m_buttonImage.sprite = isMuted ? m_pressedSprite : m_normalSprite;

            if (isMuted)
            {
                m_volumeSettings.MuteMusic();
                AudioManager.Instance.PlayButtonSound();
            }
            else
            {
                m_volumeSettings.UnmuteMusic();
                AudioManager.Instance.PlayButtonSound();
            }
        }
    }
}
