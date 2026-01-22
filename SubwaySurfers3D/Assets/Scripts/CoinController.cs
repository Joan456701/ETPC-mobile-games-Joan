using UnityEngine;

public class CoinController : MonoBehaviour
{
    private BoxCollider bCol;
    private Transform magnetTarget;

    [SerializeField] private float _speed;
    void Start()
    {
        bCol = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (magnetTarget != null) //Comprueba si magnetTarget tiene valor
        {
            transform.position = Vector3.Lerp(transform.position, magnetTarget.position, _speed);
            //Le pasa a la posicion de la moneda un vector para que la moneda vaya hacia el player
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            //Aumenta la cuenta del monedas en el player
            PlayerController pController = other.GetComponent<PlayerController>();
            if (pController != null)
            {
                if (pController.doubleCoins == false)
                {
                    pController.coinCount += 1;
                }
                else
                {
                    pController.coinCount += 2;
                }
            }
            Destroy(this.gameObject);
        }
    }

    public void SetMagnet (Transform magnet)
    {
        magnetTarget = magnet;
    }
}
