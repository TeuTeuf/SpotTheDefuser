using Main.Domain.Players;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
	public class UIController : MonoBehaviour
	{
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
	}
}
