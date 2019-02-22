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
        private readonly DefuserCounter _defuserCounter;

        public SetNewDefuseAttempt(IRandom random, AllPlayers allPlayers, DefusingState defusingState, DefuserCounter defuserCounter)
        {
            _random = random;
            _allPlayers = allPlayers;
            _defusingState = defusingState;
            _defuserCounter = defuserCounter;
        }

        public virtual void Set()
        {
            _defusingState.CurrentDefuseAttempt = new DefuseAttempt(_random, _defuserCounter, _allPlayers.GetAll());
            Debug.Log("New Defuse Attempt Set!");
        }
    }
}