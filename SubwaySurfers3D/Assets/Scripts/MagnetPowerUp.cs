using System.Collections;
using UnityEngine;

public class MagnetPowerUp : MonoBehaviour
{
    public BoxCollider boxCollider;
    private Coroutine activeCoroutine;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            CoinController coin = other.GetComponent<CoinController>();
            coin.SetMagnet(this.transform);
        }
    }
    public void ActiveMagnet()
    {
        boxCollider.enabled = true;

        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        activeCoroutine = StartCoroutine(PowerUpTime());
    }
    public IEnumerator PowerUpTime()
    {
        yield return new WaitForSeconds(10f);
        boxCollider.enabled = false;
    }
 
}
