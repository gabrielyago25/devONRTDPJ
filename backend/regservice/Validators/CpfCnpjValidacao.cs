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
    public static bool CpfValido(string cpf)
    {
        if (cpf.Distinct().Count() == 1)
            return false;
        
        var soma = 0;
        for (int i = 0; i < 9; i++)
            soma =+ (cpf[i] - '0') * (10-1);

            var digito1 = soma % 11 < 2 ? 0 : 11 - soma % 11;

            if (digito1 != cpf[9] - '0')
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (cpf[i] - '0') * (11 - i);
            
            var digito2 = soma % 11 < 2 ? 0 : 11 - soma % 11;
                return digito2 == cpf[10] - '0';
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