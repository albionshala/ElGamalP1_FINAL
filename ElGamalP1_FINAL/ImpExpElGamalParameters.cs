using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security.Cryptography;

namespace P1ElGamal
{
   public abstract class ImpExpElGamalParameters : AsymmetricAlgorithm
    {
        [Serializable]
        public struct ElGamalParameters
        {
            public byte[] P;
            public byte[] G;
            public byte[] Y;
            [NonSerialized] public byte[] X;
        } 

        public abstract void ImportParameters(ElGamalParameters import_parameters); //METHOD: Import ElGamalParameters
        public abstract ElGamalParameters ExportParameters(bool export_parameters); //METHOD: Export ElGamalParameter

        public abstract byte[] EncryptData(byte[] _data);
        public abstract byte[] DecryptData(byte[] _data);

        //Gets and Sets the details for key
        public override string ToXmlString(bool _private)
        {
            ElGamalParameters elgamal_params = ExportParameters(_private);
         
            StringBuilder string_builder = new StringBuilder();
            string_builder.Append("<s--------------------KeyValues--------------------------------s>\n\n");
            //public elements
            string_builder.Append("<P>  " + Convert.ToBase64String(elgamal_params.P) + "  </P>\n\n");
            string_builder.Append("<G>  " + Convert.ToBase64String(elgamal_params.G) + "  </G>\n\n");
            string_builder.Append("<Y>  " + Convert.ToBase64String(elgamal_params.Y) + "  </Y>\n\n");

            if (_private)
            {
                // include private key
                string_builder.Append("<X>  " + Convert.ToBase64String(elgamal_params.X) + "  </X>\n\n");
            }
            string_builder.Append("</s--------------------KeyValues--------------------------------s>");
            return string_builder.ToString();
        }

        public override void FromXmlString(String _string)
        {
            // create parameters
            ElGamalParameters elgamal_params = new ElGamalParameters();
 
            XmlTextReader xml_text_reader = new XmlTextReader(new System.IO.StringReader(_string));

            while (xml_text_reader.Read())
            {
                
                if (true || xml_text_reader.IsStartElement())
                {
                    switch (xml_text_reader.Name)
                    {
                        case "P":
                            // set P
                            elgamal_params.P = Convert.FromBase64String(xml_text_reader.ReadString());
                            break;
                        case "G":
                            // set  G
                            elgamal_params.G = Convert.FromBase64String(xml_text_reader.ReadString());
                            break;
                        case "Y":
                            // set  Y
                            elgamal_params.Y = Convert.FromBase64String(xml_text_reader.ReadString());
                            break;
                        case "X":
                            // set  X
                            elgamal_params.X = Convert.FromBase64String(xml_text_reader.ReadString());
                            break;
                    }
                }
            }
            
            ImportParameters(elgamal_params);
        }
    }
}


