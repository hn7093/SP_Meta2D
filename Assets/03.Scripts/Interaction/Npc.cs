using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Npc : Interactable
{
    [SerializeField] private GameObject uiPrefab;
    private void Start()
    {

    }

    protected override void Interact()
    {
        uiPrefab.SetActive(true);
    }
    public void CloseUI()
    {
        uiPrefab.SetActive(false);
    }
}
