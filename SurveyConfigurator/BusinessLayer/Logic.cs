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
    public static class Logic
    {

        public static bool CanConnect()
        {
            return DatabaseAccess.CanConnect();

        }

        public static DataTable ListQuestions()
        {
            return DatabaseAccess.ListQuestions();
        }

        //if NewSmileyQuestion is set to true = database excutes insert command
        //if false -> update command
        public static int SmileyInputValidation(clsQuestionSmiley Question, int Id = 0)
        {
            try
            {
                //check that question type is selected
                if (Question.Type is null)
                {
                    return clsConstants.TYPE_NOT_SELECTED;
                }

                //check that question text is selected
                if (Question.Text == "")
                {
                    return clsConstants.TEXT_NOT_SPECIFIED; ;
                }

                //check that number of smileys is between 2 and 5
                //this isnt possible to throw and exception but its there for future changes 
                if (Question.NumberOfSmileys > 5 || Question.NumberOfSmileys < 2)
                {
                    return clsConstants.INVALID_NUMBER_OF_SMILEYS;
                }

                //layer 3 object to save in sql server
                //NewSmileyQuestion is the flag to know if we should create new question or update previous
                if (Id == -1)
                {
                    return DatabaseAccess.NewQuestion(Question);

                }
                //to update instead of creating new question
                //if flag = false -> only change text and number of smileys -> update not insert
                else
                {
                    int Result = DatabaseAccess.EditText(Id, Question.Text);
                    if (Result == 1)
                        return DatabaseAccess.EditQuestionSmiley(Id, Question.NumberOfSmileys);
                    else
                        return Result;
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
                if (Id == -1) return clsConstants.FAILED_EDIT_QUESTION;
                else return clsConstants.FAILED_NEW_QUESTION;
            }





        }

        public static int StarsInputValidation(clsQuestionStar Question, int Id = 0)
        {
            try
            {
                //check that question type is selected
                if (Question.Type is null)
                {
                    return clsConstants.TYPE_NOT_SELECTED;
                }

                //check that question text is selected
                if (Question.Text == "")
                {
                    return clsConstants.TEXT_NOT_SPECIFIED; ;
                }

                //check that number of stars is between 1 and 10
                //this isnt possible to throw an error but its there for future changes

                if (Question.NumberOfStars > 10 || Question.NumberOfStars < 1)
                {
                    return clsConstants.INVALID_NUMBER_OF_STARS;
                }
                //layer 3 object to save in sql server
                //Id is the flag to know if we should create new question or update previous
                if (Id == -1)
                {
                    return DatabaseAccess.NewQuestion(Question);
                }
                //if id!=-1 -> only change text and number of stars -> update not insert
                else
                {
                    int Result = DatabaseAccess.EditText(Id, Question.Text);
                    if (Result == 1)
                        return DatabaseAccess.EditQuestionStars(Id, Question.NumberOfStars);
                    else
                        return Result;
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
                if (Id == -1) return clsConstants.FAILED_EDIT_QUESTION;
                else return clsConstants.FAILED_NEW_QUESTION;
            }


        }



        public static int SliderInputValidation(clsQuestionSlider Question, int Id = 0)
        {
            try
            {
                //check that question type is selected
                if (Question.Type is null)
                {
                    return clsConstants.TYPE_NOT_SELECTED;
                }

                //check that question text is selected
                if (Question.Text == "")
                {
                    return clsConstants.TEXT_NOT_SPECIFIED; ;
                }

                //check that start value is > 0
                //this isnt possible to throw an error but its there for future changes
                if (Question.StartValue < 0)
                {
                    return clsConstants.INVALID_START_VALUE;
                }
                //check that end value is < 100
                if (Question.EndValue > 100)
                {
                    return clsConstants.INVALID_END_VALUE;
                }
                //check that start value is less than end value
                if (Question.StartValue > Question.EndValue)
                {
                    return clsConstants.INVALID_END_LESS_THAN_START_VALUE;
                }

                if (Question.StartCaption.Length > 50)
                {
                    return clsConstants.INVALID_START_CAPTION;
                }


                if (Question.EndCaption.Length > 50)
                {
                    return clsConstants.INVALID_END_CAPTION;
                }

                //layer 3 object to save in sql server
                //NewSmileyQuestion is the flag to know if we should create new question or update previous
                if (Id == -1)
                {
                    return DatabaseAccess.NewQuestion(Question);
                }
                else
                {
                    int Result = DatabaseAccess.EditText(Id, Question.Text);
                    if (Result == 1)
                        return DatabaseAccess.EditQuestionSlider(Id, Question);
                    else
                        return Result;
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
                if (Id == -1) return clsConstants.FAILED_EDIT_QUESTION;
                else return clsConstants.FAILED_NEW_QUESTION;
            }


        }

        public static int DeleteQuestion(int Id)
        {
            return DatabaseAccess.DeleteQuestion(Id);
        }
        public static string GetType(int Id)
        {
            return DatabaseAccess.GetType(Id);
        }
        public static string GetText(int Id)
        {
            return DatabaseAccess.GetText(Id);
        }
        public static int Text(int Id, string Text)
        {
            return DatabaseAccess.EditText(Id, Text);
        }

        public static int NewQuestion(clsQuestionSmiley Question)
        {
            return DatabaseAccess.NewQuestion(Question);
        }

        public static int GetNumberOfSmileys(int Id)
        {
            return DatabaseAccess.GetNumberOfSmileys(Id);
        }
        public static int EditQuestionSmiley(int Id, int NumberOfSmileys)
        {
            return DatabaseAccess.EditQuestionSmiley(Id, NumberOfSmileys);
        }
        public static int GetNumberOfStars(int Id)
        {
            return DatabaseAccess.GetNumberOfStars(Id);
        }
        public static int EditQuestionStars(int Id, int NumberOfStars)
        {
            return DatabaseAccess.EditQuestionStars(Id, NumberOfStars);
        }
        public static int GetStartValue(int Id)
        {
            return DatabaseAccess.GetStartValue(Id);
        }
        public static int GetEndValue(int Id)
        {
            return DatabaseAccess.GetEndValue(Id);
        }
        public static string GetStartCaption(int Id)
        {
            return DatabaseAccess.GetStartCaption(Id);
        }
        public static string GetEndCaption(int Id)
        {
            return DatabaseAccess.GetEndCaption(Id);
        }

        public static void SetConnectionString(string Server, string Database, string Username, string Password, bool IntegratedSecurity)
        {
            DatabaseAccess.SetConnectionString(Server, Database, Username, Password, IntegratedSecurity);
       }
        public static List<clsMergedQuestions> ViewQuestions()
        {
            return DatabaseAccess.ViewQuestions();
        }


    }
}
