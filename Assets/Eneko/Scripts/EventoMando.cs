using UnityEngine;
using UnityEngine.InputSystem;

public class EventoMando : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameplayUI;

    [Header("Input")]
    [SerializeField] private InputActionReference menuActionReference;

    private bool isPaused = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
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
        // Reproducir sonido al abrir/cerrar menu
        if (SoundEffectsManager.Instance != null)
            SoundEffectsManager.Instance.PlayButton2Sound();

        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);

        if (gameplayUI != null)
            gameplayUI.SetActive(false);

        // Pausar música de fondo
        if (AudioManager.Instance != null)
            AudioManager.Instance.PauseMusic();

        Debug.Log("Juego pausado");
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        if (gameplayUI != null)
            gameplayUI.SetActive(true);

        // Reanudar musica de fondo
        if (AudioManager.Instance != null)
            AudioManager.Instance.ResumeMusic();

        Debug.Log("Juego reanudado");
    }

    // Para botones del menu
    public void ResumeGameButton()
    {
        // Reproducir sonido de boton
        if (SoundEffectsManager.Instance != null)
            SoundEffectsManager.Instance.PlayButton1Sound();

        ResumeGame();
    }

    public void RestartLevel()
    {
        // Reproducir sonido de boton
        if (SoundEffectsManager.Instance != null)
            SoundEffectsManager.Instance.PlayButton2Sound();

        Time.timeScale = 1f;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        if (gameplayUI != null)
            gameplayUI.SetActive(true);

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void GoToMainMenu()
    {
        // Reproducir sonido de boton
        if (SoundEffectsManager.Instance != null)
            SoundEffectsManager.Instance.PlayButton2Sound();

        Time.timeScale = 1f;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        if (gameplayUI != null)
            gameplayUI.SetActive(true);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuMain");
    }
}
