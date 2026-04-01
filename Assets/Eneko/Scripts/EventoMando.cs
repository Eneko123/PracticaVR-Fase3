using UnityEngine;
using UnityEngine.InputSystem;

public class EventoMando : MonoBehaviour
{
    [SerializeField] private InputActionReference menuActionReference;

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
        Debug.Log("¡Botón Menú pulsado!");
        // Aquí tu lógica para abrir/cerrar el menú
    }
}
