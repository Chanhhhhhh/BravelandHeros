using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : UICanvas
{
    public void OnRetry()
    {
        this.CloseDirectly();
        GameManager.ChangeState(GameState.GamePlay);
        LevelManager.Instance.OnInit();
        
    }

    public void MainMenu()
    {
        this.CloseDirectly();
        UIManager.Instance.GetUI<GamePlay>().CloseDirectly();
        GameManager.ChangeState(GameState.MainMenu);
        LevelManager.Instance.CloseMap();
    }
}
