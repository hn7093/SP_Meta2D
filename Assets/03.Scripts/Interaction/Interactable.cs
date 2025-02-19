using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] KeyCode keyCode;
    [SerializeField] private SpriteRenderer portalImage;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private TextMeshProUGUI descript;
    private bool onPlayer;
    protected virtual void Interact(){}
    
    void FixedUpdate()
    {
        if(!onPlayer) return;
        if (onPlayer && Input.GetKeyDown(keyCode))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 이미지 표시
        portalImage.enabled = true;
        uiCanvas.enabled = true;
        onPlayer = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 이미지 숨김
        portalImage.enabled = false;
        uiCanvas.enabled = false;
        onPlayer = false;
    }
    protected void SetDescript(string comment)
    {
        descript.text = comment;
    }
}
