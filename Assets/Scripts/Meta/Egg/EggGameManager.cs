using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EggGameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _dialogText;

    public void SetDialogText(TMP_InputField inputField) 
    {
        _dialogText.text =  $"Привет, {inputField.text}";
    }
}
