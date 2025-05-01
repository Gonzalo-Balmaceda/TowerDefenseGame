using UnityEngine;

[System.Serializable]
public class Wave
{
    // Prefab del enemigo que se va a instanciar.
    public GameObject enemyPrefab;

    // Cantidad de enemigos en esta oleada.
    public int enemyCount;

    // Velocidad de aparición de enemigos (enemigos por segundo).
    public float spawnRate;
}
