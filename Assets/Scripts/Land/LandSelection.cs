using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LandSelection: MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler {

    public void OnPointerClick(PointerEventData eventData) {
        if (transform.childCount == 0)
            InGamePanel.Instance.OpenBuildTowerPanel(transform, null);
        else {
            InGamePanel.Instance.OpenUpgradeTowerPanel(transform, transform.GetChild(0).GetComponent<BaseTower>().Data);
        }
    }


    public void OnPointerDown(PointerEventData eventData) {

    }

    public void OnPointerUp(PointerEventData eventData) {

    }
}
