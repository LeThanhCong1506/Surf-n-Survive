using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Singleton instance
    public static SaveManager Instance { get; private set; }

    // Các khóa cho PlayerPrefs
    private const string HighScoreKey = "HighScore";
    private const string MusicVolumeKey = "MusicVolume";
    private const string VFXVolumeKey = "VFXVolume";
    private const string PreviousMusicVolumeKey = "PreviousMusicVolume";
    private const string PreviousVFXVolumeKey = "PreviousVFXVolume";

    private void Awake()
    {
        // Đảm bảo chỉ có một instance của SaveManager tồn tại
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

    // Phương thức để lưu một giá trị bool
    public void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetString(key, value.ToString());
    }

    // Phương thức để lấy một giá trị bool
    public string GetBool(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    // Phương thức để lưu điểm cao
    public void SaveHighScore(int score)
    {
        int currentHighScore = GetHighScore();
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
        }
    }

    // Phương thức để lấy điểm cao
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    // Phương thức để lưu âm lượng nhạc
    public void SaveMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    // Phương thức để lấy âm lượng nhạc
    public float GetMusicVolume()
    {
        return PlayerPrefs.HasKey(MusicVolumeKey) ? PlayerPrefs.GetFloat(MusicVolumeKey) : 0.75f;
    }

    // Phương thức để lưu âm lượng hiệu ứng âm thanh
    public void SaveVFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(VFXVolumeKey, volume);
    }

    // Phương thức để lấy âm lượng hiệu ứng âm thanh
    public float GetVFXVolume()
    {
        return PlayerPrefs.HasKey(VFXVolumeKey) ? PlayerPrefs.GetFloat(VFXVolumeKey) : 0.75f;
    }

    // Phương thức để xóa tất cả dữ liệu đã lưu (nếu cần)
    public void ClearAllSavedData()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnApplicationQuit()
    {
        // Lưu lại tất cả dữ liệu quan trọng trước khi thoát ứng dụng
        Debug.Log("Saving all data before application quit...");

        // Đảm bảo PlayerPrefs được lưu
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
