using Main.Domain.UI;
using UnityEngine.Serialization;

namespace Main.Infrastructure.UI
{
    public class BasicLayer : BaseLayer
    {
        public View view;

        public override View GetView()
        {
            return view;
        }
    }
}