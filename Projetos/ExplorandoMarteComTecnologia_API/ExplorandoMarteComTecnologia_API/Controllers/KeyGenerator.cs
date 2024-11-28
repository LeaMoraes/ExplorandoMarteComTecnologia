using ExplorandoMarteComTecnologia_API.DTO;
using System.Text.RegularExpressions;

namespace ExplorandoMarteComTecnologia_API.Controllers
{
    public class KeyGenerator
    {

        private readonly Random random = new Random();

        public List<IngressoDTO> AtribuirKey(List<IngressoDTO> ingressoDTO, string email)
        {
            List<IngressoDTO> ingressoComKey = new List<IngressoDTO>();

            foreach (var ingressoObjectDTO in ingressoDTO)
            {
                ingressoObjectDTO.Key = GenerateKey(ingressoObjectDTO.Nome, email);

                if (!string.IsNullOrEmpty(ingressoObjectDTO.Key) && ingressoObjectDTO.Key.Length == 9)
                {
                    ingressoComKey.Add(ingressoObjectDTO);
                }
                else
                {
                    throw new Exception($"Erro ao gerar a Key para {ingressoObjectDTO.Nome}. A Key deve ter exatamente 9 caracteres e não pode ser vazia.");
                }
            }

            return ingressoComKey;
        }

        private string GenerateKey(string nome, string email)
        {            

            // Remove caracteres não alfanuméricos e converte para maiúsculas
            string nomeParte = Regex.Replace(nome, @"[^a-zA-Z]", "").ToUpper();
            string emailParte = Regex.Replace(email, @"[^a-zA-Z0-9]", "").ToUpper();

            // Garantindo que cada parte tenha no mínimo 3 caracteres
            string keyParteNome = nomeParte.Length >= 3 ? nomeParte.Substring(0, 3) : nomeParte.PadRight(3, 'X');
            string keyParteEmail = emailParte.Length >= 3 ? emailParte.Substring(0, 3) : emailParte.PadRight(3, 'Y');

            // Embaralha e converte as partes do nome e email
            string shuffledNomeParte = ShuffleString(keyParteNome);
            string shuffledEmailParte = ShuffleString(keyParteEmail);

            //Converte alguns caracteres para Alfanumerico
            string convertedNomeParte = ConvertToAlphanumeric(shuffledNomeParte);
            string convertedEmailParte = ConvertToAlphanumeric(shuffledEmailParte);

            // Gera uma parte numérica aleatória de 3 dígitos
            string randomPart = random.Next(100, 999).ToString();

            // Gera o Key final
            string keyFinal = ShuffleKey(convertedNomeParte, convertedEmailParte, randomPart);

            if (keyFinal.Length != 9 || string.IsNullOrEmpty(keyFinal))
            {
                return GenerateKey(nome, email);
            }

            return keyFinal;           

        }

        private string ShuffleKey(string keyParteNome, string keyParteEmail, string randomPart)
        {
            int keySelecao = random.Next(1, 7); // Gera um número aleatório entre 1 e 6

            return keySelecao switch
            {
                1 => keyParteNome + keyParteEmail + randomPart,
                2 => keyParteNome + randomPart + keyParteEmail,
                3 => keyParteEmail + randomPart + keyParteNome,
                4 => randomPart + keyParteEmail + keyParteNome,
                5 => randomPart + keyParteNome + keyParteEmail,
                6 => keyParteEmail + keyParteNome + randomPart,
                _ => keyParteEmail + randomPart + keyParteNome // Default, mas não deve ser chamado
            };
        }

        private string ShuffleString(string input)
        {
            var chars = input.ToCharArray();
            for (int i = chars.Length - 1; i > 0; i--)
            {
                int swapIndex = random.Next(i + 1);
                (chars[i], chars[swapIndex]) = (chars[swapIndex], chars[i]);
            }
            return new string(chars);
        }

        private string ConvertToAlphanumeric(string input)
        {
            // Mapeamento simples de substituições de letras por números
            var substitutionMap = new Dictionary<char, char>
            {
                { 'A', '4' }, { 'E', '3' }, { 'I', '1' }, { 'O', '0' },
                { 'S', '5' }, { 'T', '7' }, { 'B', '8' }, { 'G', '6' },
                { 'Z', '2' }, { 'L', '1' }
            };

            // Substitui letras de acordo com o mapeamento de forma randômica
            return new string(input.Select(c =>
                random.Next(2) == 0 && substitutionMap.ContainsKey(c) ? substitutionMap[c] : c
            ).ToArray());
        }

    }
}
