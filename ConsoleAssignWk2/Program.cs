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
            int count = 0;
            string secretWord = FromArray(index);
//            Console.WriteLine("New guess is "+FromArray(index));
            char[] secretChar = secretWord.ToCharArray();
            UpperCaseText(secretChar);
            string userword = AskUserAword("Enter your guess: ");
            char[] storeWord = userword.ToCharArray();
            char[] displayGus = secretWord.ToCharArray();
            Array.Fill(displayGus, '-');
            UpperCaseText(storeWord);

            if (storeWord.Length == 1)
            {
                guessBychar = true;
            }

            do
            {
                if (guessBychar)
                {

                    complete = GuessByLetter(complete, storeWord, secretChar, displayGus, count);
                }
                else
                {
                    guessBychar = false;
                    complete = GuessByWord(complete, storeWord, secretChar);
                }
                count++;
                userword = AskUserAword("Enter next guest:");
                storeWord = userword.ToCharArray();
                UpperCaseText(storeWord);
                Console.WriteLine($"You have {10 - count} to guess!!!");
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
        private static bool GuessByWord(bool complete, char[] storeWord, char[] secretChar)
        {
            int numRight = 0;

            if (storeWord.Length == secretChar.Length)
            {
                for (int i = 0; i < secretChar.Length; i++)
                {
                    if (storeWord[i] == secretChar[i])
                        numRight++;
                }
            }
            else
            {
                complete = false;
            }
            if (numRight == secretChar.Length && storeWord.Length == secretChar.Length)
                complete = true;
                    
            return complete;
        }
// End of Guess By Word Method
        private static bool GuessByLetter(bool complete, char [] storeWord, char[] secretChar, char[] displayGus, int count)
        {
            int numRight = 0;
            bool inPutBefore = false;
       
            for (int i=0; i<secretChar.Length; i++)
            {
                if (storeWord[0] == displayGus[i])
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
                }

                for (int i = 0; i < secretChar.Length; i++)
                {
                    if (displayGus[i] == secretChar[i])
                    {
                        numRight++;
                    }
                }
            Console.WriteLine(displayGus);
            }
            if (numRight == secretChar.Length)
                complete = true;

            return complete;
        } // End of GuessALetter


        private static string FromArray(int index)
        {
            string[] animal = new[] { "DEER", "MOOSE", "BOARS", "WOLF", "ELEPHANT", "KANGAROO", "LEOPARD" };
            return animal[index];
        }

        static string AskUserAword(string desc)
        {
            String word = "";

            while (word == "")
            {

                Console.Write($"Input {desc}: ");
                word = Console.ReadLine();                                
            }

            return word;
        }
    }// End of Program
}
