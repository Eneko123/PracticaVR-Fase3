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

        Debug.Log("Juego reanudado");
    }

    // Para botones del menu
    public void ResumeGameButton()
    {
        ResumeGame();
    }

    public void RestartLevel()
    {
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
        Time.timeScale = 1f;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        if (gameplayUI != null)
            gameplayUI.SetActive(true);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuMain");
    }
}
