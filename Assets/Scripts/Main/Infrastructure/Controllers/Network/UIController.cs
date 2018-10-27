using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
	public class UIController : MonoBehaviour
	{
		public Text UIDebugMessage;
		
		private AllPlayerControllers _allPlayerControllers;
		
		public string PlayerName { get; private set; }

		[Inject]
		public void Init(AllPlayerControllers allPlayerControllers)
		{
			_allPlayerControllers = allPlayerControllers;
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
