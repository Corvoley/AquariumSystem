using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] shopScreens;
    private static GameObject currentScreen;


    private void Awake()
    {
        foreach (var screen in shopScreens)
        {
            screen.gameObject.SetActive(false);
        }
    }

    public void ShowCreatureShopMenu()
    {
        ShowScreen(FindScreenWithComponent(typeof(CreatureShop)));
    }
    public void ShowPlantShopMenu()
    {
        ShowScreen(FindScreenWithComponent(typeof(PlantShop)));
    }

    private void ShowScreen(GameObject screen)
    {
        if (!screen.activeInHierarchy)
        {
            CloseCurrent();
            screen.SetActive(true);
            currentScreen = screen;
        }
        else
        {
            CloseCurrent();

        }
    }

    public static void CloseCurrent()
    {
        if (currentScreen != null)
        {
            currentScreen.gameObject.SetActive(false);
        }
    }
    private GameObject FindScreenWithComponent(Type type)
    {
        foreach (GameObject screen in shopScreens)
        {
            if (screen.GetComponent(type) != null)
            {
                return screen;
            }
        }
        return null;
    }

}
