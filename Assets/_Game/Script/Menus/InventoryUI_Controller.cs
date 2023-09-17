using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_Controller : MonoBehaviour
{
    [SerializeField]
    private UISkillInevntoryPage inventorySkill;

    public int inventorySize = 4;

    private void Start()
    {
        inventorySkill.InitializeIventoryUI(inventorySize);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (inventorySkill.isActiveAndEnabled == false)
            {
                inventorySkill.Show();
            }
            else
            {
                inventorySkill.Hide();
            }
        }
    }
}
