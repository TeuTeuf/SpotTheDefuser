using System;

namespace Main.Domain.Players
{
    public class LocalPlayerAlreadySetException : Exception
    {
        private const string ExceptionMessage = "Local Player has already been set.";
        
        public LocalPlayerAlreadySetException() : base(ExceptionMessage) {}
    }
}