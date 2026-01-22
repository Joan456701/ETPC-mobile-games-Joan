using System.Threading;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    //Referencia al Box Collider del objeto
    private BoxCollider boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Comprueba si el objeto que ha entrado en el trigger es el player para activar la logica
        if (other.CompareTag("Player"))
        {
            MagnetPowerUp magnetPower = other.GetComponentInChildren<MagnetPowerUp>();
            Destroy(this.gameObject);
            magnetPower.ActiveMagnet();
        }
    }
}
