using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Audio.Google;

[CreateAssetMenu(fileName = "TowerData", menuName = "Data/TowerList")]
public class TowerDataInGame: ScriptableObject {
    [SerializeField] List<TowerData> _allTower;

    public List<TowerData> GetTowerInfo() {
        return _allTower;
    }

    public void AddTower(TowerData newTower) {
        if(_allTower==null)
            _allTower = new List<TowerData>();

        _allTower.Add(newTower);
    }

    public void ReplayData(TowerData newTower) {
        if (_allTower == null)
            _allTower = new List<TowerData>();

        for (int i = 0; i < _allTower.Count; i++) {
            if(_allTower[i].Name != newTower.Name)
                continue;

            _allTower[i].Attitudes = newTower.Attitudes;
            _allTower[i].Cost = newTower.Cost;
            _allTower[i].Description = newTower.Description;
        }
    }
}

#region Data

[Serializable]
public class TowerBaseInfo {
    public string Name;
    public int Cost;
    public string Description;

    public List<AttitudeField> Attitudes;
}

[Serializable]
public class TowerData: TowerBaseInfo {
    public Sprite Icon;
    public GameObject Prefab;
}

[Serializable]
public class AttitudeField {
    public TypeAttitude Type;
    public string Description;

    public List<LevelAttitude> Levels;

    public bool CanUpgrade() {
        return Levels.Count > 1;
    }

    public float GetPriceAttitude(int level) {
        if (level < Levels.Count) {
            return Levels[level].Cost;
        }

        return -1;
    }

    public float GetValueAttitude(int level) {
        if (level < Levels.Count) {
            return Levels[level].Value;
        }

        return -1;
    }
}

[Serializable]
public class LevelAttitude {
    public float Cost;
    public float Value;
}


public enum TypeAttitude {
    RadiusRange,
    Damage,
    Firerate,
    RotateSpeed,
    AimTime,
    FrozenPercent,
    FrozenTime,
}
#endregion

#region Tower data ingame

[Serializable]
public class TowerIngame: TowerBaseInfo {
    public List<CoupleValue> LevelIndexAttitudes;

    public TowerIngame() {
        LevelIndexAttitudes = new List<CoupleValue>();
    }

    public TowerIngame(TowerBaseInfo data) {
        Name = data.Name;
        Cost = data.Cost;
        Description = data.Description;

        Attitudes = data.Attitudes;
        LevelIndexAttitudes = new List<CoupleValue>();

        FindAttitude();
    }

    public void FindAttitude() {
        for (int i = 0; i < Attitudes.Count; i++) {
            if (!Attitudes[i].CanUpgrade())
                continue;

            LevelIndexAttitudes.Add(new CoupleValue { Index = i, Level = 0 });

            if (LevelIndexAttitudes.Count >= 4)
                break;
        }

        Debug.Log("Found " + LevelIndexAttitudes.Count + " Attitude !!");
    }

    public void UpgradeAttitude(int index) {
        var attitude = Attitudes[LevelIndexAttitudes[index].Index];
        var level = LevelIndexAttitudes[index].Level;

        if (attitude.GetPriceAttitude(level) == -1)
            return;

        LevelIndexAttitudes[index].Level++;
    }
}

[Serializable]
public class CoupleValue {
    public int Index;
    public int Level;
}


#endregion