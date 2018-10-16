using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    public static PlayerData Instance;

    public PlayerGameData _playerData;
    private bool _wasLoad;

    public int Coin {
        get { return _playerData.Coin; }
        private set { _playerData.Coin = value; }
    }

    public int Gem {
        get { return _playerData.Coin; }
        private set { _playerData.Gem = value; }
    }


    void Awake() {
        if (!Instance) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        } else
            DestroyImmediate(gameObject);
    }

    void OnApplicationPause(bool isPause) {
        if (!_wasLoad) {
            _wasLoad = true;
            return;
        }


        if (isPause)
            SaveData();
        else
            LoadData();
    }

    void OnDestroy() {
        if (Instance == this)
            SaveData();
    }

    public void SaveData() {
        PlayerPrefs.SetString("Data", JsonUtility.ToJson(this));
    }

    public void LoadData() {
        var dataString = PlayerPrefs.GetString("Data");
        PlayerGameData dataSaved = dataString == null ? new PlayerGameData() : JsonUtility.FromJson<PlayerGameData>(dataString);

        _playerData = dataSaved;
    }

    public void AddCoin(int number) {
        Coin += number;
    }

    public void AddGem(int number) {
        Gem += number;
    }

    public bool UseCoin(int number) {
        if (Coin < number)
            return false;

        Coin -= number;
        return true;
    }

    public bool UseGem(int number) {
        if (Gem < number)
            return false;

        Gem -= number;
        return true;
    }
}


