using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private BuildPoint selectedBuildPoint;

    public GameObject archerTowerPrefab;

    public void SelectBuildPoint(BuildPoint buildPoint)
    {
        selectedBuildPoint = buildPoint;
    }

    public void BuildTower(GameObject towerPrefab)
    {
        if (selectedBuildPoint == null) return;

        Instantiate(towerPrefab, selectedBuildPoint.transform.position, Quaternion.identity);
        selectedBuildPoint = null;
    }
}
