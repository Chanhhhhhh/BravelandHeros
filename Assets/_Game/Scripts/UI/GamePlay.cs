using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    [SerializeField] HerosBtn HerosBtnPrefabs;
    [SerializeField] Transform ShopContent;
    [SerializeField] private Slider SlideCountDown;
    [SerializeField] TextMeshProUGUI TextCoin;
    [SerializeField] TextMeshProUGUI TextHealth;
    [SerializeField] TextMeshProUGUI TextWave;
    [SerializeField] private Button PauseBtn;



    public override void Setup()
    {
        base.Setup();
        foreach(Transform child in ShopContent)
        {
            Destroy(child.gameObject);
        }
        int countHeros = LevelManager.Instance.Heros.Count;
        for (int i = 0; i < countHeros; i++)
        {
            HerosBtn newButton = Instantiate(HerosBtnPrefabs, ShopContent);
            newButton.GetDataToBtn(LevelManager.Instance.Heros[i]);

        }
        SlideCountDown.maxValue = Spawner.Instance.timeBetweenWaves;
    }

    public void SpawnHero()
    {
        if (!LevelManager.Instance.IsSelectedNode)
        {
            return;
        }

    }

    public void OnPause()
    {
        GameManager.ChangeState(GameState.Pause);
    }

    public void SetCountDown(float value)
    {
        SlideCountDown.value = value;
    }

    public void SetCoin(int coin)
    {
        TextCoin.text = coin.ToString();
    }

    public void SetHealth(int health)
    {
        TextHealth.text = health.ToString();

    }

    public void SetWave(int wave, int maxWave)
    {
        TextWave.text = wave.ToString() + "/" + maxWave.ToString();
    }

}
