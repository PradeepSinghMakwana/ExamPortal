using System;
using System.Collections.Generic;
using System.Text;

namespace ExamPortal.Generators
{
    class OTPGenerator
    {
        public string password { get; set; }
        public enum OTPOptions {
            Alphanumeric,
            Numeric,
            AlphabetsOnly
        }
        public OTPGenerator(OTPOptions OTPOption, int size) {
            // characters store all characters used to randomly generate the code.
            string characters = "01";
            switch (OTPOption) {
                case OTPOptions.Alphanumeric:
                    characters = "ABCDEFGHIJKLMNOPQRSTUVXWYZ0123456789";
                    break;
                case OTPOptions.Numeric:
                    characters = "0123456789";
                    break;
                case OTPOptions.AlphabetsOnly:
                    characters = "ABCDEFGHIJKLMNOPQRSTUVXWYZ";
                    break;
                default: throw new Exception("Password generation option not valid.");
            }
            password = GeraHash(characters, size);
        }
        string GeraHash(string characters, int qtd) {
            int QuantidadeCharacters = characters.Length;
            QuantidadeCharacters-=1;
            StringBuilder Hash = new StringBuilder();
            Random random = new Random();
            for(int x = 1; x <= qtd; x=x+1) {
                int Posicao = random.Next(0, QuantidadeCharacters);
                Hash.Append(characters.Substring(Posicao,1));
            }
            return Hash.ToString();
        }
    }
}
