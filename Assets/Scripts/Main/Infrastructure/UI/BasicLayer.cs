using Main.Domain.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Main.Infrastructure.UI
{
    public class BasicLayer : BaseLayer
    {
        [FormerlySerializedAs("View")] public View view;

        public override View GetView()
        {
            return view;
        }
    }
}