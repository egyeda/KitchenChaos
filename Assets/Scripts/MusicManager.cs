using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    public static MusicManager Instance { get; private set; }


    private AudioSource audioSource;
    private float volume = 0.5f;


    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.5f);
        audioSource.volume = volume;
    }

    public void IncreaseVolume()
    {
        volume += 0.1f;
        if (volume > 1f)
        {
            volume = 0.1f;
        }

        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
    }

	internal float GetVolume()
	{
		return volume;
	}
}