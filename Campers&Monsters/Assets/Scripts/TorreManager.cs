using UnityEngine;
using System.Collections.Generic;

public class Torre : MonoBehaviour
{
    public float rango = 5f;
    public float tiempoEntreDisparos = 1f;
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;

    private float tiempoDisparoActual = 0f;

    void Update()
    {
        tiempoDisparoActual -= Time.deltaTime;

        GameObject enemigoObjetivo = BuscarEnemigoEnRango();
        if (enemigoObjetivo != null && tiempoDisparoActual <= 0f)
        {
            Atacar(enemigoObjetivo);
            tiempoDisparoActual = tiempoEntreDisparos;
        }
    }

    GameObject BuscarEnemigoEnRango()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");

        GameObject objetivoMasCercano = null;
        float distanciaMasCercana = Mathf.Infinity;

        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector3.Distance(transform.position, enemigo.transform.position);
            if (distancia < rango && distancia < distanciaMasCercana)
            {
                distanciaMasCercana = distancia;
                objetivoMasCercano = enemigo;
            }
        }

        return objetivoMasCercano;
    }

    void Atacar(GameObject enemigo)
    {
        // Disparar proyectil
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);
        proyectil.GetComponent<Proyectil>().Inicializar(enemigo.transform);
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja el rango de ataque en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}
