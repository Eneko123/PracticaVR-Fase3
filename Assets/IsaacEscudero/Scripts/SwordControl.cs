using System.Collections;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwordControl : MonoBehaviour
{
    int layer = 8;
    LayerMask cubeFlechaLayer;
    [SerializeField] GameObject leftSable;
    bool chronometerON = false;
    //Sirve comprobar si se ha activado el modo dos espadas o una espada
    private void Start()
    {
        cubeFlechaLayer = 1 << layer;
        if (leftSable != null)
        {
            if (!UI.instance.CheckSableMode())
            {
                leftSable.SetActive(false);
            }
            else
            {
                leftSable.SetActive(true);
            }


        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (!chronometerON)
        {
            if (collision.gameObject.TryGetComponent(out DetectorSideCube dC))
            {
                dC.gameObject.GetComponentInParent<Collider>().enabled = false;
                AudioManager.instance.PlayGoodCutSound();
                //Se llama a la funcion para actualizar la puntuacion
                Puntuation.instance.SetPuntuationCubosNormales();
                //Se destruye el cubo
                dC.gameObject.GetComponentInParent<CubesFlechas>().DestroyCube();

            }
            //Si entra en colision con un objeto que tenga el script Cubes 
            else if (collision.gameObject.TryGetComponent(out Cubes c))
            {
                if (c as CubesFlechas)
                {
                    AudioManager.instance.PlayWrongCutSound();
                    c.DestroyCube();
                }
                if (c as HardModeCubes)
                {
                    AudioManager.instance.PlayBombSound();
                    //Se llama a la funcion para actualizar la puntuacion
                    Puntuation.instance.SetPuntuationCubosModoDificil();
                    //Se destruye el cubo
                    c.DestroyCube();
                }
                else if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
                {
                    AudioManager.instance.PlayGoodCutSound();
                    //Se llama a la funcion para actualizar la puntuacion
                    Puntuation.instance.SetPuntuationCubosNormales();
                    //Se destruye el cubo
                    c.DestroyCube();
                }
            }


        }

    }
    IEnumerator DelayForCut()
    {
        chronometerON = true;
        yield return new WaitForSeconds(0.001f);
        chronometerON = false;
    }
}
