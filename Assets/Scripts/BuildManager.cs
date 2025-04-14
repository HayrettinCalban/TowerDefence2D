using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;
    [SerializeField] private Tower[] towers;
    private int selectedTower = 0;

    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }
    void Awake()
    {
        main = this;
    }
    public void SetSelectTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }
}
