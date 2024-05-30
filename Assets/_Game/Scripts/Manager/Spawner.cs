using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : Singleton<Spawner>
{

    [SerializeField] 
    List<PoolType> Enemiestype = new List<PoolType>();
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 5f;

    List<Enemy> ListEnemy = new List<Enemy>();
    private int waveIndex = 0;
    private int MaxWave = 5;
    

    public void OnInit()
    {
        countdown = 5f;
        waveIndex = 0;
        UIManager.Instance.GetUI<GamePlay>().SetWave(waveIndex, MaxWave);
        ListEnemy.Clear();
    }
    void Update()
    {
        if (!GameManager.IsState(GameState.GamePlay))
        {
            return;
        }
        if(waveIndex >= MaxWave)
        {
            return;
        }
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            UIManager.Instance.GetUI<GamePlay>().SetCountDown(countdown);

        }

        countdown -= Time.deltaTime;
        UIManager.Instance.GetUI<GamePlay>().SetCountDown(countdown);
        
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        UIManager.Instance.GetUI<GamePlay>().SetWave(waveIndex, MaxWave);
        int randCount = Random.Range(2, 5);
        for (int i = 0; i < randCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Enemy enemy = SimplePool.Spawn<Enemy>(Enemiestype[Random.Range(0, Enemiestype.Count)], spawnPoint.position, Quaternion.identity);
        if(enemy != null)
        {
            ListEnemy.Add(enemy);
            enemy.OnInit();
        }
    }

    public void DeleteEnemy(Enemy enemy)
    {
        ListEnemy.Remove(enemy);
    }

    public bool IsWin()
    {
        return waveIndex >= MaxWave && ListEnemy.Count <= 0;
    }
}
