using System.Collections.Generic;
using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;

namespace Main.UseCases.DefuseAttempts
{
	public class CreateDefuseAttempt {
		private readonly IRandom _random;
		private readonly PlayerRepository _playerRepository;

		public CreateDefuseAttempt(IRandom random, PlayerRepository playerRepository)
		{
			_random = random;
			_playerRepository = playerRepository;
		}

		public DefuseAttempt Execute()
		{
			var allPlayers = new List<Player>(_playerRepository.GetAll());
			
			var numberOfDefuserPlayers = GetNumberOfDefuserPlayers(allPlayers.Count);
			
			var defuserPlayers = GetDefuserPlayers(numberOfDefuserPlayers, allPlayers);

			return new DefuseAttempt(defuserPlayers);
		}

		private List<Player> GetDefuserPlayers(int numberOfDefuserPlayers, IList<Player> allPlayers)
		{
			var defuserPlayers = new List<Player>();
			for (var i = 0; i < numberOfDefuserPlayers; i++)
			{
				var defuserIndex = _random.Range(0, allPlayers.Count);
				defuserPlayers.Add(allPlayers[defuserIndex]);
				allPlayers.RemoveAt(defuserIndex);
			}

			return defuserPlayers;
		}

		private static int GetNumberOfDefuserPlayers(int nbAllPlayers)
		{
			var isNumberOfPlayersEven = nbAllPlayers % 2 == 0;
			var nbDefuserPlayers = nbAllPlayers / 2;

			if (isNumberOfPlayersEven)
			{
				nbDefuserPlayers--;
			}

			return nbDefuserPlayers;
		}
	}
}
