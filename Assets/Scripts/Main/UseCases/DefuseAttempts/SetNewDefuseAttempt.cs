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
        private readonly AllBombs _allBombs;
        private readonly DefusingState _defusingState;
        private readonly DefuserCounter _defuserCounter;
        private readonly INewDefuseAttemptSetListener _newDefuseAttemptSetListener;

        public SetNewDefuseAttempt(IRandom random, AllPlayers allPlayers, AllBombs allBombs,
            DefusingState defusingState, DefuserCounter defuserCounter,
            INewDefuseAttemptSetListener newDefuseAttemptSetListener)
        {
            _newDefuseAttemptSetListener = newDefuseAttemptSetListener;
            _allBombs = allBombs;
            _random = random;
            _allPlayers = allPlayers;
            _defusingState = defusingState;
            _defuserCounter = defuserCounter;
        }

        public virtual void Set()
        {
            var defuseAttempt = new DefuseAttempt(_random, _defuserCounter, _allBombs, _allPlayers.GetAll());
            _defusingState.CurrentDefuseAttempt = defuseAttempt;
            _newDefuseAttemptSetListener.OnNewDefuseAttemptSet(defuseAttempt);
        }
    }
}