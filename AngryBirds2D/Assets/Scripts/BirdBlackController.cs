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
    private bool _hasExploded; // Indica si ya explotó realmente

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
        _used = false;
        _hasExploded = false; // Inicialmente no ha explotado
    }

    private void Update()
    {
        Debug.Log(_hasExploded);
        if (isActive)
        {
            // Solo detectar si está vivo después de haber explotado
            if (_hasExploded == true)
            {
                DetectAlive();
            }

            DrawTrace();

            // Activar explosión con la tecla Espacio
            if (!_used && Input.GetKeyDown(KeyCode.Space))
            {
                _used = true;
                Explode();
            }
        }
    }

    // También explotar al colisionar con algo (con delay)
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

        // Detectar todos los objetos en el radio de explosión
        int layerMask = LayerMask.GetMask("Box", "Enemy");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, layerMask);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // Aplicar daño si tiene HealthController
            HealthController healthCtr = hitCollider.GetComponent<HealthController>();
            if (healthCtr != null)
            {
                healthCtr.UpdateHealth(explosionDamage);
            }

            // Aplicar fuerza de explosión si tiene Rigidbody2D
            Rigidbody2D rb = hitCollider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (hitCollider.transform.position - transform.position).normalized;
                float distance = Vector2.Distance(transform.position, hitCollider.transform.position);

                // La fuerza disminuye con la distancia
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

        // Recargar el siguiente pájaro
        ReloadNext();

        // Destruir este objeto
        Destroy(gameObject);
    }
}