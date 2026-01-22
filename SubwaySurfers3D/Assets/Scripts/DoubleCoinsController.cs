using System.Collections;
using UnityEngine;

public class DoubleCoinsController : MonoBehaviour
{
    PlayerController pController;
    

    private void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        pController = obj.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            pController.doubleCoins = true;
            pController.TimeDoubleCoin();
            Destroy(this.gameObject);
        }
    }

    
}
