///Author: Erik Morén
///Version: 1.0
///Date: 2014-10-13
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2MallNS
{
    /// <summary>
    /// KundKlassen, contains all query actions along with the definition of a
    /// customer as well as the list of customers
    /// </summary>
    public class KundKlass
    { 
        /// <summary>
        /// The Kund class, defines a customer
        /// </summary>
        protected class Kund
        {
            public string Namn;
            public int Ålder;
            public float AttBetala;
            public List<String> Varor;
        }
        /// <summary>
        /// The darasource kunder, a list of customers
        /// </summary>
        protected static List<Kund> kunder = new List<Kund>
        {
            new Kund{Namn = "Kalle", Ålder = 18, AttBetala = 1549,
            Varor = new List<String>{"iPhone3GS skärm", "Skyddsfilm, iPhone3GS", "SkullCandy Hörlurar Rosa"}},
            new Kund{Namn = "Kristin", Ålder = 25, AttBetala = 7599,
            Varor = new List<String>{"iPhone6+", "Skyddsfilm, iPhone6+"}},
            new Kund{Namn = "Kerstin", Ålder = 20, AttBetala = 7599,
            Varor = new List<String>{"iPhone6+", "Skyddsfilm, iPhone6+"}},
            new Kund{Namn = "Karl", Ålder = 39, AttBetala = 7499,
            Varor = new List<String>{"iPhone6+"}},
            new Kund{Namn = "Stefan", Ålder = 19, AttBetala = 99,
            Varor = new List<string>{"SmartPhone Vantar"}},
            new Kund{Namn = "Sebastian", Ålder = 18, AttBetala = 21000,
            Varor = new List<string>{"iPhone6+", "MacBook Pro Retina 15t"}}

        };

        /// <summary>
        /// The KundNamn method, prints the names of all customers ordered by age ascending
        /// </summary>
        public void KundNamn()
        {
            var kundnamn = kunder.OrderBy(k => k.Ålder).Select(k => k.Namn);
            foreach (var item in kundnamn)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// HarKöpt method, prints the name and age of all customers who have bought a specific item
        /// </summary>
        /// <param name="vara">item to filter at</param>
        public void HarKöpt(String vara)
        {
            var kundnamn = from k in kunder
                           where k.Varor.Contains(vara)
                           select k;

            foreach (var item in kundnamn)
            {
                Console.WriteLine(item.Namn + ", " + item.Ålder + " år");
            }
        }

        /// <summary>
        /// Find and print all customers (name and age) who has bought an iPhone6+
        /// </summary>
        public void iPhone6Plus()
        {
            var kundnamn = from k in kunder
                           where k.Varor.Contains("iPhone6+")
                           select k;

            foreach (var item in kundnamn)
            {
                Console.WriteLine(item.Namn + ", " + item.Ålder + " år");
            }
        }

        
        /// <summary>
        /// The KunderÄldreÄn method, prints the name of all customers
        /// older than a set age
        /// </summary>
        /// <param name="gräns">Age to filter at</param>
        public void KunderÄldreÄn(int gräns)
        {
            var kunderÄldreÄn = from kund in kunder
                                where kund.Ålder > gräns
                                select kund.Namn;
            foreach (var item in kunderÄldreÄn)
            {
                Console.WriteLine(item);
            }
        }
 
        /// <summary>
        /// Print all the items customers below a certain age have bought
        /// </summary>
        /// <param name="gräns">age to filter at</param>
        public void VarorKunderYngreÄn(int gräns)
        {
            var varor = from kund in kunder
                        where kund.Ålder < gräns
                        select kund.Varor;

            //Flatten the list-of-lists and remove duplicates
            List<String> varorStr = varor.SelectMany(x => x).Distinct().ToList();

            foreach (String vara in varorStr)
            {
                Console.WriteLine(vara);
            }

        }
    
        /// <summary>
        /// Print all customers who bought for more than a specified amount. Order by amount and number of items.
        /// </summary>
        /// <param name="amount">Our specified amount in kronor</param>
        public void KunderKöptMerÄn(int amount)
        {
            var kunderAboveAmount = from kund in kunder
                                    where kund.AttBetala > amount
                                    orderby kund.AttBetala, kund.Varor.Count
                                    select new { Name = kund.Namn, Amount = kund.AttBetala };

            foreach (var k in kunderAboveAmount)
            {
                Console.WriteLine(k.Name + ", " + k.Amount);
            }
        }

        /// <summary>
        /// Print customers who have bought either an iPhone or some iPhone accessory
        /// </summary>
        public void KunderiPhone()
        {
            var myCustomers = from k in kunder
                              where k.Varor.Any(v => v.ToLower().Contains("iphone"))
                              select k;

            foreach (var k in myCustomers)
            {
                Console.WriteLine(k.Namn);
            }
        }

        /// <summary>
        /// Print customers average amount per item. Order by average amount.
        /// </summary>
        public void KundersMedelkostnad()
        {
            var myCustomers = from k in kunder
                              orderby (k.AttBetala / k.Varor.Count)
                              select new { Name = k.Namn, Medelkostnad = k.AttBetala / k.Varor.Count };

            foreach (var k in myCustomers)
            {
                Console.WriteLine(k.Name + ", " + k.Medelkostnad + " kr");
            }
        }
    
    }
    /// <summary>
    /// The program class, where the console handling is taking place
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main method, runs the menue and calls the querys from kundklass
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            KundKlass kc = new KundKlass();

            while (true)
            {
                Console.WriteLine("Välj:");
                Console.WriteLine("1. Visa samtliga namn");
                Console.WriteLine("2. Visa samtliga kunder äldre än X år");
                Console.WriteLine("3. Visa samtliga kunder som köpt iPhone6+");
                Console.WriteLine("4. Visa samtliga kunder som köpt X");
                Console.WriteLine("5. Visa alla varor köpta av kund yngre än X år");
                Console.WriteLine("6. Visa alla kunder som köpt för över X kr");
                Console.WriteLine("7. Visa alla kunder som köpt antingen en iPhone eller ett iPhonetillbehör");
                Console.WriteLine("8. Visa alla kunders medelkostnad per vara (Total Summa/antal varor)");


                switch (Console.ReadLine())
                { 
                    case "1":
                        kc.KundNamn();
                        break;
                    case "2":
                        Console.WriteLine("Ange antal år:");

                        String ageStr = Console.ReadLine();
                        try 
                        {
                            int age = Convert.ToInt32(ageStr);
                            kc.KunderÄldreÄn(age);
                        } 
                        catch (FormatException e)
                        {
                            Console.WriteLine("Felaktigt inmatad ålder");
                        }
                        
                        break;

                    case "3":
                        kc.iPhone6Plus();
                        break;

                    case "4":
                        Console.WriteLine("Ange vara:");
                        String vara = Console.ReadLine();

                        kc.HarKöpt(vara);
                        break;

                    case "5":
                        Console.WriteLine("Ange antal år:");

                        ageStr = Console.ReadLine();
                        try
                        {
                            int age = Convert.ToInt32(ageStr);
                            kc.VarorKunderYngreÄn(age);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Felaktigt inmatad ålder");
                        }

                        break;

                    case "6":
                        Console.WriteLine("Ange summa i kr:");

                        String amountStr = Console.ReadLine();
                        try
                        {
                            int amount = Convert.ToInt32(amountStr);
                            kc.KunderKöptMerÄn(amount);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Felaktigt inmatad summa.");
                        }

                        break;

                    case "7":
                        kc.KunderiPhone();
                        break;

                    case "8":
                        kc.KundersMedelkostnad();
                        break;

                    default:
                        Console.WriteLine("Felaktigt val. Försök igen.");
                        break;
                 }
                Console.WriteLine("");  //Stylistic empty row before the menu is shown.
                //Console.ReadKey();    //Wait for a keypress before returning to the menu.
            }
        }
    }
}
