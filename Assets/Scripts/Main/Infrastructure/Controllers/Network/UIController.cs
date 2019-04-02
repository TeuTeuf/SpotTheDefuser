using Main.Domain.Players;
using Main.Domain.UI;
using Main.Domain.UI.Layers;
using Main.UseCases.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
	public class UIController : MonoBehaviour, IUIController
	{
		public View startingView;
		
		private ChangeCurrentView _changeCurrentView;
		private ILobbyLayer _lobbyLayer;
		private IDefusingLayer _defusingLayer;
		private IEndLayer _endLayer;

		[Inject]
		public void Init(ChangeCurrentView changeCurrentView, ILobbyLayer lobbyLayer, IDefusingLayer defusingLayer, IEndLayer endLayer)
		{
			_endLayer = endLayer;
			_defusingLayer = defusingLayer;
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

		public void UpdateDefusing(string defuseAttemptBombId, bool isPlayerDefuser)
		{
			_defusingLayer.UpdateDisplayedBomb(defuseAttemptBombId, isPlayerDefuser);
		}

		public void UpdateEnd(int nbBombsDefused)
		{
			_endLayer.UpdateNbBombsDefused(nbBombsDefused);
		}
	}
}
