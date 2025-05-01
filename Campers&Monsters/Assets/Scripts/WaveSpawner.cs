using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    // Configuración de oleadas
    // Lista de oleadas que se van a lanzar."
    public Wave[] waves;

    // Punto de aparición de los enemigos.
    public Transform spawnPoint;

    // Tiempo de espera entre oleadas (en segundos).
    public float timeBetweenWaves = 10f;

    // UI
    // Texto que muestra el tiempo restante para la próxima oleada.
    public Text waveCountdownText;

    // Índice actual de la oleada que se está lanzando.
    private int currentWaveIndex = 0;

    // Contador de tiempo para la próxima oleada.
    private float countdown;

    // Indica si actualmente se están generando enemigos.
    private bool isSpawning = false;

    // Inicializa el contador al iniciar la escena.
    private void Start()
    {
        countdown = timeBetweenWaves;
    }

    // Se llama una vez por frame. Maneja el conteo y el inicio de nuevas oleadas.
    private void Update()
    {
        // Si ya se está generando una oleada, no hacer nada.
        if (isSpawning)
            return;

        // Si el contador llegó a cero, lanzar una nueva oleada.
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        // Reducir el contador con el tiempo real.
        countdown -= Time.deltaTime;

        // Mostrar el contador en la UI (con 1 decimal).
        if (waveCountdownText != null)
            waveCountdownText.text = Mathf.Clamp(countdown, 0, 999).ToString("F1");
    }

    // Lanza una oleada de enemigos, uno por uno con tiempo entre ellos.
    IEnumerator SpawnWave()
    {
        isSpawning = true;

        // Obtener la oleada actual.
        Wave wave = waves[currentWaveIndex];

        // Instanciar enemigos con tiempo entre cada uno.
        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        // Pasar a la siguiente oleada.
        currentWaveIndex++;

        // Si se completaron todas las oleadas, mostrar mensaje (o finalizar juego).
        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("¡Todas las oleadas han sido completadas!");
            // Podés llamar aquí una función de victoria.
        }

        isSpawning = false;
    }


    // Instancia un enemigo en el punto de aparición.
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, Quaternion.identity);
    }

    // Permite iniciar una oleada manualmente desde UI (botón).
    public void StartNextWaveManually()
    {
        if (!isSpawning && currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
    }
}
