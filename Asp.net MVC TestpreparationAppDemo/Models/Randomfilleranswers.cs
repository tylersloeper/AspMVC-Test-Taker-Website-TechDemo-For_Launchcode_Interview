using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp.net_MVC_TestpreparationAppDemo.Models
{
    public static class Randomfilleranswers
    {

        static Random random = new Random();

        //return a random gibberish answer to string.
        public static string Returnfilleranswer()
        {
            string answerVar;
            string[] Filleranswer = new string[100];
            


            //fill all elements of the array with gibberish answers;
            for (int i =0; i<100;i++)
            {
                int j = random.Next(1, 25);
                if(j == 1)
                {
                    Filleranswer[i] = "Burying eternal-return ubermensch enlightenment free";
                }
                else if (j == 2)
                {
                    Filleranswer[i] = "Joy virtues victorious decieve superiority";
                }
                else if (j == 3)
                {
                    Filleranswer[i] = "Madness burying ideal zarathustra virtues";
                }
                else if (j == 4)
                {
                    Filleranswer[i] = "Ocean marvelous pious prejudice justice";
                }
                else if (j == 5)
                {
                    Filleranswer[i] = "Truth ocean law burying";
                }
                else if (j == 6)
                {
                    Filleranswer[i] = "Of aversion victorious value";
                }
                else if (j == 7)
                {
                    Filleranswer[i] = "Endless faith passion prejudice";
                }
                else if (j == 8)
                {
                    Filleranswer[i] = "Reason sexuality strong ideal";
                }
                else if (j == 9)
                {
                    Filleranswer[i] = "Pinnacle convictions";
                }
                else if (j == 10)
                {
                    Filleranswer[i] = "Oneself philosophy";
                }
                else if (j == 11)
                {
                    Filleranswer[i] = "Moral";
                }
                else if (j == 12)
                {
                    Filleranswer[i] = "Enlightenment";
                }
                else if (j == 13)
                {
                    Filleranswer[i] = "Sea";
                }
                else if (j == 14)
                {
                    Filleranswer[i] = "Revaluation joy reason";
                }
                else if (j == 15)
                {
                    Filleranswer[i] = "Moral strong god morality burying battle";
                }
                else if (j == 16)
                {
                    Filleranswer[i] = "Of justice joy ultimate marvelous grandeur";
                }
                else if (j == 17)
                {
                    Filleranswer[i] = "Ideal abstract eternal-return abstract decieve selfish";
                }
                else if (j == 18)
                {
                    Filleranswer[i] = "Madness ideal hope gains self dead";
                }
                else if (j == 19)
                {
                    Filleranswer[i] = "Fearful sea oneself derive decieve against burying";
                }
                else if (j == 20)
                {
                    Filleranswer[i] = "Madness pinnacle overcome insofar holiest. Burying deceptions";
                }
                else if (j == 21)
                {
                    Filleranswer[i] = "Contradict contradict dead strong ocean enlightenment marvelous";
                }
                else if (j == 22)
                {
                    Filleranswer[i] = "Disgust evil of grandeur derive reason right";
                }
                else if (j == 23)
                {
                    Filleranswer[i] = "Victorious chaos war depths derive. Transvaluation philosophy decrepit";
                }
                else if (j == 24)
                {
                    Filleranswer[i] = "Depths joy reason horror ascetic depths ubermensch oneself";
                }
                else if (j == 25)
                {
                    Filleranswer[i] = "Mountains ubermensch philosophy ideal aversion pinnacle joy derive";
                }

                else
                {
                    Filleranswer[i] = "Assignment Error";
                }

            }


            //choose a random element from the array of gibberish answers and return it.
            
            int randomnumber = random.Next(Filleranswer.Length);
            answerVar = Filleranswer[randomnumber];

            return answerVar;
        }

        public static string ReturnCorrectOrIncorrect()
        {
            int j = random.Next(1, 3);
            string status = "incorrect";

            if (j > 1)
            {
                status = "correct";
            }

            return status;
        }



    }
}