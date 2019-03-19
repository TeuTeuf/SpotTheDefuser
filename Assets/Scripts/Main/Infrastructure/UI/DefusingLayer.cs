using Main.Domain.UI;

namespace Main.Infrastructure.UI
{
    public class DefusingLayer : BaseLayer
    {
        public override View GetView()
        {
            return View.Defusing;
        }
    }
}