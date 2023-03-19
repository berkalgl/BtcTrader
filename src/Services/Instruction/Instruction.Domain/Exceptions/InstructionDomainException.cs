namespace Instruction.Domain.Exceptions
{
    public class InstructionDomainException : Exception
    {
        public InstructionDomainException()
        {
            
        }
        public InstructionDomainException(string message) : base(message)
        {
            
        }
        public InstructionDomainException(string message, Exception innerException) 
            : base(message, innerException)
        {
            
        }
    }
}
