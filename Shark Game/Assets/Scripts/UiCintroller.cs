using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCintroller : MonoBehaviour
{
    public TMP_Text lifeText, scoreText;
    public CanvasGroup instructions;
    public Button goBtn;

    public void Start()
    {
        Instructions(true);
        goBtn.onClick.RemoveAllListeners();
        goBtn.onClick.AddListener(() => Instructions(false));
    }
    public void PrintLife(float val)
    {
        lifeText.text = val.ToString();
    }
    public void PrintScore(float val)
    {
        scoreText.text = val.ToString();
    }

    public void Instructions(bool on)
    {
        if (on)
        {
            CanvasOnOff(instructions, true);
            GameController.instance.sharkController.isControllable = false;
        }
        else
        {
            GameController.instance.sharkController.isControllable = true;
            CanvasOnOff(instructions, false);
        }
    }

     void CanvasOnOff(CanvasGroup canvas, bool on)
    {
        if (on)
        {
            canvas.alpha = 1;
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        }
        else
        {

            canvas.alpha = 0;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }
    }
}
