using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNpc : Interactable
{
    [SerializeField] DialogSystem dialogSystem;
    protected override void Interact()
    {
        dialogSystem.StartDialog();
    }
}
