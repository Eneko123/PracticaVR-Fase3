using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    static bool isDoubleSableActive = true;
    [SerializeField] Text textForSableOption;
    public static UI instance;
    //Se hace singelton y se presentan parametos de por defecto
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        textForSableOption.text = "Modo 2 sables";
        
    }
    private void Start()
    {
        Puntuation.instance.SetMaxPuntuation(10);
    }
    //Cambia el modo de jugar con una espada o dos
    public void ChangeSableOption()
    {
        if (isDoubleSableActive)
        {
            textForSableOption.text = "Modo 1 sable";
            isDoubleSableActive = false;
        }
        else if (!isDoubleSableActive)
        {
            textForSableOption.text = "Modo 2 sables";
            isDoubleSableActive = true;
        }

    }
    //Devuelve si se ha activado el modo una espada o no
    public bool CheckSableMode()
    {
        return isDoubleSableActive;
    }
    //Sirve para cambiar cual sera el maximo de puntos
    public void ChangeMaxPoints(int value)
    {
        switch (value)
        {
            case 0:
                Puntuation.instance.SetMaxPuntuation(10);
                break;
            case 1:
                Puntuation.instance.SetMaxPuntuation(20);
                break;
            case 2:
                Puntuation.instance.SetMaxPuntuation(30);
                break;
        }
    }
}
