using UnityEngine;

public class SoundSingleton : MonoBehaviour
{
    public static SoundSingleton Instance;
    [SerializeField] private AudioSource[] audioSounds;

    public AudioSource GetGunSound { get => audioSounds[0]; }
    public AudioSource GetLuckSound { get => audioSounds[1]; }
    public AudioSource GetExplosion { get => audioSounds[2]; }

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
            return;
        }
    }
}
