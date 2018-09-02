using Main.Domain.Players;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
	public class UIController : MonoBehaviour
	{
		[Inject] public AllPlayerControllers AllPlayerControllers;
		
		public string PlayerName { get; private set; }

		public void OnEndEditOnPlayerName(string playerName)
		{
			PlayerName = playerName;
		}

		public void OnClickOnAddPlayer()
		{
			AllPlayerControllers.AddNewPlayerOnServer(PlayerName);
		}

		public void OnClickOnNewDefuseAttempt()
		{
			AllPlayerControllers.SetNewDefuseAttemptOnServer();
		}

		public void OnClickOnTryToDefuse()
		{
			AllPlayerControllers.TryToDefuseOnServer();
		}

//		[ClientRpc]
//		public virtual void RpcOnDefuseTried(bool defuseSucceeded, Player player)
//		{
//			Debug.Log($"{player.Name} tried to defuse. Success: {defuseSucceeded}");
//		}
	}
}
