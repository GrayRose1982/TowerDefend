using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.UI;

public class BuyTowerPanel: MonoBehaviour {
    [SerializeField] private Image[] _items;
    [SerializeField] private GameObject _itemSelect;

    private int _currentIndex = -1;

    private Transform _lastClick;

    public void Open(Transform landClick, TowerIngame towerData) {
        _lastClick = landClick;
        _itemSelect.SetActive(_currentIndex != -1);
        gameObject.SetActive(true);
    }

    public void InitButton(List<TowerData> itemsShows, Action<TowerData> updateinfo, Action<Transform, TowerIngame> updateInfo) {
        for (int i = 0; i < _items.Length; i++) {
            if (i >= itemsShows.Count) {
                continue;
            }

            _items[i].sprite = itemsShows[i].Icon;
            var btn = _items[i].GetComponent<Button>();
            if (btn) {
                var index = i;
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => {
                    if (_currentIndex == index) {
                        var newTower = Instantiate(itemsShows[index].Prefab, _lastClick).transform;
                        newTower.localPosition = Vector3.zero;

                        var tower = newTower.GetComponent<BaseTower>();
                        tower.SetupDataTower(itemsShows[index]);
                        InGamePanel.Instance.OpenUpgradeTowerPanel(_lastClick, tower.Data);
                        //updateInfo(_lastClick, itemsShows[index]);
                    } else {
                        updateinfo(itemsShows[index]);
                        SelectItem(_items[index].transform);
                        _currentIndex = index;
                    }
                });
            }
        }
    }

    private void SelectItem(Transform posTo) {
        _itemSelect.gameObject.SetActive(true);
        _itemSelect.transform.SetParent(posTo);
        _itemSelect.transform.localPosition = Vector3.zero;
    }

    public void Close() {
        _itemSelect.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}