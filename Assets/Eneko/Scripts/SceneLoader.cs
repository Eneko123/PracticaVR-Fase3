using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadEasyLevel()
    {
        // Reproducir sonido de boton
        if (SoundEffectsManager.Instance != null)
            SoundEffectsManager.Instance.PlayButton1Sound();

        SceneManager.LoadScene("Easy");
    }

    public void LoadHardLevel()
    {
        // Reproducir sonido de boton
        if (SoundEffectsManager.Instance != null)
            SoundEffectsManager.Instance.PlayButton1Sound();

        SceneManager.LoadScene("Hard");
    }

    public void LoadArrowLevel()
    {
        // Reproducir sonido de boton
        if (SoundEffectsManager.Instance != null)
            SoundEffectsManager.Instance.PlayButton1Sound();

        SceneManager.LoadScene("Arrow");
    }

    public void LoadMainMenu()
    {
        // Reproducir sonido de boton
        if (SoundEffectsManager.Instance != null)
            SoundEffectsManager.Instance.PlayButton2Sound();

        SceneManager.LoadScene("MenuMain");
    }
}
