using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepController : MonoBehaviour
{
    public EnemyBase[] Enemies;

    public int[] WaveEnemy;

    void Start()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartSpawn());
        }
    }

    private IEnumerator StartSpawn()
    {
        var path = LandCreation.Instance.PathMove.ToArray();

        for (int i = 0; i < WaveEnemy.Length; i++)
        {
            var e = Instantiate(Enemies[WaveEnemy[i]]);
            e.SetPath(path);
            yield return new WaitForSeconds(.1f);
        }
    }
}
