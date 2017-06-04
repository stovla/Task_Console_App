using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;

namespace TestApp1
{
    class DataLogic
    {
        public const string server = "localhost";
        public const string userId = "stovla";
        public const string pass = "stovla";
        public const string db = "console_app_db";
   
        InputOutput inputOutput = new InputOutput();
        public const string connectionString = "server=" + server + ";user id=" + userId + ";password=" + pass + ";database=" + db + ";allowuservariables=True";
        public MySqlConnection dbConn = new MySqlConnection(connectionString);
        public string sql = String.Empty;
        public string id = String.Empty;


        
        // handles the input from the database to the text variable
        public string DataFileInput()
        {   
            string commandText = "SELECT * FROM word_counter";
            string listOutput = "";
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            dbConn.Open();

            MySqlCommand command = new MySqlCommand(commandText, dbConn);
            MySqlDataReader dataReader = command.ExecuteReader();

            // this is a simplest way of outputing the information
            // putting all of the database info into one string
            // because writing to the file does not recognize escape "\" character, it will input evertything in one line
            // one of ways to fix this is that we could input the writing function in a loop and/or dataReader in a for loop

            while (dataReader.Read())
            {
                list[0].Add(dataReader["id"] + "");
                list[1].Add("The input is: " + dataReader["word_input"] + "");
                list[2].Add("The number of words in the input is: " + dataReader["word_count"] + "");
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    listOutput += " \n " + list[j][i] + " \n";
                }
            }
            dataReader.Close();
            dbConn.Close();
            // Console.WriteLine(listOutput); // testing the string to be outputed
            return listOutput;
        }

        // handles the input to the database, (output refers to the choice of the user)
        public void DataOutput(string newText, string count)
        {
            string text = newText;
            // Console.WriteLine("database input try with " + text);
            try
            {
                MySqlConnection dbConn = new MySqlConnection(connectionString);
                dbConn.Open();
                string commandText = "INSERT INTO word_counter (word_input, word_count) VALUES (@word_input, @Word_count)";
                MySqlCommand command = new MySqlCommand(commandText, dbConn);
                command.Parameters.AddWithValue("@word_input", text);
                command.Parameters.AddWithValue("@word_count", count);
                command.ExecuteNonQuery();
                Console.WriteLine("\n Input: - " + text + " - saved to database!");

            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
