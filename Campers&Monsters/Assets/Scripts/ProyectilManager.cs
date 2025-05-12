using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidad = 10f;
    public int daño = 1;
    private Transform objetivo;

    public void Inicializar(Transform enemigo)
    {
        objetivo = enemigo;
    }

    void Update()
    {
        if (objetivo == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direccion = objetivo.position - transform.position;
        transform.position += direccion.normalized * velocidad * Time.deltaTime;

        if (direccion.magnitude < 0.1f)
        {
            // Aquí podrías aplicar daño al enemigo si tiene un script de vida
            Destroy(gameObject);
            // ejemplo: objetivo.GetComponent<Enemigo>().RecibirDaño(daño);
        }
    }
}

