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
        if (magnetTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, magnetTarget.position, _speed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.coinCount += 1;
            }
            Destroy(this.gameObject);
        }
    }

    public void SetMagnet (Transform magnet)
    {
        magnetTarget = magnet;
    }
}
