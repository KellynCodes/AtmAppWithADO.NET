using System;

namespace ATM.BLL.Utilities
{
    public class QueryResult
    {
        public static void Result(int numberOfRowAffected, string queryMessage)
        {
            if(numberOfRowAffected > 0)
            {
                Console.WriteLine(queryMessage);
            }
            else
            {
                Console.WriteLine(numberOfRowAffected);
            }
        } 
        
        public static void Result(string queryMessage)
        {
                Console.WriteLine(queryMessage);
        }
    }
}
