using UnityEngine;

public class TileController : MonoBehaviour
{
    public Transform pivotBack;

    [Header("Power-Up Prefabs")]
    public GameObject magnetPrefab;
    public GameObject doubleCoinsPrefix;

    [Header("Coin Prefabs")]
    public GameObject coinPrefab;

    [Header("Power-Up Spawn Settings")]
    [Range(0f, 1f)]
    public float powerUpSpawnChance = 0.3f; // Probabilidad por defecto
    public float powerUpYPosition = 1f; // Altura a la que aparecen los power-ups
    public float powerUpZOffset = 10f; // Distancia desde el inicio del tile

    [Header("Coin Spawn Settings")]
    [Range(0f, 1f)]
    public float coinLineSpawnChance = 0.7f; // Probabilidad de spawn de monedas
    public int minCoinsInLine = 5; // Mínimo de monedas
    public int maxCoinsInLine = 10; // Máximo de monedas
    public float coinYPosition = 1f; // Altura de las monedas
    public float coinZStartOffset = 5f; // Donde empieza la línea de monedas
    public float coinSpacing = 2f; // Separación entre monedas en la línea

    private int[] lanePositions = { -4, 0, 4 }; // Posicion de los carriles

    void Start()
    {
        SpawnPowerUp();
        SpawnCoinLine();
    }

    private void SpawnPowerUp()
    {
        // Determinar si debe aparecer un power-up
        float randomChance = Random.Range(0f, 1f);

        if (randomChance > powerUpSpawnChance)
            return;

        // Elegir qué tipo de power-up spawner (50% imán, 50% monedas dobles)
        GameObject powerUpToSpawn = Random.Range(0, 2) == 0 ? magnetPrefab : doubleCoinsPrefix;

        if (powerUpToSpawn == null)
            return; // No hay prefab asignado

        // Elegir un carril aleatorio
        int randomLane = Random.Range(0, lanePositions.Length);
        float xPosition = lanePositions[randomLane];

        // Calcular posición de spawn
        Vector3 spawnPosition = new Vector3(
            xPosition,
            powerUpYPosition,
            transform.position.z + powerUpZOffset
        );

        // Determinar rotación: -90º en X solo para doubleCoins
        Quaternion rotation = Quaternion.identity;
        if (powerUpToSpawn == doubleCoinsPrefix)
        {
            rotation = Quaternion.Euler(-90f, 0f, 0f);
        }

        // Instanciar el power-up con la rotación apropiada
        Instantiate(powerUpToSpawn, spawnPosition, rotation, transform);
    }

    private void SpawnCoinLine()
    {
        if (coinPrefab == null)
            return;

        // Determinar si debe aparecer una línea de monedas
        float randomChance = Random.Range(0f, 1f);

        if (randomChance > coinLineSpawnChance)
            return; // No spawneamos monedas en este tile

        // Elegir un carril aleatorio para la línea de monedas
        int randomLane = Random.Range(0, lanePositions.Length);
        float xPosition = lanePositions[randomLane];

        // Determinar cuántas monedas spawner en la línea
        int coinCount = Random.Range(minCoinsInLine, maxCoinsInLine + 1);

        // Spawner la línea de monedas
        for (int i = 0; i < coinCount; i++)
        {
            float zPosition = transform.position.z + coinZStartOffset + (i * coinSpacing);

            Vector3 spawnPosition = new Vector3(
                xPosition,
                coinYPosition,
                zPosition
            );

            Instantiate(coinPrefab, spawnPosition, Quaternion.identity, transform);
        }
    }
}

