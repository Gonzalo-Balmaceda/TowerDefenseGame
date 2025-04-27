using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    private BuildPoint selectedBuildPoint; // Guardamos el punto de construcción seleccionado.

    public GameObject archerTowerPrefab;
    public GameObject mageTowerPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectBuildPoint(BuildPoint buildPoint)
    {
        selectedBuildPoint = buildPoint;
        // Mostrar menú para elegir torre
    }

    public void BuildTower(GameObject towerPrefab)
    {
        if (selectedBuildPoint != null)
        {
            Instantiate(towerPrefab, selectedBuildPoint.transform.position, Quaternion.identity);
            Destroy(selectedBuildPoint.gameObject); // Opcional: eliminar el punto una vez usado
            selectedBuildPoint = null;
        }
    }
}

