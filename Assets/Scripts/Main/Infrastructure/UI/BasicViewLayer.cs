using Main.Domain.UI;
using UnityEngine;

namespace Main.Infrastructure.UI
{
    public class BasicViewLayer : MonoBehaviour, IViewLayer
    {
        public View View;

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public View GetView()
        {
            return View;
        }
    }
}