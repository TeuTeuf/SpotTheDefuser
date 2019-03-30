using Main.Domain.UI;
using Main.Domain.UI.Layers;

namespace Main.Infrastructure.UI
{
    public class DefusingLayer : BaseLayer, IDefusingLayer
    {
        public override View GetView()
        {
            return View.Defusing;
        }

        public void UpdateDisplayedBomb(string bombId, bool isPlayerDefuser)
        {
            throw new System.NotImplementedException();
        }
    }
}