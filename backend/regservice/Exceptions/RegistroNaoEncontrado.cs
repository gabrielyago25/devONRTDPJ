namespace regservice.Exceptions;
public class RegistroNaoEncontrado : Exception
{
    public RegistroNaoEncontrado()
        : base("O registro não foi encontrado.")
    {
    }
}