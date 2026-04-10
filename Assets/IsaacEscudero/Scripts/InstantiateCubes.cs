using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;

public class InstantiateCubes : MonoBehaviour
{
    //Referencia al jugador
    [SerializeField] protected Transform player;
    //Referencia a los cubos que debe de instanciar
    [SerializeField] protected GameObject cubes;
    //Se usa como offset
    protected float destinationOffsetRange;
    //Referencia para la posicion del spawner
    [SerializeField] protected Transform spawnPos;
    //Instancia para el singelton
    public static InstantiateCubes instance;
    //Se utiliza para controlar la generacion de los cubos
    protected bool isSpawning = true;
    //Se usa para mantener que los cubos se spawneen en una determinada Y
    protected float originalY;
    
    //Singelton para la creacion de cubos
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
    void Start()
    {
        //Se asigna el offset
        destinationOffsetRange = 5;
        //Se guarda la posicion Y del generador 
        originalY = spawnPos.transform.position.y;
        //Comienza la corrutina para generar cubos
        StartCoroutine(InstantianteDelay());
    }
    public virtual IEnumerator InstantianteDelay()
    {
        //Mientras se mantenga activo el generador de cubos
        while (isSpawning && Time.timeScale != 0)
        {
            //Se creara un offset
            float offset = Random.Range(-destinationOffsetRange, destinationOffsetRange);
            //Se le dará una direccion a los cubos en la que aparecer
            Vector3 direction = (new Vector3(Camera.main.transform.position.x + offset, Camera.main.transform.position.y + offset, Camera.main.transform.position.z + offset) - spawnPos.position).normalized;
            //Se le dara una posicion donde el cubo deba aparecer
            Vector3 cubePosition =
                new Vector3(spawnPos.position.x + direction.x + offset, originalY + direction.y, spawnPos.position.z);
            //Se crea un objeto cubo
            GameObject c = Instantiate(cubes, cubePosition, Quaternion.identity);
            //Se calcula la rotacion del cubo sin afectar la rotacion en Y para que el cubo se diriga hacia el jugador
            Vector3 playerdirection = player.transform.position - c.transform.position; 
            playerdirection.y = 0;
            c.transform.rotation = Quaternion.LookRotation(playerdirection);
            //Se generara cada dos segundos
            yield return new WaitForSeconds(2);

        }
    }
    //Funcion utilizada para desactivar este generador de cubos
    public void DeactivateSpawner()
    {
        isSpawning = false;
        StopCoroutine(InstantianteDelay());
        this.enabled = false;
    }
}
