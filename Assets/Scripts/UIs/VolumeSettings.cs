using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer m_myMixer;
    [SerializeField] private Slider m_musicSlider;
    [SerializeField] private Slider m_vfxSlider;

    private float m_previousMusicVolume;
    private float m_previousVFXVolume;

    private void Start()
    {
        // Kiểm tra xem SaveManager có tồn tại không
        if (SaveManager.Instance != null)
        {
            LoadMusicAndVFX();
        }
        else
        {
            // Fallback nếu SaveManager chưa được khởi tạo
            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                LoadMusicAndVFX();
            }
            else
            {
                SetMusic();
                SetVFX();
            }
        }
    }

    private void LoadMusicAndVFX()
    {
        // Sử dụng SaveManager để tải cài đặt
        if (SaveManager.Instance != null)
        {
            m_musicSlider.value = SaveManager.Instance.GetMusicVolume();
            m_vfxSlider.value = SaveManager.Instance.GetVFXVolume();
        }
        else
        {
            m_musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            m_vfxSlider.value = PlayerPrefs.GetFloat("VFXVolume");
        }

        SetMusic();
        SetVFX();
    }

    public void SetMusic()
    {
        float volume = m_musicSlider.value;
        m_myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        if (volume == 0)
        {
            m_myMixer.SetFloat("music", -80);
        }

        // Sử dụng SaveManager để lưu cài đặt
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveMusicVolume(volume);
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }

    public void SetVFX()
    {
        float volume = m_vfxSlider.value;
        m_myMixer.SetFloat("vfx", Mathf.Log10(volume) * 20);
        if (volume == 0)
        {
            m_myMixer.SetFloat("vfx", -80);
        }

        // Sử dụng SaveManager để lưu cài đặt
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveVFXVolume(volume);
        }
        else
        {
            PlayerPrefs.SetFloat("VFXVolume", volume);
        }
    }

    public void MuteMusic()
    {
        m_previousMusicVolume = m_musicSlider.value;
        m_musicSlider.value = 0;
        SetMusic();

        // Lưu giá trị volume trước khi mute vào SaveManager
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SavePreviousMusicVolume(m_previousMusicVolume);
        }
        else
        {
            PlayerPrefs.SetFloat("PreviousMusicVolume", m_previousMusicVolume);
        }
    }

    public void UnmuteMusic()
    {
        // Kiểm tra giá trị volume trước khi mute từ SaveManager
        if (SaveManager.Instance != null)
        {
            m_previousMusicVolume = SaveManager.Instance.GetPreviousMusicVolume();
        }
        else
        {
            m_previousMusicVolume = PlayerPrefs.GetFloat("PreviousMusicVolume");
        }

        m_musicSlider.value = m_previousMusicVolume;
        SetMusic();
    }

    public void MuteVFX()
    {
        m_previousVFXVolume = m_vfxSlider.value;
        m_vfxSlider.value = 0;
        SetVFX();

        // Lưu giá trị volume trước khi mute vào SaveManager
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SavePreviousVFXVolume(m_previousVFXVolume);
        }
        else
        {
            PlayerPrefs.SetFloat("PreviousVFXVolume", m_previousVFXVolume);
        }
    }

    public void UnmuteVFX()
    {
        // Kiểm tra giá trị volume trước khi mute từ SaveManager
        if (SaveManager.Instance != null)
        {
            m_previousVFXVolume = SaveManager.Instance.GetPreviousVFXVolume();
        }
        else
        {
            m_previousVFXVolume = PlayerPrefs.GetFloat("PreviousVFXVolume");
        }

        m_vfxSlider.value = m_previousVFXVolume;
        SetVFX();
    }

    public float GetMusicVolume()
    {
        return SaveManager.Instance.GetMusicVolume();
    }

    public float GetVFXVolume()
    {
        return SaveManager.Instance.GetVFXVolume();
    }
}
