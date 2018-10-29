using System;
using Main.Domain.UI;
using Main.UseCases.UI;
using UnityEngine;
using Zenject;

public class DebugViewLayers : MonoBehaviour {

    private ChangeCurrentView _changeCurrentView;

    [Inject]
    public void Init(ChangeCurrentView changeCurrentView)
    {
        _changeCurrentView = changeCurrentView;
    }
    
    public void ChangeView(View view)
    {
        _changeCurrentView.Change(view);
    }
    
    public void ChangeView(string strView)
    {
        View view;
        Enum.TryParse(strView, out view);
        _changeCurrentView.Change(view);
    }
}
