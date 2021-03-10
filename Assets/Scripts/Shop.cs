using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shop;
    public GameObject warning;
    public void OpenShop()
    {
        Time.timeScale = 0;
        shop.SetActive(true);
    }
    public void CloseShop()
    {
        Time.timeScale = 1;
        shop.SetActive(false);
        warning.SetActive(false);
    }
}
