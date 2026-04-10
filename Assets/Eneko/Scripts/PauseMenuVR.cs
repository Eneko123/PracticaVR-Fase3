using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuVR : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseMenuUI;
    public GameObject gameplayUI;

    [Header("Input")]
    [SerializeField] private InputActionReference menuActionReference;

    private bool isPaused = false;
    private AudioSource musicSource;
    private Spawner spawners;

    void Start()
    {
        // Encontrar componentes
        musicSource = GetComponent<AudioSource>();
        spawners = GetComponent<Spawner>();

        // Inicializar UI
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        if (gameplayUI != null)
            gameplayUI.SetActive(true);
    }

    private void OnEnable()
    {
        if (menuActionReference != null)
        {
            menuActionReference.action.Enable();
            menuActionReference.action.performed += OnMenuButtonPressed;
        }
    }

    private void OnDisable()
    {
        if (menuActionReference != null)
        {
            menuActionReference.action.performed -= OnMenuButtonPressed;
        }
    }

    private void OnMenuButtonPressed(InputAction.CallbackContext context)
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    void Update()
    {
        // Tecla ESC para testing en editor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);

        if (gameplayUI != null)
            gameplayUI.SetActive(false);

        // Pausar musica
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Pause();

        // Desactivar spawners
        if (spawners != null)
            spawners.enabled = false;
        

        Debug.Log("Juego pausado");
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        if (gameplayUI != null)
            gameplayUI.SetActive(true);

        // Reanudar musica
        if (musicSource != null)
            musicSource.UnPause();

        // Reactivar spawners
        if (spawners != null)
            spawners.enabled = true;
        

        Debug.Log("Juego reanudado");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuMain");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}