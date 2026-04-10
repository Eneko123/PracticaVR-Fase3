using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager Instance { get; private set; }

    [Header("Sonidos de Proyectiles")]
    public AudioClip arrowSound;
    public AudioClip normalSound;
    public AudioClip boomSound;

    [Header("Sonidos de Botones")]
    public AudioClip button1Sound;
    public AudioClip button2Sound;

    private AudioSource sfxSource;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Crear AudioSource para efectos de sonido
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSource.loop = false;
            sfxSource.volume = 0.7f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Reproducir sonido de proyectil segun el tipo
    public void PlayProjectileSound(Proyectil.Type projectileType)
    {
        AudioClip clipToPlay = null;

        switch (projectileType)
        {
            case Proyectil.Type.Arrow:
                clipToPlay = arrowSound;
                break;
            case Proyectil.Type.Normal:
                clipToPlay = normalSound;
                break;
            case Proyectil.Type.Bomb:
                clipToPlay = boomSound;
                break;
        }

        if (clipToPlay != null)
        {
            sfxSource.PlayOneShot(clipToPlay);
        }
    }

    // Reproducir sonido de botón 1
    public void PlayButton1Sound()
    {
        if (button1Sound != null)
        {
            sfxSource.PlayOneShot(button1Sound);
        }
    }

    // Reproducir sonido de boton 2
    public void PlayButton2Sound()
    {
        if (button2Sound != null)
        {
            sfxSource.PlayOneShot(button2Sound);
        }
    }

    // Metodo generico para reproducir cualquier clip
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // Controlar volumen de efectos de sonido
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }
}
