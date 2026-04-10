using UnityEngine;
using UnityEngine.UI;

public class CubesFlechas : Cubes
{
    [SerializeField] Collider arriba;
    [SerializeField] Collider abajo;
    [SerializeField] Collider derecha;
    [SerializeField] Collider izquierda;
    [SerializeField] Sprite flechaArriba;
    [SerializeField] Sprite flechaAbajo;
    [SerializeField] Sprite flechaDerecha;
    [SerializeField] Sprite flechaIzquierda;  
    [SerializeField] SpriteRenderer flecha;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(true);
        float probability = Random.value;
        if (0 <= probability && probability < 0.25f)
        {
            arriba.gameObject.SetActive(true);
            abajo.gameObject.SetActive(false);
            derecha.gameObject.SetActive(false);
            izquierda.gameObject.SetActive(false);
            flecha.sprite = flechaAbajo;
        }
        else if (0.25f <= probability && probability < 0.50f)
        {
            arriba.gameObject.SetActive(false);
            abajo.gameObject.SetActive(true);
            derecha.gameObject.SetActive(false);
            izquierda.gameObject.SetActive(false);
            flecha.sprite = flechaArriba;
        }
        else if (0.50f <= probability && probability < 0.75f)
        {
            arriba.gameObject.SetActive(false);
            abajo.gameObject.SetActive(false);
            derecha.gameObject.SetActive(true);
            izquierda.gameObject.SetActive(false);
            flecha.sprite = flechaIzquierda;
        }
        else if (0.75 <= probability && probability <= 1f)
        {
            arriba.gameObject.SetActive(false);
            abajo.gameObject.SetActive(false);
            derecha.gameObject.SetActive(false);
            izquierda.gameObject.SetActive(true);
            flecha.sprite = flechaDerecha;
        }
    }
}
