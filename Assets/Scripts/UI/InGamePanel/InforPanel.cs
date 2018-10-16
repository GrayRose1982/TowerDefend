using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InforPanel: MonoBehaviour {
    [SerializeField] private Text _txtName;
    [SerializeField] private Text _txtInformation;

    [SerializeField] private BuyTowerPanel _buyTowerPanel;
    [SerializeField] private UpgradeTowerPanel _upgradeTowerPanel;

    void Start() {
        InitData();
    }

    void InitData() {
        LoadDataBuy();
    }

    private void LoadDataUpgrade() {

    }

    private void LoadDataBuy() {
        var dataShow = GameData.Instance.Towers.GetTowerInfo();
        _buyTowerPanel.InitButton(dataShow, ShowInformation, ChangeToUpgradePanel);
    }

    private void ShowInformation(TowerData tower) {
        _txtName.text = tower.Name;
        _txtInformation.text = tower.Description;
    }

    private void ChangeToUpgradePanel(Transform positionClick, TowerIngame towerData) {
        Open(true, positionClick, towerData);
    }

    public void Open(bool isClickOnTower, Transform positionClick, TowerIngame towerData) {
        if (isClickOnTower) {
            _upgradeTowerPanel.Open(positionClick, towerData);
            _buyTowerPanel.Close();
        } else {
            _buyTowerPanel.Open(positionClick, towerData);
            _upgradeTowerPanel.Close();
        }

        gameObject.SetActive(true);
    }
}
