using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer m_myMixer;
    [SerializeField] private Slider m_musicSlider;
    [SerializeField] private Slider m_vfxSlider;

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
        m_musicSlider.value = 0;
        SetMusic();
    }

    public void MuteVFX()
    {
        m_vfxSlider.value = 0;
        SetVFX();
    }
}
