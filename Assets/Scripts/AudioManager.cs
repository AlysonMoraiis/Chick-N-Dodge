using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [SerializeField] private Button _muteAndUnmuteButton;

    private void OnEnable()
    {
        _muteAndUnmuteButton.onClick.AddListener(MuteAndUnmute);
    }

    private void OnDisable()
    {
        _muteAndUnmuteButton.onClick.RemoveListener(MuteAndUnmute);
    }

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

    public void PlayMusic(AudioClip clip, float volume = 1f, bool loop = true)
    {
        _musicSource.clip = clip;
        _musicSource.volume = volume;
        _musicSource.loop = loop;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        _sfxSource.PlayOneShot(clip, volume);
    }

    public void MuteAndUnmute()
    {
        _musicSource.mute = !_musicSource.mute;
        _sfxSource.mute = !_sfxSource.mute;
    }
}
