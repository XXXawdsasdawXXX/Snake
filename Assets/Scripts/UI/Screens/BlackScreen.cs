using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Screen = UI.Screens.Screen;

public class BlackScreen : Screen
{
    [SerializeField] private Image _image;
    [SerializeField] private float _duration = 0.7f;

    
    public override void Show(Action onShown = null)
    {
        base.Show();
        _image.DOFade(1, _duration);
    }

    public override void Hide(Action onHidden = null)
    {
        _image.DOFade(0, _duration).OnComplete(() => base.Hide(onHidden));
    }

    
    
    [ContextMenu("Hide")]
    private void EditorHide()
    {
        Hide();
    }
    [ContextMenu("Show")]
    private void EditorShow()
    {
        Show();
    }
}
