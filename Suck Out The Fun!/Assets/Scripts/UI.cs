using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Health playerHealthComponent;

    public void UpdateHealthUI (float newhealthvalue)
    {
        playerHealthComponent.OnHealthChange(newhealthvalue);
    }
}