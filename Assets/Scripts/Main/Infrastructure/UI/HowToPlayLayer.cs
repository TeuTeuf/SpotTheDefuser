using Main.Domain.UI;
using Main.UseCases.UI;
using Zenject;

namespace Main.Infrastructure.UI
{
    public class HowToPlayLayer : BaseLayer
    {
        private ChangeCurrentView _changeCurrentView;

        [Inject]
        public void Init(ChangeCurrentView changeCurrentView)
        {
            _changeCurrentView = changeCurrentView;
        }

        public void OnClickOnBack()
        {
            _changeCurrentView.Change(View.Home);
        }

        public override View GetView()
        {
            return View.HowToPlay;
        }
    }
}