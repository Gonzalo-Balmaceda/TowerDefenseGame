using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public List<Transform> wayPoints = new List<Transform>(); // Creamos una lista para los way points.
    private int targetIndex = 1;
    public float speed = 4F;
    public float rotationSpeed = 6F;
    private Animator anim;

    [Header("Life")]
    private bool isDead;
    private bool isTakingDamage;
    public float maxLife = 100F;
    public float currentLife = 0;
    public Image fillLifeImage;
    private Transform canvasRoot;
    private Quaternion initLifeRotation;

    private void Awake()
    {
        GetWayPoints();
        /*canvasRoot = fillLifeImage.transform.parent.parent; // Otenemos el canvas.
        initLifeRotation = canvasRoot.rotation; // Obtenemos la posición inicial del canvas.
        anim = GetComponent<Animator>();
        anim.SetBool("Movement", true);*/
        
    }

    void Start()
    {
       currentLife = maxLife; // Establecemos la cantidad de vida inicial.
       Debug.Log("Cantidad de waypoints: " + wayPoints.Count);
    }

    // Método para obtener los way points.
    private void GetWayPoints()
    {
        Debug.Log("WayPoints Obtenidos");
        wayPoints.Clear(); // Limpiamos la lista de way points.
        var rootWaypoint = GameObject.Find("WayPointsContainer"); // Buscamos el objeto que contiene los way points.

        if (rootWaypoint == null)
    {
        Debug.LogError("No se encontró el objeto 'WayPointsContainer' en la escena.");
        return;
    }
        for (int i = 0; i < rootWaypoint.transform.childCount; i++) // Iteramos cada way point.
        {
            wayPoints.Add(rootWaypoint.transform.GetChild(i)); // Añadimos los way points a la lista.
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //canvasRoot.transform.rotation = initLifeRotation; // Hacemos que se mantenga el canvas de la vida en la misma posición que la del inicio.

        //LookAt();

        /*if (Input.GetKeyDown(KeyCode.Space)) // Codigo temporal para testear con el espacio la animación de daño.
        {
            TakeDamage(10);
        }*/
    }

    #region Movement & Rotations
    public void Movement()
    {
        /*if (isDead)
        {
            return; // Si muere cortamos la ejecución del código.
        }*/
        // Movemos el objeto en la posición actual hacia los way points establecidos.
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[targetIndex].position, speed * Time.deltaTime);

        // Calculamos la distancia entre dos objetos.
        var distance = Vector3.Distance(transform.position, wayPoints[targetIndex].position); 

        if (distance <= 0.1F)
        {
            if (targetIndex >= wayPoints.Count-1) // wayPoint.Count-1 = 11
            {
                return; // Cortamos la ejecución del código para que no se lance un error.
            }
            targetIndex++; // Aumentamos le targetIndex para que se vaya moviendo el enemigo al siguiente way point.
        }
    }

    public void LookAt()
    {
        // Rotamos al enemigo para que mire hacia la dirección del proximo way point. (Forma 1)
        //transform.LookAt(wayPoints[targetIndex]);

        // Rotamos hacia la dirección del próximo way point pero de una manera mas suavizada. (Forma 2)
        // 1- Obtener dirección.
        var direction = wayPoints[targetIndex].position - transform.position;
        // 2- Obtener rotación según la dirección.
        var rootTarget = Quaternion.LookRotation(direction);
        // 3- Hacemos la rotación suavizada.
        transform.rotation = Quaternion.Slerp(transform.rotation, rootTarget, rotationSpeed * Time.deltaTime);
    }
    #endregion

    /*public void TakeDamage(float dmg)
    {
        var newLife = currentLife - dmg;

        if(isDead)
        {
            return;
        }

        if (newLife <= 0)
        {
            OnDead();
        }

        currentLife = newLife;
        var fillValue = currentLife * 1 / 100;
        fillLifeImage.fillAmount = fillValue;
        currentLife = newLife;
        StartCoroutine(AnimationDamage());
    }*/

    /*private IEnumerator AnimationDamage()
    {
        anim.SetBool("Damage", true);
        yield return new WaitForSeconds(0.1F);
        anim.SetBool("Damage", false);
    }*/

    /*public void OnDead()
    {
        isDead = true;
        anim.SetBool("Die", true);
        anim.SetBool("Damage", false);
        currentLife = 0;
        fillLifeImage.fillAmount = 0; // Dejamos la barra de vida vacia.
        StartCoroutine(OnDeadEffect());
    }*/

    /*private IEnumerator OnDeadEffect()
    {
        yield return new WaitForSeconds(3f);
        var finalPositionY = transform.position.y -5;
        Vector3 target = new Vector3(transform.position.x, finalPositionY, transform.position.z);
        while (transform.position.y != finalPositionY)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 1.5f * Time.deltaTime); // Hacemos que el enemigo se hunda en el mapa.
            yield return null;
        }
    }*/
}
