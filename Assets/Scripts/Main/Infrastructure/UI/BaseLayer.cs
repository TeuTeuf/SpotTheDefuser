using Main.Domain.UI;
using Main.Domain.UI.Layers;
using UnityEngine;

namespace Main.Infrastructure.UI
{
    public abstract class BaseLayer : MonoBehaviour, IViewLayer
    {
        private IViewLayer _viewLayerImplementation;

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public abstract View GetView();
    }
}