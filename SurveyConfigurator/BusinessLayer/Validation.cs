using DatabaseLayer;
using ErrorLogger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utilities;

namespace BusinessLayer
{
    public static class Validation
    {

        public static bool CanConnect()
        {
            return Database.CanConnect();

        }

        public static DataTable ListQuestions()
        {
            return Database.ListQuestions();
        }

        //if NewSmileyQuestion is set to true = database excutes insert command
        //if false -> update command
        public static int SmileyInputValidation(clsQuestionSmiley Question, int Id = 0, bool NewQuestion = true)
        {

            //check that question type is selected
            if (Question.Type is null)
            {
                return -2;
            }

            //check that question text is selected
            if (Question.Text == "")
            {
                return -4; ;
            }

            //check that number of smileys is between 2 and 5
            //this isnt possible to throw and exception but its there for future changes 
            if (Question.NumberOfSmileys > 5 || Question.NumberOfSmileys < 2)
            {
                return -20;
            }

            //layer 3 object to save in sql server
            //NewSmileyQuestion is the flag to know if we should create new question or update previous
            if (NewQuestion)
            {
                return Database.StoreQuestion(Question);

            }
            //to update instead of creating new question
            //if flag = false -> only change text and number of smileys -> update not insert
            else
            {
                int SetTextCode = Database.SetText(Id, Question.Text);
                if (SetTextCode == 1)
                    return Database.SetNumberOfSmileys(Id, Question.NumberOfSmileys);
                else
                    return SetTextCode;
            }




        }

        public static int StarsInputValidation(clsQuestionStar Question, int Id = 0, bool NewQuestion = true)
        {
            //check that question type is selected
            if (Question.Type is null)
            {
                return -2;
            }

            //check that question text is selected
            if (Question.Text == "")
            {
                return -4; ;
            }

            //check that number of stars is between 1 and 10
            //this isnt possible to throw an error but its there for future changes

            if (Question.NumberOfStars > 10 || Question.NumberOfStars < 1)
            {
                return -21;
            }
            //layer 3 object to save in sql server
            //NewSmileyQuestion is the flag to know if we should create new question or update previous
            if (NewQuestion)
            {
               return Database.StoreQuestion(Question);
            }
            //if flag = false -> only change text and number of stars -> update not insert
            else
            {
                int SetTextCode = Database.SetText(Id, Question.Text);
                if (SetTextCode == 1)
                    return Database.SetNumberOfStars(Id, Question.NumberOfStars);
                else 
                    return SetTextCode;
            }
        }



        public static int SliderInputValidation(clsQuestionSlider Question, int Id = 0, bool NewQuestion = true)
        {
            //check that question type is selected
            if (Question.Type is null)
            {
                return -2;
            }

            //check that question text is selected
            if (Question.Text == "")
            {
                return -4; ;
            }

            //check that start value is > 0
            //this isnt possible to throw an error but its there for future changes
            if (Question.StartValue < 0)
            {
                return -22;
            }
            //check that end value is < 100
            if (Question.EndValue > 100)
            {
                return -23;
            }
            //check that start value is less than end value
            if (Question.StartValue > Question.EndValue)
            {
                return -24;
            }

            if (Question.StartCaption.Length > 50)
            {
                return -25;
            }


            if (Question.EndCaption.Length > 50)
            {
                return -26;
            }

            //layer 3 object to save in sql server
            //NewSmileyQuestion is the flag to know if we should create new question or update previous
            if (NewQuestion)
            {
                return Database.StoreQuestion(Question);
            }
            else
            {
                //return codes
                int SetTextCode = Database.SetText(Id, Question.Text);
                if (SetTextCode == 1)
                {
                    int SetStartValueCode=Database.SetStartValue(Id, Question.StartValue);
                    if (SetStartValueCode == 1)
                    {
                        int SetStartCaptionCode = Database.SetStartCaption(Id, Question.StartCaption);
                        if (SetStartCaptionCode == 1)
                        {
                            int SetEndValueCode=Database.SetEndValue(Id, Question.EndValue);
                            if (SetEndValueCode == 1)
                                return Database.SetEndCaption(Id, Question.EndCaption);
                            else return SetEndValueCode;
                        }
                        else return SetStartCaptionCode;
                    }
                    else return SetStartValueCode;
                }
                else return SetTextCode;


            }



        }

        public static int DeleteQuestion(int Id)
        {
           return Database.DeleteQuestion(Id);
        }
        public static string GetType(int Id)
        {
            return Database.GetType(Id);
        }
        public static string GetText(int Id)
        {
            return Database.GetText(Id);
        }
        public static int SetText(int Id, string Text)
        {
           return Database.SetText(Id, Text);
        }

        public static int StoreQuestion(clsQuestionSmiley Question)
        {
            return Database.StoreQuestion(Question);
        }

        public static int GetNumberOfSmileys(int Id)
        {
            return Database.GetNumberOfSmileys(Id);
        }
        public static int SetNumberOfSmileys(int Id, int NumberOfSmileys)
        {
            return Database.SetNumberOfSmileys(Id, NumberOfSmileys);
        }
        public static int GetNumberOfStars(int Id)
        {
            return Database.GetNumberOfStars(Id);
        }
        public static int SetNumberOfStars(int Id, int NumberOfStars)
        {
            return Database.SetNumberOfStars(Id, NumberOfStars);
        }
        public static int GetStartValue(int Id)
        {
            return Database.GetStartValue(Id);
        }
        public static int GetEndValue(int Id)
        {
            return Database.GetEndValue(Id);
        }
        public static string GetStartCaption(int Id)
        {
            return Database.GetStartCaption(Id);
        }
        public static string GetEndCaption(int Id)
        {
            return Database.GetEndCaption(Id);
        }
        public static int SetStartValue(int Id, int StartValue)
        {
            return Database.SetStartValue(Id, StartValue);
        }
        public static int SetEndValue(int Id, int EndValue)
        {
            return Database.SetEndValue(Id, EndValue);
        }
        public static int SetStartCaption(int Id, string StartCaption)
        {
            return Database.SetStartCaption(Id, StartCaption);
        }
        public static int SetEndCaption(int Id, string EndCaption)
        {
            return Database.SetEndCaption(Id, EndCaption);
        }
    }
}
