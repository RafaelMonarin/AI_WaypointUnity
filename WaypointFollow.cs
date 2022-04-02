using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    // Array de gameobjects waypoints, vari�vel que ser� o valor do waypoint atual para seguir.
    public GameObject[] waypoints;
    int currentWP = 0;

    // Declar��o das vari�veis de: Velocidade, Precis�o (para verificar qu�o perto est� do alvo) e velocidade de rota��o.
    float speed = 1;
    float accuracy = 1;
    float rotSpeed = .4f;

    // Start() � chamado assim que inicia. 
    private void Start()
    {
        // Pega todos os gameobjects na cena com a tag "Waypoint" e adiciona ao array "waypoint[]"
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }

    // ULatepdate() � chamado depois de todas as fun��es serem chamadas.
    private void LateUpdate()
    {
        // Se o tamanho do array "waypoint" for igual a 0, retorna.
        if (waypoints.Length == 0)
        {
            return;
        }

        // Declara��o de uma vari�vel Vector3 "lookAtGoal", atribuindo a ela, a posi��o X e Z do waypoint atual "waypoints[currentWP]", e a posi��o Y igual a do pr�prio objeto com esse script.
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y, waypoints[currentWP].transform.position.z);

        // Declara��o de uma vari�vel Vector3 "direction", atribuindo a ela, a posi��o do "lookAtGoal" (waypoint atual) menos a posi��o do pr�prio objeto com esse script.
        // Rotaciona o objeto para a dire��o "direction" declarada anteriormente.
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        // Se a magnitude da vari�vel "direction" for menor que o valor da vari�vel accuracy (quando estiver perto do objeto).
        if (direction.magnitude < accuracy)
        {
            // Adiciona 1 a vari�vel "currentWP" que � o indice do array "waypoint[]".
            currentWP++;
            // Se o valor do "currentWp" for maior ou igual ao tamanho do array "waypoint[]".
            if (currentWP >= waypoints.Length)
            {
                // Reseta o valor da vari�vel "currentWP".
                currentWP = 0;
            }
        }

        // Move o objeto.
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}