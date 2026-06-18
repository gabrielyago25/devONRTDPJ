namespace regservice.Exceptions;
public class MudancaStatusIncorreta : Exception
{
    public MudancaStatusIncorreta()
        : base("Não é possível realizar a mudança de status.")
    {
    }
}