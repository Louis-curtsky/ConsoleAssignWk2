using System;
using System.Collections.Generic;//need this to use List
using System.Text;
using System.Linq;
/*
 * Hangman game
 * User input a letter or a word and system will decide the user is play by a letter or by word
 * User has 10 tries to guess
 * For guess by letter, every right letter will be appear in the secret word in the rigjt position
 * For those not been guess or not been guess correctly will be show as "-"
 * Repeat guess of letter will not be consume
 * Louis Lim - Feb 2022
 */
namespace ConsoleAssignWk2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random position = new Random();
            bool complete = false;
            bool guessBychar = false;
            int index = position.Next(0,6);
//            int index = 1;
            int count = 1;
            string secretWord = FromArray(index);
//            Console.WriteLine("New guess is "+FromArray(index));
            char[] secretChar = secretWord.ToCharArray();
            UpperCaseText(secretChar);
            string userword = AskUserAword("Enter your guess: ");
            char[] storeWord = userword.ToCharArray();
            char[] displayGus = secretWord.ToCharArray();
            StringBuilder userWrongLtr = new StringBuilder();
            userWrongLtr.Append("Wrong Guess: ");

            Array.Fill(displayGus, '_');
            UpperCaseText(storeWord);

            if (storeWord.Length == 1)
            {
                guessBychar = true;
            }
            else
            {
                guessBychar = false;

            }

            do
            {
                if (guessBychar)
                {
                    complete = GuessByLetter(complete, storeWord, secretChar, displayGus, userWrongLtr);
                }
                else
                {
                    complete = GuessByWord(complete, secretWord, userword);
 //                   Console.WriteLine("GBW");
                }

                Console.WriteLine($"You have {10-count} left to guess!!!");
                ++count;
                if (!complete)
                {
                    userword = AskUserAword("Enter next guest:");
                    storeWord = userword.ToCharArray();
                    UpperCaseText(storeWord);
                }
            }// end of DO
            while (count < 10 && !complete);
            Console.WriteLine($"The correct animal is {secretWord} and you had made {count} guess!!!");
        } // End of Main

        private static void UpperCaseText(char[] secretChar)
        {
            string temp = new string(secretChar);
            temp = temp.ToUpperInvariant();

            for (int i = 0; i < secretChar.Length; i++)
            {
                secretChar[i] = temp[i];
            }
        }
// End Upper Case Text Method
        private static bool GuessByWord(bool complete, string secretword, string userword)
        {
            if (userword.ToUpper() == secretword.ToUpper())
                complete = true;
            return complete;
        }
// End of Guess By Word Method
        private static bool GuessByLetter(bool complete, char [] storeWord, char[] secretChar, char[] displayGus, StringBuilder userWrongLtr)
        {
            int numRight = 0;
            bool inPutBefore = false;
            bool wrongGuess = false; 
       
     
            for (int i=0; i<userWrongLtr.Length; i++)
            {
                if (storeWord[0] == userWrongLtr[i])
                {
                    inPutBefore = true;
                }

            } 

            if (!inPutBefore)
            {
                for (int i = 0; i < secretChar.Length; i++)
                {
                    if (storeWord[0] == secretChar[i])
                    {
                        displayGus[i] = storeWord[0];
                    }
                    else
                    {
                        wrongGuess = true;
                    }
                }

                for (int i = 0; i < secretChar.Length; i++)
                {
                    if (displayGus[i] == secretChar[i])
                    {
                        numRight++;
                    }
                }
                if (wrongGuess == true && !inPutBefore)
                {
                    userWrongLtr.Append(storeWord);
                    userWrongLtr.Append(',');
                }
                Console.WriteLine(displayGus);
                Console.WriteLine(userWrongLtr);
            }

            if (numRight == secretChar.Length)
                complete = true;

            return complete;
        } // End of GuessALetter


        private static string FromArray(int index)
        {
            string[] animal = new[] { "DEER", "MOOSE", "BOARS", "LION", "ELEPHANT", "KANGAROO", "LEOPARD" };
            return animal[index];
        }

        static string AskUserAword(string desc)
        {
            String word = "";

            while (word == "")
            {

                Console.Write($"{desc} ");
                word = Console.ReadLine();                                
            }

            return word;
        }
    }// End of Program
}
