using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using UnityEngine;

namespace Main.UseCases.DefuseAttempts
{
    public class SetNewDefuseAttempt
    {
        private readonly IRandom _random;
        private readonly AllPlayers _allPlayers;
        private readonly DefusingState _defusingState;

        public SetNewDefuseAttempt(IRandom random, AllPlayers allPlayers, DefusingState defusingState)
        {
            _random = random;
            _allPlayers = allPlayers;
            _defusingState = defusingState;
        }

        public virtual void Set()
        {
            _defusingState.CurrentDefuseAttempt = new DefuseAttempt(_random, _allPlayers.GetAll());
            Debug.Log("New Defuse Attempt Set!");
        }
    }
}