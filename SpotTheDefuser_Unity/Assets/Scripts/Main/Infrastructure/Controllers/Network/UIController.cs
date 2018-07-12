using UnityEngine;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
	public class UIController : MonoBehaviour
	{
		[Inject] public AllPlayerControllers AllPlayerControllers;

		private string _playerName;
		
		public void OnEndEditOnPlayerName(string playerName)
		{
			_playerName = playerName;
		}

		public void OnClickOnAddPlayer()
		{
			Debug.Log("OnClickOnAddPlayer with player name: " + _playerName);
		}
		
		public void OnClickOnNewDefuseAttempt()
		{
			AllPlayerControllers.SetNewDefuseAttemptOnServer();
		}
		
		public void OnClickOnTryToDefuse()
		{
			Debug.Log("OnClickOnTryToDefuse");
		}
	}
}
