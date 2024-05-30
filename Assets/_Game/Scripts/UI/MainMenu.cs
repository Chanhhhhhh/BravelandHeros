using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void OnPlay()
    { 
        this.CloseDirectly();
        GameManager.ChangeState(GameState.GamePlay);
        LevelManager.Instance.OnInit();
    }
}
