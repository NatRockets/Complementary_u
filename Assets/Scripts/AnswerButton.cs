using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    private Button target;
    private Image varImage;

    public void InitAnswer()
    {
        target = GetComponent<Button>();
        varImage = transform.GetChild(0).GetComponent<Image>();
    }
    
    public void ResetAnswer()
    {
        target.onClick.RemoveAllListeners();
        target.interactable = false;
        varImage.enabled = false;
    }

    public void SetAnswer(Color shade, Action callback)
    {
        target.onClick.AddListener(() => callback());
        varImage.color = shade;
        varImage.enabled = true;
        target.interactable = true;
    }
}
