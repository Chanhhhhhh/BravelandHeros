using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HerosBtn : MonoBehaviour
{
    [SerializeField] ButtonAction btn;
    [SerializeField] Image ImageHero;
    [SerializeField] TextMeshProUGUI TextPrice;

    private void Start()
    {
        btn.GetComponent<ButtonAction>();
    }

    public void GetDataToBtn(HerosData herosData)
    {
        ImageHero.sprite = herosData.imageHero;
        TextPrice.text = herosData.priceHero.ToString();
        btn.action += () =>
        {
            if (!GameManager.IsState(GameState.GamePlay))
            {
                return;
            }
            if (!LevelManager.Instance.IsSelectedNode)
            {
                return;
            }
            if (LevelManager.Instance.currentCoin < herosData.priceHero)
            {
                return;
            }
            LevelManager.Instance.currentCoinBuy = herosData.priceHero;
            LevelManager.Instance.typeSelectHero = herosData.poolType;
            LevelManager.Instance.SpawnHero();
        };
    } 
}
