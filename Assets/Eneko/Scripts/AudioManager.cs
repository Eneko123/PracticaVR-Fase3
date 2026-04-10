using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Música de Fondo")]
    public AudioClip menuMusic;
    public AudioClip easyLevelMusic;
    public AudioClip hardLevelMusic;
    public AudioClip arrowLevelMusic;

    private AudioSource _musicSource;
    private string currentSceneName;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Crear AudioSource para la musica
            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;
            _musicSource.playOnAwake = false;
            _musicSource.volume = 0.5f; // Ajusta el volumen al 50%
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // Reproducir musica de la escena inicial
        PlayMusicForCurrentScene();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        PlayMusicForCurrentScene();
    }

    private void PlayMusicForCurrentScene()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        AudioClip clipToPlay = null;

        // Determina que musica reproducir segun la escena
        switch (currentSceneName)
        {
            case "MenuMain":
            case "Menu":
                clipToPlay = menuMusic;
                break;

            case "Easy":
                clipToPlay = easyLevelMusic;
                break;

            case "Hard":
                clipToPlay = hardLevelMusic;
                break;

            case "Arrow":
            case "ArrowLevel":
                clipToPlay = arrowLevelMusic;
                break;

            default:
                Debug.LogWarning($"No hay musica asignada para la escena: {currentSceneName}");
                break;
        }

        // Solo cambiar la musica si es diferente a la actual
        if (clipToPlay != null && _musicSource.clip != clipToPlay)
        {
            _musicSource.Stop();
            _musicSource.clip = clipToPlay;
            _musicSource.Play();
        }
    }

    // Metodos publicos para controlar el volumen
    public void SetMusicVolume(float volume)
    {
        _musicSource.volume = Mathf.Clamp01(volume);
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    public void ResumeMusic()
    {
        _musicSource.UnPause();
    }
}
