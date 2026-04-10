using TMPro;
using UnityEngine;

public class Puntuation : MonoBehaviour
{
    //Texto para mostrar la puntuacion
    [SerializeField] TMP_Text puntuation;
    //Texto para guardar los puntos
    int points = 0;
    static int maxPoints;
    //Instacia del gestor de puntuacion
    public static Puntuation instance;

    //Singleton para este gestor
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    //Se inicializa la puntuacion por defecto a 0
    void Start()
    {
        if (puntuation != null)
        {
            puntuation.text = points.ToString();
        }
    }
    //Se llama a esta funcion para establecer la puntuacion
    public void SetPuntuationCubosNormales()
    {
        //Si el jugador tiene menos de veinte puntos 
        if (points < maxPoints)
        {
            //Se suma un punto y se actualiza la puntuacion por pantalla
            points++;
            puntuation.text = points.ToString();
        }
        else if (points < 0)
        {
            points = 0;
            puntuation.text = points.ToString();
        }
        else
        {
            //Si no, sw mostrara el cartel FIN y se desactivara el generador de cubos
            puntuation.text = "FIN";
            InstantiateCubes.instance.DeactivateSpawner();
        }

    }
    public void SetPuntuationCubosModoDificil()
    {
        //Si el jugador tiene menos de veinte puntos 
        if (points < maxPoints)
        {
            //Se resta un punto y se actualiza la puntuacion por pantalla
            points--;
            puntuation.text = points.ToString();
        }
        //Se usa para que el marcador no muestre menos puntos de cero
        else if (points < 0)
        {
            points = 0;
            puntuation.text = points.ToString();
        }
        else
        {
            //Si no, sw mostrara el cartel FIN y se desactivara el generador de cubos
            puntuation.text = "FIN";
            InstantiateCubes.instance.DeactivateSpawner();
        }

    }
    public void SetMaxPuntuation(int _maxPoints)
    {
        maxPoints = _maxPoints;
        Debug.Log(maxPoints);
    }
}
