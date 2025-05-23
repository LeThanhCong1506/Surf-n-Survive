using UnityEngine;

/// <summary>
/// Manages saving and loading of game data using PlayerPrefs.
/// Provides methods to save and retrieve high scores, volume settings, and other game-related data.
/// Ensures data persistence across game sessions.
/// </summary>
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private const string HighScoreKey = "HighScore";
    private const string MusicVolumeKey = "MusicVolume";
    private const string VFXVolumeKey = "VFXVolume";
    private const string PreviousMusicVolumeKey = "PreviousMusicVolume";
    private const string PreviousVFXVolumeKey = "PreviousVFXVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetString(key, value.ToString());
    }

    public string GetBool(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public void SaveHighScore(int score)
    {
        int currentHighScore = GetHighScore();
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public void SaveMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.HasKey(MusicVolumeKey) ? PlayerPrefs.GetFloat(MusicVolumeKey) : 0.75f;
    }

    public void SaveVFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(VFXVolumeKey, volume);
    }

    public float GetVFXVolume()
    {
        return PlayerPrefs.HasKey(VFXVolumeKey) ? PlayerPrefs.GetFloat(VFXVolumeKey) : 0.75f;
    }

    public void ClearAllSavedData()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Saving all data before application quit...");

        PlayerPrefs.Save();
    }

    public void SavePreviousMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(PreviousMusicVolumeKey, volume);
    }

    public float GetPreviousMusicVolume()
    {
        return PlayerPrefs.GetFloat(PreviousMusicVolumeKey, 1.0f);
    }

    public void SavePreviousVFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(PreviousVFXVolumeKey, volume);
    }

    public float GetPreviousVFXVolume()
    {
        return PlayerPrefs.GetFloat(PreviousVFXVolumeKey, 1.0f);
    }
}
