using System;
using System.Text;
using System.Security.Cryptography;

namespace P1ElGamal
{
    class Program
    {
        static void Main()
        {
            Console.Write("Type your PLAINTEXT: ");
            string _plaintext = Console.ReadLine();
            byte[] plaintext = Encoding.ASCII.GetBytes(_plaintext);

            //generate keys
            ImpExpElGamalParameters _parameters = new ImplementationClass();

            // set key size
            _parameters.KeySize = 384;

            // extract and print the xml string
            string xml_string = _parameters.ToXmlString(true);
            Console.WriteLine("\n{0} \n", xml_string);

            ImpExpElGamalParameters encrypt = new ImplementationClass();
    
            encrypt.FromXmlString(_parameters.ToXmlString(false));
            byte[] ciphertext = _parameters.EncryptData(plaintext);
          
            // decrypt instance
            ImpExpElGamalParameters decrypt = new ImplementationClass();

            decrypt.FromXmlString(_parameters.ToXmlString(true));
            byte[] potential_plaintext = decrypt.DecryptData(ciphertext);
            
            Console.WriteLine("\n\nPLAINTEXT: {0} \n\nCIPHERTEXT: {1} \n\nDECRYPTED: {2} \n\n", Encoding.ASCII.GetString(plaintext),
               Convert.ToBase64String(ciphertext), Encoding.ASCII.GetString(potential_plaintext));
            Console.ReadLine();
        }

    }
    
}
