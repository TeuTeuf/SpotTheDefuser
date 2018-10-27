using System;
using Main.Domain.UI;
using UnityEngine;

namespace Main.Infrastructure.UI
{
    public class BasicViewLayer : MonoBehaviour, IViewLayer
    {
        public View View { get; set; }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}