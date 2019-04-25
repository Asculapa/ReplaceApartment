using System;
using System.IO;

namespace ConsoleApplication1
{
    struct CardFailes
    {
        private int room;
        private int floor;
        private float square;
        private string street;
        private string ownerName;

        private static double maxSquare = 200.23;
        private static double minSquare = 30.24;


        public CardFailes[] swapApartment(CardFailes[] cardFaileses, CardFailes card)
        {
            for (int i = 0; i < cardFaileses.Length; i++)
            {
                if (cardFaileses[i].room == card.room && cardFaileses[i].floor == card.floor &&
                    compareSquare(cardFaileses[i].square, card.square))
                {
                    CardFailes[] newCardFaileses = new CardFailes[cardFaileses.Length - 1];
                    int temp = 0;

                    for (int j = 0; j < newCardFaileses.Length; j++)
                    {
                        if (i != j)
                        {
                            newCardFaileses[j] = cardFaileses[j + temp];
                        }
                        else
                        {
                            temp = 1;
                            newCardFaileses[j] = cardFaileses[j + temp];
                        }
                    }

                    Console.WriteLine("Apartment was changed!");
                    return newCardFaileses;
                }
            }

            Console.WriteLine("Apartment not found !");
            CardFailes[] oldCardFaileses = new CardFailes[cardFaileses.Length + 1];
            for (int i = 0; i < cardFaileses.Length; i++)
            {
                oldCardFaileses[i] = cardFaileses[i];
            }

            oldCardFaileses[oldCardFaileses.Length - 1] = card;
            return oldCardFaileses;
        }

        private bool compareSquare(float first, float second)
        {
            float a = Math.Abs((first - second) / ((first + second) / 2)) * 100;
            return a <= 10;
        }

        public CardFailes[] autoInit(int count)
        {
            CardFailes[] cardFaileses = new CardFailes[count];
            Random r = new Random();
            for (int i = 0; i < count; i++)
            {
                cardFaileses[i].room = r.Next(1, 10);
                cardFaileses[i].floor = r.Next(1, 30);
                cardFaileses[i].square = (float) (r.NextDouble() * (maxSquare - minSquare) + minSquare);
                cardFaileses[i].street = i + " East Edgefield Lane Mount Airy";
                cardFaileses[i].ownerName = "OwnerNumber " + i;
            }

            return cardFaileses;
        }

        public CardFailes[] manualInit(int count)
        {
            CardFailes[] cardFaileses = new CardFailes[count];

            for (int i = 0; i < count; i++)
            {
                cardFaileses[i].room = writeInt("Count rooms: ");
                cardFaileses[i].floor = writeInt("Floor: ");
                cardFaileses[i].square = writeFloat("Square: ");
                cardFaileses[i].street = writeString("Street: ");
                cardFaileses[i].ownerName = writeString("Owner: ");
            }

            return cardFaileses;
        }

        public void outInf(CardFailes[] cards)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                Console.WriteLine("Count rooms: " + cards[i].room);
                Console.WriteLine("Floor: " + cards[i].floor);
                Console.WriteLine("Square: " + cards[i].square);
                Console.WriteLine("Street: " + cards[i].street);
                Console.WriteLine("Owner: " + cards[i].ownerName);
                Console.WriteLine();
            }
        }


        public CardFailes[] fileInit(string path)
        {
            try
            {
                int temp = 0;
                string[] lines = File.ReadAllLines(@path);
                if (lines.Length % 5 == 0)
                {
                    CardFailes[] cardFaileses = new CardFailes[lines.Length / 5];
                    Console.WriteLine(lines.Length % 5);


                    for (int i = 0; i < lines.Length / 5; i++)
                    {
                        cardFaileses[i].room = Convert.ToInt32(lines[temp]);
                        cardFaileses[i].floor = Convert.ToInt32(lines[temp + 1]);
                        cardFaileses[i].square = (float) Convert.ToDouble(lines[temp + 2]);
                        cardFaileses[i].street = lines[temp + 3];
                        cardFaileses[i].ownerName = lines[temp + 4];
                        temp += 5;
                    }
                    return cardFaileses;
                }
                else
                    {
                        Console.WriteLine("Error !");
                        return null;
                    }
                

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error!" + e);
                return null;
            }
        }

        public void writeCardsToFile(CardFailes[] cardFaileses)
        {
            try
            {
                StreamWriter sw = new StreamWriter(@"Cards.txt");

                for (int i = 0; i < cardFaileses.Length; i++)
                {
                    sw.WriteLine(cardFaileses[i].room);
                    sw.WriteLine(cardFaileses[i].floor);
                    sw.WriteLine(cardFaileses[i].square);
                    sw.WriteLine(cardFaileses[i].street);
                    sw.WriteLine(cardFaileses[i].ownerName);
                }

                sw.Close();

                Console.WriteLine("Succesful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error!");
            }
        }

        public Int32 writeInt(string s)
        {
            int a;
            while (true)
            {
                try
                {
                    Console.WriteLine(s);
                    a = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorect data");
                }
            }

            return a;
        }

        public float writeFloat(string s)
        {
            float a;
            while (true)
            {
                try
                {
                    Console.WriteLine(s);
                    a = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorect data");
                }
            }

            return a;
        }

        public string writeString(string s)
        {
            Console.WriteLine(s);
            return Console.ReadLine();
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Program p = new Program();
            p.menu();
        }

        public void menu()
        {
            Console.WriteLine("1.Auto fill");
            Console.WriteLine("2.Manual fill");
            Console.WriteLine("3.Import from file");
            Console.WriteLine("4.Exit");

            CardFailes[] cardFaileses = null;
            CardFailes c = new CardFailes();
            int a = c.writeInt("Choose number ");

            switch (a)
            {
                case 1:
                    int b = c.writeInt("Enter count: ");
                    cardFaileses = c.autoInit(b);
                    break;
                case 2:
                    int bb = c.writeInt("Enter count: ");
                    cardFaileses = c.manualInit(bb);
                    break;
                case 3:
                    string bbb = c.writeString("Entre path: ");
                    cardFaileses = c.fileInit(bbb);
                    if (cardFaileses == null)
                    {
                        Console.WriteLine("Error!");
                        menu();
                        return;
                    }

                    break;
                case 4:
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Error!");
                    menu();
                    return;
            }

            nextmenu(cardFaileses);
        }

        public void nextmenu(CardFailes[] a)
        {
            Console.WriteLine("1.Swap apartment");
            Console.WriteLine("2.OutInf");
            Console.WriteLine("3.Export to file");
            Console.WriteLine("4.Exit");

            CardFailes[] cardFaileses = a;
            CardFailes c = new CardFailes();

            int sk = c.writeInt("Choose number ");

            switch (sk)
            {
                case 1:
                    Console.WriteLine("Describe your apartment");
                    CardFailes temp = c.manualInit(1)[0];
                    cardFaileses = c.swapApartment(cardFaileses, temp);
                    break;
                case 2:
                    c.outInf(cardFaileses);
                    break;
                case 3:
                    c.writeCardsToFile(cardFaileses);
                    break;
                case 4:
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Error!");
                    nextmenu(a);
                    return;
            }

            nextmenu(cardFaileses);
        }
    }
}