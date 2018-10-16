using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanel: MonoBehaviour {
    public static InGamePanel Instance;

    [SerializeField] private InforPanel InforPanel;

    void Awake() {
        Instance = this;
        InforPanel.gameObject.SetActive(false);
    }

    public void OpenBuildTowerPanel(Transform trans , TowerIngame tower) {
        InforPanel.Open(false, trans, tower);
    }

    public void OpenUpgradeTowerPanel(Transform trans, TowerIngame tower) {
        InforPanel.Open(true, trans, tower);
    }
}
