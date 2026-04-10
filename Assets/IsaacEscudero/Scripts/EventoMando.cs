using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EventoMandooo : MonoBehaviour
{
    [SerializeField] private InputActionReference menuActionReference;
    [SerializeField] GameObject UI;
    private void Awake()
    {
        UI.SetActive(false);
    }
    private void OnEnable()
    {
        // Suscribirse al evento 'performed' (cuando se pulsa el botón)
        menuActionReference.action.performed += OnMenuButtonPressed;
    }

    private void OnDisable()
    {
        // Desuscribirse para evitar errores de memoria
        menuActionReference.action.performed -= OnMenuButtonPressed;
    }

    private void OnMenuButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (UI.activeSelf)
            {
                UI.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                UI.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }
    public void Continue()
    {
        StartCoroutine(AudioManager.instance.PlayMenuButtonSound());
        UI.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void ExitToMenu()
    {
        StartCoroutine(AudioManager.instance.PlayExitMenuButtonSound());
    }
    public void ExitGame()
    {
        StartCoroutine(AudioManager.instance.PlayExitGameButtonSound());
    }
}
