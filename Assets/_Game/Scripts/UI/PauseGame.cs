using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : UICanvas
{

    public void OnResume()
    {
        this.CloseDirectly();
        GameManager.ChangeState(GameState.GamePlay);
    }

    public void OnReStart()
    {
        this.CloseDirectly();
        GameManager.ChangeState(GameState.GamePlay);
        SimplePool.CollectAll();
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
