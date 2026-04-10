using UnityEngine;
using System.Collections;

public class HardModeInstantiateCubes : InstantiateCubes
{
    [SerializeField] GameObject bombCube;
    float bombProbability = 0.20f;
    public override IEnumerator InstantianteDelay()
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
            //Se crea un objeto vacio para que estalle la bomba
            GameObject c;
            if (Random.value <= bombProbability)
            {
                Debug.Log("Bomba");
                //Se crea un cubo bomba
                c = Instantiate(bombCube, cubePosition, Quaternion.identity);

            }
            else
            {
                Debug.Log("Normal");
                //Se crea un objeto cubo
                c = Instantiate(cubes, cubePosition, Quaternion.identity);
            }
                //Se calcula la rotacion del cubo sin afectar la rotacion en Y para que el cubo se diriga hacia el jugador
                Vector3 playerdirection = player.transform.position - c.transform.position;
            playerdirection.y = 0;
            c.transform.rotation = Quaternion.LookRotation(playerdirection);
            //Se generara cada dos segundos
            yield return new WaitForSeconds(2);

        }
    }
}
