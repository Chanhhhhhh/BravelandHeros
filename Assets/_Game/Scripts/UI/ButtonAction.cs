using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ButtonAction : MonoBehaviour
{

    public UnityAction action;
    private RectTransform rectTransform;
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }


    private void OnClick()
    {
        action?.Invoke();
    }
}
