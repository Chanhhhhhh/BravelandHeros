using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{

    //UnityAction SpawnHero;
    [SerializeField] private GameObject Map;
    public PoolType typeSelectHero;
    public int currentCoin;
    public int currentCoinBuy;
    public List<HerosData> Heros;

    public Node NodeSelect;
    public bool IsSelectedNode;
    public Vector3 positionOffset;
    public int currentHealth;

    List<Node> NodeHaveHero = new List<Node>();

    public void OnInit()
    {
        if (!Map.gameObject.activeSelf)
        {
            Map.gameObject.SetActive(true);
        }
        NodeHaveHero.Clear();
        currentHealth = 5;
        currentCoin = 500;
        UpdateCoin();
        UIManager.Instance.GetUI<GamePlay>().SetHealth(currentHealth);
        Spawner.Instance.OnInit();
    }
    public void SpawnHero()
    {
        Hero hero = SimplePool.Spawn<Hero>(typeSelectHero);
        hero.TF.position = NodeSelect.transform.position + positionOffset;
        hero.TF.rotation = NodeSelect.transform.rotation;
        hero.OnInit();
        typeSelectHero = PoolType.none;
        NodeHaveHero.Add(NodeSelect);
        currentCoin -= currentCoinBuy;
        UpdateCoin();
        currentCoinBuy = 0;
        ClearNode();
    }


    public bool CheckNodeFull(Node node)
    {
        return NodeHaveHero.Contains(node);
    }
    public void ClearNode()
    {
        NodeSelect.ExitSelect();
        NodeSelect = null;
        IsSelectedNode = false;
    }


    public void UpdateCoin()
    {
        UIManager.Instance.GetUI<GamePlay>().SetCoin(currentCoin);
    }

    internal void ReceiveCoin(int value)
    {
        currentCoin+= value;
        UpdateCoin();
    }

    public void ReduceHealth()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            Lose();
            SimplePool.CollectAll();
            return;
        }
        UIManager.Instance.GetUI<GamePlay>().SetHealth(currentHealth);
    }

    public void Lose()
    {
        GameManager.ChangeState(GameState.Lose);       
    }

    public void Win()
    {
        SimplePool.CollectAll();
        GameManager.ChangeState(GameState.Win);
    }

    public void CloseMap()
    {
        Map.gameObject.SetActive(false);
    }

}
