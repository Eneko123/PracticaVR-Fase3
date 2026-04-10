using UnityEngine;
using TMPro;

public class Buttons : MonoBehaviour
{
    public GameObject leftSable;
    public GameObject rigthSable;
    Counter counter;
    public TMP_Text countText;
    public TMP_Text leftText;
    public TMP_Text rigthText;

    //private void Awake()
    //{
    //    leftSable.SetActive(false);
    //    rigthSable.SetActive(false);
    //}

    private void Start()
    {
        if (GameConfig.leftSableActive)
            leftText.text = "Yes";
        else
            leftText.text = "No";
        if (GameConfig.rightSableActive)
            rigthText.text = "Yes";
        else
            rigthText.text = "No";
    }

    public void ActiveDesactiveLeftSable()
    {
        Debug.Log("Funcion llamada");
        // Reproducir sonido de boton
        //if (SoundEffectsManager.Instance != null)
        //    SoundEffectsManager.Instance.PlayButton1Sound();

        GameConfig.leftSableActive = !GameConfig.leftSableActive;
        if (GameConfig.leftSableActive)
            leftText.text = "Yes";
        else
            leftText.text = "No";
    }

    public void ActiveDesactiveRigthSable()
    {
        Debug.Log("Funcion llamada");

        // Reproducir sonido de boton
        //if (SoundEffectsManager.Instance != null)
        //    SoundEffectsManager.Instance.PlayButton1Sound();

        GameConfig.rightSableActive = !GameConfig.rightSableActive;
        if (GameConfig.rightSableActive)
            rigthText.text = "Yes";
        else
            rigthText.text = "No";
    }

    public void UpMax()
    {
        Debug.Log("Funcion llamada");

        // Reproducir sonido de boton
        //if (SoundEffectsManager.Instance != null)
        //    SoundEffectsManager.Instance.PlayButton2Sound();

        GameConfig.counterMax++;
    }

    public void DownMax()
    {
        Debug.Log("Funcion llamada");

        //if (SoundEffectsManager.Instance != null)
        //    SoundEffectsManager.Instance.PlayButton2Sound();

        if (GameConfig.counterMax > 1)
            GameConfig.counterMax--;
    }

    private void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        countText.text = GameConfig.counterMax.ToString();
    }
}
