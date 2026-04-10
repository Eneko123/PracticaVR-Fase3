using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PushButtonSceneLoader : MonoBehaviour
{
    public string sceneToLoad; // Configurar en el Inspector
    public bool useButton1Sound = true; // true = button1, false = button2

    private XRBaseInteractable interactable;

    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();

        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnButtonPressed);
        }
    }

    void OnButtonPressed(SelectEnterEventArgs args)
    {

        // Reproducir sonido de boton
        if (SoundEffectsManager.Instance != null)
        {
            if (useButton1Sound)
                SoundEffectsManager.Instance.PlayButton1Sound();
            else
                SoundEffectsManager.Instance.PlayButton2Sound();
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnButtonPressed);
        }
    }
}
