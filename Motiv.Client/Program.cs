using System;
using System.Collections.Generic;
using Motiv.Client.Interfaces;

namespace Motiv.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Keep list here because clients can inject any list they want
            List<string> list = new List<string>();

            list.Add("XM0GGWTX7WUO0Z1K3IXI");
            list.Add("LHUKOOHTN1PN2XO2V1DD");
            list.Add("94BWMADMP480IA03THLQ");
            list.Add("COO6OE94NUXPOUYXOLM3");
            list.Add("P0CM35IUJ01W2VHJOYW3");
            list.Add("UAWBE3T1DZY0B9E69QYC");
            list.Add("6F2IKBL5KNPOD73W96QT");
            list.Add("YS7Y2HDJMICC20SAUKTD");
            list.Add("DIT1LR8MGR3UO18OU0GA");
            list.Add("M7YFN6JMDLSVCV31XFJ9");

            //Dependency injection could be added later
            //Add abliity to change list...maybe at runtime as well (not enough time to finish)
             ICharacterController controller = new DisplayerFactory(new CharacterRecognition(list, new CharMapperFactory()));

            Console.WriteLine("\n Display highest char count per row \n-------------------------------------------------------\n");

            var highestOccurenceRow = controller.CreateHighestOccurrPerRow();            
            highestOccurenceRow.Display(false);

            Console.WriteLine("\n Display lowest char count per row \n-------------------------------------------------------\n");
            var lowestOccurenceRow = controller.CreateLowestOccurrPerRow();
            lowestOccurenceRow.Display(false);

            Console.WriteLine("\n display every char count \n-------------------------------------------------------\n");
            var allNumeric = controller.CreateAllNumeric();
            var allNonNumeric = controller.CreateAllNonNumeric();
            allNumeric.Display(false);
            allNonNumeric.Display(false);

            Console.WriteLine("\n Display Max char count in all \n-------------------------------------------------------\n");
            var maxCharCount = controller.CreateMaxCharCountInList();
            maxCharCount.Display(true);

            Console.WriteLine("\n Display Min char count in all \n-------------------------------------------------------\n");
            var minCharCount = controller.CreateMinCharCountInList();
            minCharCount.Display(true);

            Console.ReadLine();
        }
    }
}
