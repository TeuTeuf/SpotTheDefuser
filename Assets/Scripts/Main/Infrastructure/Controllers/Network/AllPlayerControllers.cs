using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using UnityEngine;

namespace Main.Infrastructure.Controllers.Network
{
    public class AllPlayerControllers: IDefuseTriedListener, IPlayerAddedListener, INewGameStartedListener, INewDefuseAttemptSetListener
    {
        private readonly AllPlayers _allPlayers;
        
        public PlayerController LocalPlayerController { get; set; }

        private readonly List<PlayerController> _playerControllersOnServer;
        
        public AllPlayerControllers(AllPlayers allPlayers)
        {
            _allPlayers = allPlayers;
            _playerControllersOnServer = new List<PlayerController>();
        }

        public void AddPlayerControllerOnServer(PlayerController playerController)
        {
            _playerControllersOnServer.Add(playerController);
        }

        public ReadOnlyCollection<PlayerController> GetPlayerControllersOnServer()
        {
            return _playerControllersOnServer.AsReadOnly();
        }

        public virtual void AddLocalPlayerOnServer()
        {
            LocalPlayerController.CmdAddNewPlayer(_allPlayers.LocalPlayer);
        }

        public virtual void StartNewGameOnServer()
        {
            LocalPlayerController.CmdStartNewGame();
        }

        public virtual void TryToDefuseOnServer()
        {
            LocalPlayerController.CmdTryToDefuse();
        }

        public virtual void OnDefuseTried(bool defuseSucceeded, Player player)
        {
            foreach (var playerController in _playerControllersOnServer)
            {
                playerController.RpcOnDefuseTried(defuseSucceeded, player);
            }
        }

        public virtual void OnPlayerAdded(Player player)
        {
            foreach (var playerController in _playerControllersOnServer)
            {
                playerController.RpcOnPlayerAdded(_allPlayers.GetAll().ToArray());
            }
        }

        public void OnNewGameStarted()
        {
            LocalPlayerController.CmdOnNewGameStarted();
            foreach (var playerController in _playerControllersOnServer)
            {
                playerController.RpcOnNewGameStarted();
            }
        }

        public void OnNewDefuseAttemptSet(DefuseAttempt defuseAttempt)
        {
            foreach (var playerController in _playerControllersOnServer)
            {
                playerController.RpcOnNewDefuseAttemptSet(defuseAttempt.BombId);
            }
        }
    }
}