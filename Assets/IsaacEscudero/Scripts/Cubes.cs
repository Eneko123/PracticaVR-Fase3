using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
public class Cubes : MonoBehaviour
{
    //Se usa para en caso de que el cubo pueda morir despues de varios segundos activados
    [SerializeField] float durationTime = 3;
    //Comprueba si el cubo esta vivo y para controlar el temporizador de vida
    bool isAlive = false;
    //Velocidad a la que se mueve el cubo
    float cubeSpeed = 4;
    
    //Se activa el objeto una vez creado
    void Start()
    {
        this.gameObject.SetActive(true);
    }

    void Update()
    {
        //En cada frame se movera en su eje forward 
        transform.position += transform.forward * cubeSpeed * Time.deltaTime;
        //Si no esta vivo y tiene tiempo de vida
        if (!isAlive && durationTime > 0)
        {
            //Se empieza a usar el cronometro de vida
            StartCoroutine(InstantianteDelay());
        }
        //Si el tiempo es menor o igual a 0 Se detiene la corrutina y se destruye el objeto
        else if (durationTime <= 0)
        {
            StopCoroutine(InstantianteDelay());
            DestroyCube();
        }
    }
    IEnumerator InstantianteDelay()
    {
        //Cada frame se disminuye el tiempo de vida del cubo
        durationTime -= Time.deltaTime;
        isAlive = true;
        yield return new WaitForSecondsRealtime(0.05f);
        isAlive = false;
    }
    //Se usa para destruir el cubo
    public void DestroyCube()
    {
        Destroy(this.gameObject);
    }
}
