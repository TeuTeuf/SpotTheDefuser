using System;
using Main.Domain.DefuseAttempts;
using Main.Domain.UI;
using Main.Domain.UI.Layers;
using Main.Infrastructure.Controllers.Network;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.Infrastructure.UI
{
    public class DefusingLayer : BaseLayer, IDefusingLayer
    {
        public Text timerText;
        public Image bombImage;
        
        private AllBombs _allBombs;
        private AllPlayerControllers _allPlayerControllers;
        
        private float _remainingTime;
        private IDefusingTime _defusingTime;

        [Inject]
        public void Init(AllBombs allBombs, AllPlayerControllers allPlayerControllers, IDefusingTime defusingTime)
        {
            _defusingTime = defusingTime;
            _allPlayerControllers = allPlayerControllers;
            _allBombs = allBombs;
        }
        
        public void UpdateDisplayedBomb(string bombId, bool isPlayerDefuser)
        {
            var bomb = _allBombs.GetByBombId(bombId);
            bombImage.sprite = bomb.GetSprite(isPlayerDefuser);
        }

        public void UpdateTimer(float remainingTime)
        {
            _remainingTime = remainingTime;
            UpdateDisplayedRemainingTime();
        }

        public void OnClickOnDefuse()
        {
            _allPlayerControllers.TryToDefuseOnServer();
        }

        public override View GetView()
        {
            return View.Defusing;
        }

        public void Update()
        {
            _remainingTime -= _defusingTime.GetDeltaTime();
            UpdateDisplayedRemainingTime();
        }

        private void UpdateDisplayedRemainingTime()
        {
            var remainingTimeAsTimeSpan = TimeSpan.FromSeconds(_remainingTime);
            timerText.text = remainingTimeAsTimeSpan.ToString(@"mm\:ss\:ff");
        }
    }
}