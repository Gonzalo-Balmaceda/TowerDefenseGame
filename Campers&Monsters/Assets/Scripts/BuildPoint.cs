using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("Punto de construcción seleccionado: " + gameObject.name);
        // Mostrar el menú de construcción
        BuildManager.Instance.SelectBuildPoint(this);
    }
}
