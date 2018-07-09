using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;

namespace Main.UseCases.DefuseAttempts
{
    public class SetNewDefuseAttempt
    {
        private readonly IRandom _random;
        private readonly PlayerRepository _playerRepository;
        private readonly DefusingState _defusingState;

        public SetNewDefuseAttempt(IRandom random, PlayerRepository playerRepository, DefusingState defusingState)
        {
            _random = random;
            _playerRepository = playerRepository;
            _defusingState = defusingState;
        }

        public virtual void Set()
        {
            _defusingState.CurrentDefuseAttempt = new DefuseAttempt(_random, _playerRepository.GetAll());
        }
    }
}