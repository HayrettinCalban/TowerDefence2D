using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;
    private bool isMenuOpen = true;
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }
    void OnGUI()
    {
        currencyUI.text = "Currency: " + LevelManager.main.currency.ToString();
    }
    void SetSelected(int _selectedTower)
    {
        BuildManager.main.SetSelectTower(_selectedTower);
    }

}
