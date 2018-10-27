using Main.Domain.UI;
using Main.UseCases.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
	public class UIController : MonoBehaviour
	{
		public View StartingView;
		public Text UIDebugMessage;
		
		private ChangeCurrentView _changeCurrentView;
		private AllPlayerControllers _allPlayerControllers;

		public string PlayerName { get; private set; }

		[Inject]
		public void Init(AllPlayerControllers allPlayerControllers, ChangeCurrentView changeCurrentView)
		{
			_changeCurrentView = changeCurrentView;
			_allPlayerControllers = allPlayerControllers;
		}

		public void Start()
		{
			_changeCurrentView.Change(StartingView);
		}

		public void OnEndEditOnPlayerName(string playerName)
		{
			PlayerName = playerName;
		}

		public void OnClickOnAddPlayer()
		{
			_allPlayerControllers.AddNewPlayerOnServer(PlayerName);
		}

		public void OnClickOnNewDefuseAttempt()
		{
			_allPlayerControllers.SetNewDefuseAttemptOnServer();
		}

		public void OnClickOnTryToDefuse()
		{
			_allPlayerControllers.TryToDefuseOnServer();
		}

		public void SetDebugMessage(string debugMessage)
		{
			Debug.Log("Please kill me...");
			Debug.Log(debugMessage);
			UIDebugMessage.text = debugMessage;
		}
	}
}
