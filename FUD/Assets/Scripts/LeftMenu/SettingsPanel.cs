﻿using UnityEngine;


public class SettingsPanel : MonoBehaviour
{
    public void SetView()
    {
        gameObject.SetActive(true);
    }
    public void OnBackButtonAction()
    {
        gameObject.SetActive(false);
    }
}
