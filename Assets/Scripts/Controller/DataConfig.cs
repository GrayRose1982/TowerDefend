using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataConfig : MonoBehaviour
{
    public static DataConfig Instance;

    public Color CanUpgrade = Color.white;
    public Color CantUpgrade = Color.white;
    public Color Gem = Color.white;
    public Color Coin = Color.white;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
}
