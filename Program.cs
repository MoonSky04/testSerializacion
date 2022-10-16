using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace testSerializacion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Stream file = new FileStream("/Users/memo0/Documents/UPIICSA/7mo y 8vo/.NET/Practicas/testSerializacion/application.ttf", FileMode.Open);
            Stream file1 = new FileStream("/Users/memo0/Documents/UPIICSA/7mo y 8vo/.NET/Practicas/testSerializacion/application1.ttf", FileMode.Open);
            Stream file2 = new FileStream("/Users/memo0/Documents/UPIICSA/7mo y 8vo/.NET/Practicas/testSerializacion/application2.ttf", FileMode.Open);

            byte[] arreglo = LeerArchivoBinario(file);
            byte[] arreglo1 = LeerArchivoBinario(file1);
            byte[] arreglo2 = LeerArchivoBinario(file2);

            Animal tortuga = ByteAObjeto(arreglo);
            Animal liebre = ByteAObjeto(arreglo1);
            Animal Donatello = ByteAObjeto(arreglo2);

            List<Animal> animals = new List<Animal>();
            animals.Add(tortuga);
            animals.Add(liebre);
            animals.Add(Donatello);

            Thread t1 = new Thread(() => Correr(animals[1]));
            Thread t2 = new Thread(() => Correr(animals[2]));
            Console.WriteLine("La competencia ha iniciado");
            t1.Start();
            t2.Start();;
            Console.ReadKey();
        }

        public static byte[] LeerArchivoBinario(Stream archivo)
        {
            MemoryStream mems = new MemoryStream();
            archivo.CopyTo(mems);
            return mems.ToArray();
        }

        public static Animal ByteAObjeto(byte[] array)
        {
            MemoryStream mems = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            mems.Write(array, 0, array.Length);
            mems.Seek(0, SeekOrigin.Begin);
            Animal animal = (Animal)formatter.Deserialize(mems);
            return animal;

        }

        public static void Correr(Animal animal)
        {
            Char[] nombre = animal.Nombre.ToCharArray();
            Char inicial = nombre[0];
            int velocidad = 100 - animal.Velocidad;

            for (int i = 1; i <= 800; i++)
            {
                Console.WriteLine(inicial);
                Thread.Sleep(velocidad);

                if (i == 400)
                {
                    if (animal.SeDuerme == true)
                    {
                        Console.WriteLine(animal.Nombre + " se mimio");
                        Thread.Sleep(10 * animal.Velocidad);
                        Console.WriteLine("zzzzzzzzzzzzzzzzz...");
                        Console.WriteLine("\n " + animal.Nombre + "Desperto!");
                    }
                }

                if (i == 800)
                {
                    Console.WriteLine("Gano " + animal.Nombre);
                    Environment.Exit(-1);
                }
            }
        }
    }
}

