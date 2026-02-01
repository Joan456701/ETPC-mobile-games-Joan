using System.Collections;
using UnityEngine;

public class DoubleCoinsController : MonoBehaviour
{
    //Referencia al Player Controller
    PlayerController pController;


    private void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        pController = obj.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Comprueba si el objeto que ha entrado en el trigger es el player para activar la logica
        if (other.CompareTag("Player"))
        {
            pController.doubleCoins = true; //Activa el multiplicador de monedas x2
            pController.TimeDoubleCoin(); //Inicia el temporizador del power-up
            Destroy(this.gameObject); //Destruye el objeto del power-up
        }
    }


}

