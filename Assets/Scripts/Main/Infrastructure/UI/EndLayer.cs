using Main.Domain.UI;
using Main.Domain.UI.Layers;
using UnityEngine.UI;

namespace Main.Infrastructure.UI
{
    public class EndLayer: BaseLayer, IEndLayer
    {
        public Text nbBombsDefusedText;

        public void UpdateNbBombsDefused(int nbBombsDefused)
        {
            nbBombsDefusedText.text = nbBombsDefused.ToString();
        }

        public override View GetView()
        {
            return View.End;
        }
    }
}