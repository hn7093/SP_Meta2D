using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : Interactable
{
    public string scneName;
    private void Start()
    {
        
    }

    protected override void Interact()
    {
        SceneManager.LoadScene(scneName);
    }
}
