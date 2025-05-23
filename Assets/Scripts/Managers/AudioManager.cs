using UnityEngine;

/// <summary>
/// Manages all audio-related functionality in the game, including background music,
/// sound effects, and button click sounds. Ensures a singleton instance for global access.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip[] BackgroundMenuGame;
    public AudioClip[] BackgroundGameSound;
    public AudioClip ButtonSound;
    public AudioClip Jump;
    public AudioClip Bend_Down;
    public AudioClip Hit_SpeedUp;
    public AudioClip Death;

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

    public void PlayBendDownSound()
    {
        sfxSource.PlayOneShot(Bend_Down);
    }

    public void Pause()
    {
        musicSource.clip = null;
    }

    public void PlayJumpSound()
    {
        sfxSource.PlayOneShot(Jump);
    }

    public void PlayHitSpeedUpSound()
    {
        sfxSource.PlayOneShot(Hit_SpeedUp);
    }

    public void PlayDeathSound()
    {
        sfxSource.PlayOneShot(Death);
    }

    private void Start()
    {
        PlayBackgroundMenuGame();
    }

    public void PlayButtonSound()
    {
        sfxSource.PlayOneShot(ButtonSound);
    }

    public void PlayBackgroundMenuGame()
    {
        int randomIndex = Random.Range(0, BackgroundMenuGame.Length);
        musicSource.clip = BackgroundMenuGame[randomIndex];
        musicSource.Play();
    }

    public void PlayBackgroundMusicGame()
    {
        int randomIndex = Random.Range(0, BackgroundGameSound.Length);
        musicSource.clip = BackgroundGameSound[randomIndex];
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
