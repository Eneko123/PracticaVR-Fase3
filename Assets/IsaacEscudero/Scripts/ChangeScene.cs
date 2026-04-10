using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //Se usa el ID de la escena (Escribirla en el inspector)
    [SerializeField] int sceneID;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject AudioManager;

    //Se cambia a la escena sugerida
    public void OnSceneChange()
    {
        DontDestroyOnLoad(Menu);
        DontDestroyOnLoad(AudioManager);
        SceneManager.LoadScene(sceneID);
    }
}
