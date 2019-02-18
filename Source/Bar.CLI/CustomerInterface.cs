using System;

namespace Bar.CLI
{
    internal class CustomerInterface : ICustomerInterface
    {
        public string Listen()
        {
            Console.Write("> ");
            return Console.ReadLine().Trim().ToLower();
        }

        public void Pause() => Console.WriteLine();

        public void Say(string sentence) => Console.WriteLine(sentence);
    }

    internal interface ICustomerInterface
    {
        string Listen();
        void Pause();
        void Say(string sentence);
    }
}
