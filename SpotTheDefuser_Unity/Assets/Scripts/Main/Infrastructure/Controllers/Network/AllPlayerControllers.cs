using System.Collections.Generic;
using System.Collections.ObjectModel;
using Main.Domain.Players;

namespace Main.Infrastructure.Controllers.Network
{
    public class AllPlayerControllers
    {
        public PlayerController LocalPlayerController { get; set; }
        
        private readonly List<PlayerController> _playerControllersOnServer;
        
        public AllPlayerControllers()
        {
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

        public virtual void SetNewDefuseAttemptOnServer()
        {
            LocalPlayerController.CmdSetNewDefuseAttempt();
        }

        public virtual void AddNewPlayerOnServer(string playerName)
        {
            LocalPlayerController.CmdAddNewPlayer(playerName);
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
    }
}