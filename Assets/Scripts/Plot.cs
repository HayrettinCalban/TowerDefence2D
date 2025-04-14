using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject towerObj;
    public Turret turret;
    private Color startColor;
    void Start()
    {
        startColor = sr.color;
    }
    void OnMouseEnter()
    {
        sr.color = hoverColor;
    }
    void OnMouseExit()
    {
        sr.color = startColor;
    }
    void OnMouseDown()
    {
        if (towerObj != null)
        {
            return;
        }
        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("Not enough currency to build this tower!");
            return;
        }
        LevelManager.main.SpendCurrency(towerToBuild.cost);
        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();
    }
}
