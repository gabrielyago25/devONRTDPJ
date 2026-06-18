using System.Text.RegularExpressions;

namespace regservice.Validators;

public static class CpfCnpjValidador
{
    public static bool Valido(string cpfCnpj)
    {
        var apenasNumeros = Regex.Replace(cpfCnpj, @"\D", "");
        return apenasNumeros.Length switch
        {
            11 => CpfValido(apenasNumeros),
            14 => CnpjValido(apenasNumeros),
            _ => false
        };
    }
    private static bool CpfValido(string cpf)
    {
        if (cpf.Length != 11)
            return false;

        if (cpf.Distinct().Count() == 1)
            return false;

        var soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(cpf[i].ToString()) * (10 - i);

        var resto = soma % 11;
        var digito1 = resto < 2 ? 0 : 11 - resto;

        if (digito1 != int.Parse(cpf[9].ToString()))
            return false;

        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(cpf[i].ToString()) * (11 - i);

        resto = soma % 11;
        var digito2 = resto < 2 ? 0 : 11 - resto;

        return digito2 == int.Parse(cpf[10].ToString());
    }

    public static bool CnpjValido (string cnpj)
    {
        if (cnpj.Distinct().Count() == 1)
            return false;
        
        int[] peso1 = {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
        int[] peso2 = {6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};

        var soma = 0;

        for (int i = 0; i < 12; i++)
            soma += (cnpj[i] - '0') * peso1[i];

        var digito1 = soma % 11 < 2 ? 0 : 11 - soma % 11;

        if (digito1 != cnpj[12] - '0')
            return false;

        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += (cnpj[i] - '0') * peso2[i];
        
        var digito2 = soma % 11 < 2 ? 0 : 11 - soma % 11;

        return digito2 == cnpj[13] - '0';
    }
}