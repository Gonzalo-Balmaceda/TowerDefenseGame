using UnityEngine;

public class BuildUI : MonoBehaviour
{
    public static BuildUI Instance;

    public GameObject uiPanel;

    private BuildPoint targetPoint;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    public void SetTarget(BuildPoint buildPoint)
    {
        targetPoint = buildPoint;
        uiPanel.SetActive(true);
        uiPanel.transform.position = Camera.main.WorldToScreenPoint(buildPoint.transform.position);
    }

    public void Hide()
    {
        uiPanel.SetActive(false);
        targetPoint = null;
    }

    public void BuildArcher()
    {
        BuildManager.Instance.BuildTower(BuildManager.Instance.archerTowerPrefab);
        Hide();
    }

    public void BuildMage()
    {
        BuildManager.Instance.BuildTower(BuildManager.Instance.mageTowerPrefab);
        Hide();
    }
}
