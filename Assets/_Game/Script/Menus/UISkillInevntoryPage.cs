using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillInevntoryPage : MonoBehaviour
{

    [SerializeField]
    private UIInventorySkill skillPrefabs;

    [SerializeField]
    private UI_SkillDescription skillDescription;

    [SerializeField]
    private RectTransform contentPanel;

    List<UIInventorySkill> listofUISkills = new List<UIInventorySkill>();

    public Sprite sprite;
    public string quantity;
    public string title, description;
    private void Awake()
    {
        Hide();
        skillDescription.ResetDescription();
    }

    public void InitializeIventoryUI(int iventorySize)
    {
        for(int i = 0; i < iventorySize; i++)
        {
            UIInventorySkill uiItem =
                Instantiate(skillPrefabs, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listofUISkills.Add(uiItem);
            uiItem.OnItemClicker += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMauseBtnClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UIInventorySkill skill)
    {
        
    }

    private void HandleEndDrag(UIInventorySkill skill)
    {
        
    }

    private void HandleSwap(UIInventorySkill skill)
    {
        
    }

    private void HandleBeginDrag(UIInventorySkill skill)
    {
        
    }

    private void HandleItemSelection(UIInventorySkill skill)
    {
        skillDescription.SetDescription(title, description);
        listofUISkills[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        skillDescription.ResetDescription();

        listofUISkills[0].SetData(sprite, quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
