using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// trying to use Composition of Inheritance pattern since there are no objects needed for the task
namespace TestApp1
{
    class InputOutput
    {
        public static DataLogic dataLogic = new DataLogic();
        public string text = "";
        public string newDbText = "";
        public static int count;
        string choiceWarning = "\n Not correct option!!!\nPlease choose the correct option.\n";
        string pathWarning = "\n Please input the correct file path:\n";

        // *** Console in/out Methods ***
        
        // handle console input
        public string HandleConsoleInput()
        {
            Console.WriteLine("\n Input the string: \n");
            return text = Console.ReadLine();
        }
        // handle console output
        public void HandleConsoleOutput()
        {
            if (text != "")
            {
                Console.WriteLine("\n The input is: " + text + "\n And the number of entered words is: " + count);
            }
        }

        // *** File in/out Methods ***

        // handle file input , reading from a specific file
        public string HandleFileInput()
        {
            Console.WriteLine(pathWarning);
            
            // test with the existing file
            //string path = "C:\\Users\\Neda\\Desktop\\text1.txt";
            
            // console input
            string path = Console.ReadLine();
            if(System.IO.File.Exists(path))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(path))
                {
                    if (sr.EndOfStream != true)
                    {
                        text = sr.ReadToEnd();
                        Console.WriteLine(sr.ReadToEnd());
                        sr.Close();
                       // Console.WriteLine(" the file text is reading after reading" + text);
                    }
                    // Console.WriteLine(" the file text is reading after closing " + text);
                    return text;
                }
            } else
            {
                new Exception("\n The File Does Not Exist");
                Console.WriteLine("\n Incorrect File Path!!!");
                return HandleFileInput();
            }
        }

        // handle the file output, the way you save something to a text file
        public void HandleFileOutput( string newCount)
        {
            //newDbText is getting the information from the database and it resets it self when you choose a different type of input and the same file output
            
            string count = newCount;
            Console.WriteLine("\n Input the correct file path or create a new file by typing any kind of path.\n");
            string path = Console.ReadLine();
            try
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path, true))
                {
                    // check if the text is filled from the database or from other input
                    if (newDbText != "")
                    {
                        sw.WriteLine(newDbText);
                    } else
                    {
                        sw.WriteLine(text);
                    }
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
    
        }
        // *** Data in/out Methods ***

        // output refers to the choice output, but inserts the database
        public void HandleDataOutput()
        {
            string newText = text;
            Console.WriteLine("\n Saved to database.\n");
            string countStr = Convert.ToString(count);
            dataLogic.DataOutput(newText, countStr);
        }
        // handles the data output to a list file or console
        public string HandleDataInput()
        {
            text = dataLogic.DataFileInput();
            Console.WriteLine(text);
            return text;
        }
        
        // *** Choice in/out methods ***
        // handling the choice for input
        public void HandleChoiceInput(string newChoice)
        {
            //choice = Console.Read();
            string choice = newChoice;
            
            if (choice == "1")
            {
                Console.Write("\n Console Input Chosen.");
                text = HandleConsoleInput();      
            } else if(choice == "2")
            {
                Console.Write("\n File Input Chosen.");
                text = HandleFileInput();
            }else if (choice == "3")
            {
                text = HandleDataInput();
            } else
            {
                Console.WriteLine(choiceWarning);
                choice = Console.ReadLine();
                HandleChoiceInput(choice);
            }
     
        }
        // handling the choice for output
        public void HandleChoiceOutput(string newChoice)
        {
            string choice = newChoice;
            if (choice == "1")
            {
                HandleConsoleOutput();
            } else if (choice == "2")
            {
                HandleFileOutput(choice);
            }else if (choice == "3")
            {
                newDbText = "";
                newDbText = dataLogic.DataFileInput();
                HandleDataOutput();
                Console.WriteLine("This should be Database Output");
            } else
            {
                Console.WriteLine(choiceWarning);
                choice = Console.ReadLine();
                HandleChoiceOutput(choice);
            }  
            //WordCount();
        }

        // word counter method
        public void WordCount()
        {
            if (text != "")
            {
                string[] words = text.Split(new char[] { ' ',':',';','!','.',',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    count += 1;
                }

            } else
            {
                Console.WriteLine(new Exception());
            }
        }
        
    }
}
