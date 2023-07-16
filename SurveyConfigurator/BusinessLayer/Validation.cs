using DatabaseLayer;
using ErrorLogger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        public static void SmileyInputValidation(clsQuestionSmiley Question, int Id = 0, bool NewSmileyQuestion = true)
        {
            //nested try catch blocks because we validate each one separately
            //check that question type is selected
            try
            {
                if (Question.Text =="")
                {
                    throw new ArgumentNullException(nameof(Question.Text));
                }

                //check that question text is selected
                try
                {
                    if (Question.Type is null)
                    {
                        throw new ArgumentNullException(nameof(Question.Type));
                    }
                    //check that number of smileys is between 2 and 5
                    //this isnt possible to throw and exception but its there for future changes
                    try
                    {
                        if (Question.NumberOfSmileys > 5 && Question.NumberOfSmileys < 2)
                        {
                            throw new ArithmeticException(nameof(Question.NumberOfSmileys));
                        }

                        //layer 3 object to save in sql server
                        if (NewSmileyQuestion)
                        {
                            Database StoreSmiley = new Database();
                            StoreSmiley.StoreSmileyQuestion(Question);
                        }
                        //to use update method
                        else 
                        {
                            Database UpdateSmiley = new Database();
                            //UpdateSmiley.SetNumberOfSmileys(Id,Question.NumberOfSmileys);
                        }
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Enter Valid Number of Smileys (2-5).");
                    }
                }
                catch (Exception E)
                {
                    Logger.WriteLog("Question Text should be specified.");
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Question Type should be specified.");
            }
        }

        public static void StarsInputValidation(clsQuestionStar Question)
        {
            //nested try catch blocks because we validate each one separately
            //check that question type is selected
            try
            {
                if (Question.Text == "")
                {
                    throw new ArgumentNullException(nameof(Question.Text));
                }

                //check that question text is selected
                try
                {
                    if (Question.Type is null)
                    {
                        throw new ArgumentNullException(nameof(Question.Type));
                    }

                    //check that number of stars is between 1 and 10
                    //this isnt possible to throw and exception but its there for future changes
                    try
                    {
                        if (Question.NumberOfStars > 10 || Question.NumberOfStars < 1)
                        {
                            throw new ArithmeticException(nameof(Question.NumberOfStars));
                        }
                        //layer 3 object to save in sql server
                        Database StoreStar = new Database();
                        StoreStar.StoreStarQuestion(Question);
                    }
                    catch 
                    {
                        Logger.WriteLog("Enter Valid Number of Stars (1-10).");
                    }
                }
                catch (Exception E)
                {
                    Logger.WriteLog("Question Text should be specified.");
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Question Type should be specified.");
            }
        }

        public static void SliderInputValidation(clsQuestionSlider Question)
        {
            //nested try catch blocks because we validate each one separately
            //check that question type is selected
            try
            {
                if (Question.Text == "")
                {
                    throw new ArgumentNullException(nameof(Question.Text));
                }

                //check that question text is selected
                try
                {
                    if (Question.Type is null)
                    {
                        throw new ArgumentNullException(nameof(Question.Type));
                    }
                    //check that start value is > 0
                    //this isnt possible to throw an exception but its there for future changes
                    try
                    {
                        if (Question.StartValue < 0)
                        {
                            throw new ArgumentOutOfRangeException(nameof(Question.StartValue));
                        }

                        try
                        {
                            if (Question.EndValue > 100 || Question.EndValue < Question.StartValue)
                            {
                                throw new ArgumentOutOfRangeException(nameof(Question.EndValue));
                            }

                            try
                            {
                                if (Question.StartCaption.Length >50)
                                {
                                    throw new ArgumentOutOfRangeException(nameof(Question.StartCaption));
                                }

                                try
                                {
                                    if (Question.EndCaption.Length >50)
                                    {
                                        throw new ArithmeticException(nameof(Question.StartValue));
                                    }

                                    //layer 3 object to save in sql server
                                    Database StoreSlider = new Database();
                                    StoreSlider.StoreSliderQuestion(Question);
                                }
                                catch (Exception E)
                                {
                                    Logger.WriteLog("End Caption Too Long (> 50 characters) ");
                                }
                            }
                            catch (Exception E)
                            {
                                Logger.WriteLog("Start Caption Too Long (> 50 characters) ");
                            }
                        }
                        catch (Exception E)
                        {
                            Logger.WriteLog("Enter Valid End Value - Less than 100, Greater than start.");
                        }
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Enter Valid Start Value - > 0");
                    }
                }
                catch (Exception E)
                {
                    Logger.WriteLog("Question Text should be specified.");
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Question Type should be specified.");
            }
        }

        public static void DeleteQuestion(int Id, string Type)
        {
            Database.DeleteQuestion(Id, Type);
        }
        public static string GetType(int Id)
        {
            return Database.GetType(Id);
        }
        public static string GetText(int Id)
        {
            return Database.GetText(Id);
        }
        public static void SetText(int Id, string Text)
        {
            Database.SetText(Id, Text);
        }
       

        public static int GetNumberOfSmileys(int Id)
        {
            return Database.GetNumberOfSmileys(Id);
        }
        public static void SetNumberOfSmileys(int Id, int NumberOfSmileys)
        {
            Database.SetNumberOfSmileys(Id, NumberOfSmileys);
        }
        public static int GetNumberOfStars(int Id)
        {
            return Database.GetNumberOfStars(Id);
        }
        public static void SetNumberOfStars(int Id, int NumberOfStars)
        {
            Database.SetNumberOfStars(Id, NumberOfStars);
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
        public static void SetStartValue(int Id, int StartValue)
        {
            Database.SetStartValue(Id, StartValue);
        }
        public static void SetEndValue(int Id, int EndValue)
        {
            Database.SetEndValue(Id, EndValue);
        }
        public static void SetStartCaption(int Id, string StartCaption)
        {
            Database.SetStartCaption(Id, StartCaption);
        }
        public static void SetEndCaption(int Id, string EndCaption)
        {
            Database.SetEndCaption(Id, EndCaption);
        }
    }
}
