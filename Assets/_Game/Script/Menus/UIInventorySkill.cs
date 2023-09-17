using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class UIInventorySkill : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TMP_Text headerText;

    [SerializeField]
    private Image borderImage;

    public event Action<UIInventorySkill> OnItemClicker, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMauseBtnClick;

    private bool empty = true;//some of this events shouldnt be called when empty

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }
    private void Deselect()
    {
        borderImage.enabled = false;
    }

    public void SetData(Sprite sprite, string quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.headerText.text = quantity + "";
        empty = false;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    public void OnBeginDrag()
    {
        if (empty)
        {
            return;
        }
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrop()
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnPointerClicker(BaseEventData data)
    {
        if (empty)
        {
            return;
        }

        PointerEventData pointerData = (PointerEventData)data;
        if(pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMauseBtnClick?.Invoke(this);
        }
        else 
        { 
            OnItemClicker?.Invoke(this);
        }
    }
}

