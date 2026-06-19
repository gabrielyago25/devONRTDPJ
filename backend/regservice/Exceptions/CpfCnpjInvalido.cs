namespace regservice.Exceptions;
public class CpfCnpjInvalido : Exception
{
    public CpfCnpjInvalido()
        : base("CPF/CNPJ inválido.")
    {
    }
}