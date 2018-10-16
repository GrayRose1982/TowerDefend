using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowerPanel: MonoBehaviour {

    public void Close()
    {
        gameObject.SetActive(false);
    }

    internal void Open(Transform landClick, TowerIngame towerData) {
        gameObject.SetActive(true);
    }
}
