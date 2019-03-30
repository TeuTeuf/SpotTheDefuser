using Main.Domain.DefuseAttempts;
using Main.Domain.UI;
using Main.Domain.UI.Layers;
using UnityEngine.UI;
using Zenject;

namespace Main.Infrastructure.UI
{
    public class DefusingLayer : BaseLayer, IDefusingLayer
    {
        public Text countdownText;
        public Image bombImage;
        private AllBombs _allBombs;

        [Inject]
        public void Init(AllBombs allBombs)
        {
            _allBombs = allBombs;
        }
        
        public void UpdateDisplayedBomb(string bombId, bool isPlayerDefuser)
        {
            var bomb = _allBombs.GetByBombId(bombId);
            bombImage.sprite = bomb.GetSprite(isPlayerDefuser);
        }

        public override View GetView()
        {
            return View.Defusing;
        }
    }
}