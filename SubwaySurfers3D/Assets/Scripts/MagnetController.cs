using System.Threading;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    private BoxCollider boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MagnetPowerUp magnetPower = other.GetComponentInChildren<MagnetPowerUp>();
            Destroy(this.gameObject);
            magnetPower.ActiveMagnet();
        }
    }
}
