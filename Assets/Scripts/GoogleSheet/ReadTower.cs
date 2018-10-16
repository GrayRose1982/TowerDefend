using System;
using System.Collections;
using System.Collections.Generic;
using AnhVuiVe;
using UnityEngine;

public class ReadTower: MonoBehaviour {
    public TowerData dataLoaded;


    [ContextMenu("LoadData")]
    public void LoadData() {
        dataLoaded = ReadGoogleSheet.FillDataHasList<TowerData, AttitudeField, LevelAttitude>("1fm4Mitj8s55MOAykzEjo6c6IHV3X9FX_T7HzBV1z22E", "0");
        GameData g = GameObject.FindObjectOfType<GameData>();
        g.Towers.ReplayData(dataLoaded);
    }
}


[Serializable]
public class TData {
    public string value;
    public List<Att> Att;
}

[Serializable]
public class Att {
    public string AttType;
    public int AttValue;

    public List<Att2> Att2;
}

[Serializable]
public class Att2 {
    public string Type;
    public int Value;
}