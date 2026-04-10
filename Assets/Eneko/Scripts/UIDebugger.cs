using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Script de diagnóstico para identificar problemas con botones UI que no responden
/// Adjunta este script a tu Canvas para ver qué falta
/// </summary>
public class UIDebugger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("=== DIAGNÓSTICO DE UI ===");

        // 1. Verificar Canvas
        Canvas canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("❌ No hay Canvas en este GameObject!");
        }
        else
        {
            Debug.Log($"✓ Canvas encontrado - Render Mode: {canvas.renderMode}");

            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                Debug.LogWarning("⚠️ El Canvas está en Screen Space - Overlay. Para VR debería ser World Space!");
            }
            else if (canvas.renderMode == RenderMode.WorldSpace)
            {
                Debug.Log("✓ Canvas en World Space (correcto para VR)");
            }
        }

        // 2. Verificar GraphicRaycaster
        GraphicRaycaster raycaster = GetComponent<GraphicRaycaster>();
        if (raycaster == null)
        {
            Debug.LogError("❌ No hay GraphicRaycaster en el Canvas! Los botones NO funcionarán.");
            Debug.Log("SOLUCIÓN: Añade el componente 'GraphicRaycaster' al Canvas");
        }
        else
        {
            Debug.Log($"✓ GraphicRaycaster encontrado - Blocking Objects: {raycaster.blockingObjects}");
        }

        // 3. Verificar EventSystem en la escena
        EventSystem eventSystem = FindAnyObjectByType<EventSystem>();
        if (eventSystem == null)
        {
            Debug.LogError("❌ No hay EventSystem en la escena! Los eventos UI NO funcionarán.");
            Debug.Log("SOLUCIÓN: Crea un GameObject vacío y añádele los componentes 'EventSystem' y 'XRUIInputModule'");
        }
        else
        {
            Debug.Log("✓ EventSystem encontrado en la escena");

            // Verificar si tiene XRUIInputModule (necesario para VR)
            var xrModule = eventSystem.GetComponent<UnityEngine.XR.Interaction.Toolkit.UI.XRUIInputModule>();
            if (xrModule == null)
            {
                Debug.LogWarning("⚠️ No hay XRUIInputModule. Para VR necesitas este componente.");
                Debug.Log("SOLUCIÓN: Añade el componente 'XRUIInputModule' al EventSystem");
            }
            else
            {
                Debug.Log("✓ XRUIInputModule encontrado (correcto para VR)");
            }
        }

        // 4. Verificar botones
        Button[] buttons = GetComponentsInChildren<Button>(true);
        Debug.Log($"Botones encontrados en el Canvas: {buttons.Length}");

        foreach (Button btn in buttons)
        {
            Debug.Log($"  - Botón: {btn.gameObject.name}");
            Debug.Log($"    Interactable: {btn.interactable}");
            Debug.Log($"    Eventos onClick: {btn.onClick.GetPersistentEventCount()}");

            if (btn.onClick.GetPersistentEventCount() == 0)
            {
                Debug.LogWarning($"    ⚠️ El botón '{btn.gameObject.name}' NO tiene funciones asignadas en onClick!");
            }

            // Verificar si el botón tiene algún componente bloqueando raycast
            if (!btn.GetComponent<Graphic>().raycastTarget)
            {
                Debug.LogWarning($"    ⚠️ El botón '{btn.gameObject.name}' tiene 'Raycast Target' desactivado!");
            }
        }

        Debug.Log("=== FIN DEL DIAGNÓSTICO ===");
    }
}