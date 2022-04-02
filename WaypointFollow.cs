using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    // Array de gameobjects waypoints, variável que será o valor do waypoint atual para seguir.
    public GameObject[] waypoints;
    int currentWP = 0;

    // Declarção das variáveis de: Velocidade, Precisão (para verificar quão perto está do alvo) e velocidade de rotação.
    float speed = 1;
    float accuracy = 1;
    float rotSpeed = .4f;

    // Start() é chamado assim que inicia. 
    private void Start()
    {
        // Pega todos os gameobjects na cena com a tag "Waypoint" e adiciona ao array "waypoint[]"
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    // ULatepdate() é chamado depois de todas as funções serem chamadas.
    private void LateUpdate()
    {
        // Se o tamanho do array "waypoint" for igual a 0, retorna.
        if (waypoints.Length == 0)
        {
            return;
        }

        // Declaração de uma variável Vector3 "lookAtGoal", atribuindo a ela, a posição X e Z do waypoint atual "waypoints[currentWP]", e a posição Y igual a do próprio objeto com esse script.
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y, waypoints[currentWP].transform.position.z);

        // Declaração de uma variável Vector3 "direction", atribuindo a ela, a posição do "lookAtGoal" (waypoint atual) menos a posição do próprio objeto com esse script.
        // Rotaciona o objeto para a direção "direction" declarada anteriormente.
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        // Se a magnitude da variável "direction" for menor que o valor da variável accuracy (quando estiver perto do objeto).
        if (direction.magnitude < accuracy)
        {
            // Adiciona 1 a variável "currentWP" que é o indice do array "waypoint[]".
            currentWP++;
            // Se o valor do "currentWp" for maior ou igual ao tamanho do array "waypoint[]".
            if (currentWP >= waypoints.Length)
            {
                // Reseta o valor da variável "currentWP".
                currentWP = 0;
            }
        }

        // Move o objeto.
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}