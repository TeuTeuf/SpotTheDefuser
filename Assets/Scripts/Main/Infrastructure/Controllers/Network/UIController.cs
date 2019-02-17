using Main.Domain.Players;
using Main.Domain.UI;
using Main.UseCases.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
	public class UIController : MonoBehaviour, IUIController
	{
		[FormerlySerializedAs("StartingView")] public View startingView;
		
		private ChangeCurrentView _changeCurrentView;
		private ILobbyLayer _lobbyLayer;


		[Inject]
		public void Init(ChangeCurrentView changeCurrentView, ILobbyLayer lobbyLayer)
		{
			_lobbyLayer = lobbyLayer;
			_changeCurrentView = changeCurrentView;
		}

		public void Start()
		{
			_changeCurrentView.Change(startingView);
		}

		public void UpdateLobby(Player[] allPlayers)
		{
			_lobbyLayer.UpdatePlayerList(allPlayers);
			Debug.Log($"Update Lobby, nb allPlayers: {allPlayers.Length}");
		}
	}
}
