using UnityEngine;

namespace Audio
{
    public sealed class SoundSingleton : MonoBehaviour
    {
        public static SoundSingleton Instance;
        [SerializeField] private AudioSource[] audioSounds;

        public AudioSource GetLaserGunSound { get => audioSounds[0]; }
        public AudioSource GetMachineGunSound { get => audioSounds[1]; }
        public AudioSource GetLuckSound { get => audioSounds[2]; }
        public AudioSource GetUnlockSound { get => audioSounds[3]; }
        public AudioSource GetExplosion { get => audioSounds[4]; }

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
}