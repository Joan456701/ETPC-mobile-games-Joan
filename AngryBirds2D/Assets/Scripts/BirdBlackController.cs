using UnityEngine;
using System.Collections;
public class BirdBlackController : BirdController
{
    [Header("Explosion Settings")]
    public float explosionRadius = 3f;
    public float explosionForce = 500f;
    public float explosionDamage = 50f;
    public GameObject explosionEfect;

    private bool _used;
    private bool _hasExploded;

    
    void Start()
    {
        Initialize();
        _used = false;
        _hasExploded = false;
    }

    private void Update()
    {
        Debug.Log(_hasExploded);
        if (isActive)
        {
            if (_hasExploded == true)
            {
                DetectAlive();
            }

            DrawTrace();

            if (!_used && Input.GetKeyDown(KeyCode.Space))
            {
                _used = true;
                Explode();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive && !_used)
        {
            StartCoroutine(ExplodeWithDelay(1f));
            _used = true;
        }

    }

    private IEnumerator ExplodeWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    private void Explode()
    {
        _hasExploded = true;

        Instantiate(explosionEfect, transform.position, Quaternion.identity);

        int layerMask = LayerMask.GetMask("Box", "Enemy");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerMask);

        foreach (Collider2D hitCollider in hitColliders)
        {
            HealthController healthCtr = hitCollider.GetComponent<HealthController>();
            if (healthCtr != null)
            {
                healthCtr.UpdateHealth(explosionDamage);
            }

            Rigidbody2D rb = hitCollider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (hitCollider.transform.position - transform.position).normalized;
                float distance = Vector2.Distance(transform.position, hitCollider.transform.position);

                float forceMagnitude = explosionForce * (1 - (distance / explosionRadius));
                rb.AddForce(direction * forceMagnitude);
            }
        }

        StartCoroutine(DestroyAfterExplosion());
    }

    private IEnumerator DestroyAfterExplosion()
    {
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(0.5f);

        ReloadNext();

        Destroy(gameObject);
    }
}