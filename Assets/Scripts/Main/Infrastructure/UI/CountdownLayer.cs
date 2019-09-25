using Main.Domain.DefuseAttempts;
using Main.Domain.UI;
using Main.UseCases.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.Infrastructure.UI
{
    public class CountdownLayer : BaseLayer
    {
        public const float CountdownDuration = 3f;
        
        public Text countdownText;
        
        private IDefusingTime _defusingTime;
        private ChangeCurrentView _changeCurrentView;

        private float _remainingTime;

        [Inject]
        public void Init(IDefusingTime defusingTime, ChangeCurrentView changeCurrentView)
        {
            _changeCurrentView = changeCurrentView;
            _defusingTime = defusingTime;
        }

        public void Start()
        {
            _remainingTime = CountdownDuration;
        }

        public void Update()
        {
            _remainingTime -= _defusingTime.GetDeltaTime();

            if (_remainingTime < 0f)
            {
                _changeCurrentView.Change(View.Defusing);
            }
            
            countdownText.text = Mathf.CeilToInt(_remainingTime).ToString();
        }

        public override View GetView()
        {
            return View.Countdown;
        }
    }
}