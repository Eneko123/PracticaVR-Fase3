using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField]AudioSource audioSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField]AudioClip buttonClick;
    [SerializeField] AudioClip wrongCut;
    [SerializeField] AudioClip goodCut;
    [SerializeField] AudioClip bomb;

    [SerializeField] AudioClip MenuVRMusic;
    [SerializeField] AudioClip NivelFacilMusic;
    [SerializeField] AudioClip NivelDificilMusic;
    [SerializeField] AudioClip NivelFlechasMusic;
    public static AudioManager instance;
    [SerializeField] GameObject Menu;

    private void Awake()
    {
        musicSource.Stop();
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                musicSource.clip = MenuVRMusic;
                musicSource.Play();
                break;
            case 1:
                musicSource.clip = NivelFacilMusic;
                musicSource.Play();
                break;
            case 2:
                musicSource.clip = NivelDificilMusic;
                musicSource.Play();
                break;
            case 3:
                musicSource.clip = NivelFlechasMusic;
                musicSource.Play();
                break;

        }
    }

    public IEnumerator PlayMenuButtonSound()
    {
        audioSource.PlayOneShot(buttonClick);
        yield return new WaitUntil(() => !audioSource.isPlaying);
        Debug.Log("Termino el audio");
    }
    public IEnumerator PlayExitMenuButtonSound()
    {
        audioSource.PlayOneShot(buttonClick);
        yield return new WaitUntil(() => !audioSource.isPlaying);
        DontDestroyOnLoad(Menu);
        DontDestroyOnLoad(this);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    public IEnumerator PlayExitGameButtonSound()
    {
        audioSource.PlayOneShot(buttonClick);
        yield return new WaitUntil(() => !audioSource.isPlaying);
        Application.Quit();
    }
    public void PlayGoodCutSound()
    {
        audioSource.PlayOneShot(goodCut);
    }
    public void PlayWrongCutSound()
    {
        audioSource.PlayOneShot(wrongCut);
    }
    public void PlayBombSound()
    {
        audioSource.PlayOneShot(bomb);
    }
}
