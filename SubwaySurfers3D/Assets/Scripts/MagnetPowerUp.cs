using System.Collections;
using UnityEngine;

public class MagnetPowerUp : MonoBehaviour
{
    [HideInInspector]public BoxCollider boxCollider;
    private Coroutine activeCoroutine;

    public int timeOfPowerUp;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false; //Desactiva el box collider
    }

    private void OnTriggerEnter(Collider other)
    {
        //Comprueba si en el trigger entran objetos con el tag Coin
        //y les pasa su posicion para que vengan a el player
        if (other.CompareTag("Coin"))
        {
            CoinController coin = other.GetComponent<CoinController>();
            coin.SetMagnet(this.transform);
        }
    }
    public void ActiveMagnet()
    {
        boxCollider.enabled = true; //Activa el box collider

        if (activeCoroutine != null) //Detiene la corutina en el caso de que se active una nueva
            StopCoroutine(activeCoroutine);

        activeCoroutine = StartCoroutine(PowerUpTime()); //Guarda el valor de la corutina actual
    }
    private IEnumerator PowerUpTime()
    {
        yield return new WaitForSeconds(timeOfPowerUp); //Le da un tiempo de uso al PowerUp
        boxCollider.enabled = false; //Desactiva el box collider
    }
 
}
