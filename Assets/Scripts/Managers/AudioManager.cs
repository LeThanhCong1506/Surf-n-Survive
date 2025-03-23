using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager Instance { get; private set; }

    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip[] BackgroundMusicGame;
    public AudioClip BackgroundMenu;
    //public AudioClip BackgroundGame;
    //public AudioClip Jump;
    //public AudioClip Bend_Down;
    //public AudioClip Hit_SpeedUp;
    //public AudioClip Death;

    private void Awake()
    {
        // Đảm bảo chỉ có một instance của AudioManager tồn tại
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

    private void Start()
    {
        PlayBackgroundMenu();
    }

    public void PlayBackgroundMenu()
    {
        musicSource.clip = BackgroundMenu;
        musicSource.Play();
    }

    public void PlayBackgroundMusicGame()
    {
        int randomIndex = Random.Range(0, BackgroundMusicGame.Length);
        musicSource.clip = BackgroundMusicGame[randomIndex];
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
