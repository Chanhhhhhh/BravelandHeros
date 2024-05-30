using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    private Renderer rend;
    private Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if (!GameManager.IsState(GameState.GamePlay))
        {
            return;
        }
        if (LevelManager.Instance.CheckNodeFull(this))
        {
            return;
        }
        if (LevelManager.Instance.IsSelectedNode)
        {
            if(LevelManager.Instance.NodeSelect == this)
            {
                LevelManager.Instance.ClearNode();
            }
            else
            {
                LevelManager.Instance.NodeSelect.ExitSelect();
                Selected();             
            }
        }
        else
        {
            Selected();
        }
    }

     public void ExitSelect()
    {
        rend.material.color = startColor;
    }

    public void Selected()
    {
        LevelManager.Instance.IsSelectedNode = true;
        LevelManager.Instance.NodeSelect = this;
        rend.material.color = hoverColor;
    }
}
