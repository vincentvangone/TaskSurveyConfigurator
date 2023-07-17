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
        public static void SmileyInputValidation(clsQuestionSmiley Question, int Id = 0, bool NewQuestion = true)
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
                        if (Question.NumberOfSmileys > 5 || Question.NumberOfSmileys < 2)
                        {
                            throw new ArithmeticException(nameof(Question.NumberOfSmileys));
                        }

                        //layer 3 object to save in sql server
                        //NewSmileyQuestion is the flag to know if we should create new question or update previous
                        if (NewQuestion)
                        {
                           Database.StoreQuestion(Question);
                        }
                        //to update insated of creating new question
                        //if flag = false -> only change text and number of smileys -> update not insert
                        else
                        {
                            Database.SetText(Id, Question.Text);
                            Database.SetNumberOfSmileys(Id, Question.NumberOfSmileys);
                            MessageBox.Show("Question Updated Successfully.");
                        }
                        
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Invalid Number of Smileys (2-5).", "Error", E.Message);
                    }
                }
                catch (Exception E)
                {
                    Logger.WriteLog("Question Type should be specified.", "Error", E.Message);
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Question Text should be specified.", "Error", E.Message);
                
            }
        }

        public static void StarsInputValidation(clsQuestionStar Question, int Id = 0, bool NewQuestion = true)
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
                        //NewSmileyQuestion is the flag to know if we should create new question or update previous
                        if (NewQuestion)
                        {
                            Database.StoreQuestion(Question);
                        }
                        //if flag = false -> only change text and number of stars -> update not insert
                        else
                        {
                            Database.SetText(Id, Question.Text);
                            Database.SetNumberOfStars(Id, Question.NumberOfStars);
                            MessageBox.Show("Question Updated Successfully.");
                        }
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Invalid Number of Stars (1-10).", "Error", E.Message);
                    }
                }
                catch (Exception E)
                {
                    Logger.WriteLog("Question Type should be specified.", "Error", E.Message);
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Question Text should be specified.", "Error", E.Message);
            }
        }

        public static void SliderInputValidation(clsQuestionSlider Question, int Id = 0, bool NewQuestion = true)
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
                        if (Question.StartValue < 0 || Question.StartValue > 100)
                        {
                            throw new ArgumentOutOfRangeException(nameof(Question.StartValue));
                        }

                        try
                        {
                            if (Question.EndValue > 100 || Question.EndValue <= Question.StartValue)
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
                                    //NewSmileyQuestion is the flag to know if we should create new question or update previous
                                    if (NewQuestion)
                                    {
                                        Database.StoreQuestion(Question);
                                    }
                                    else
                                    {
                                        Database.SetText(Id, Question.Text);
                                        Database.SetStartValue(Id, Question.StartValue);    
                                        Database.SetStartCaption(Id,Question.StartCaption);
                                        Database.SetEndValue(Id, Question.EndValue);    
                                        Database.SetEndCaption(Id,Question.EndCaption);
                                        MessageBox.Show("Question Updated Successfully.");
                                    }
                                }
                                catch (Exception E)
                                {
                                    Logger.WriteLog("End Caption Too Long (> 50 characters) ", "Error", E.Message);
                                }
                            }
                            catch (Exception E)
                            {
                                Logger.WriteLog("Start Caption Too Long (> 50 characters) ", "Error", E.Message);
                            }
                        }
                        catch (Exception E)
                        {
                            Logger.WriteLog("Invalid End Value - Less than 100, Greater than start.", "Error", E.Message);
                        }
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Invalid Start Value - Positive number less than 100.", "Error", E.Message);
                    }
                }
                catch (Exception E)
                {
                    Logger.WriteLog("Question Type should be specified.", "Error", E.Message);
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Question Text should be specified.", "Error", E.Message);
            }
        }

        public static void DeleteQuestion(int Id)
        {
            Database.DeleteQuestion(Id);
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
       
        public static void StoreQuestion(clsQuestionSmiley q)
        {
            Database.StoreQuestion(q);
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
