using DatabaseLayer;
using ErrorLogger;
using Microsoft.TeamFoundation.Common.Internal;
using Microsoft.VisualStudio.Services.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.Windows.Forms;
using System.Xml.Serialization;
using Utilities;

namespace BusinessLayer
{
    public class Logic
    {

        private string LastUpdateTime;


        public int CheckForUpdates()
        {

            try
            {
                // Check for updates in the shared storage (file)
                if (File.Exists(DatabaseAccess.LastUpdate))
                {
                    string LastDatabaseUpdate = File.ReadAllText(DatabaseAccess.LastUpdate);
                    if (DateTime.Compare(DateTime.Parse(LastUpdateTime), DateTime.Parse(LastDatabaseUpdate)) < 0)
                    {

                        // return 1 to Refresh the DataGridView
                        return 1;


                    }

                }

                return 0;

            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
                return -1;
            }


        }

        public bool CanConnect()
        {
            return DatabaseAccess.CanConnect();

        }



        //if id is set to -1 = database excutes insert command
        //else -> update command
        public int SmileyInputValidation(clsQuestionSmiley Question, int Id = -1)
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

                //check that 100>=order>0
                if (Question.Order <= 0 || Question.Order > 100)
                {
                    return clsConstants.INVALID_QUESTION_ORDER;
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
                    return DatabaseAccess.EditQuestion(Question);

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
                if (Id == -1) return clsConstants.FAILED_EDIT_QUESTION;
                else return clsConstants.FAILED_NEW_QUESTION;
            }





        }

        public int StarsInputValidation(clsQuestionStar Question, int Id = -1)
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

                //check that 100>=order>0
                if (Question.Order <= 0 || Question.Order > 100)
                {
                    return clsConstants.INVALID_QUESTION_ORDER;
                }

                //layer 3 object to save in sql server
                //id is the flag to know if we should create new question or update previous
                if (Id == -1)
                {
                    return DatabaseAccess.NewQuestion(Question);


                }


                //to update instead of creating new question
                //if id!=-1 -> only change text and number of smileys -> update not insert
                else
                {
                    return DatabaseAccess.EditQuestion(Question);

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
                if (Id == -1) return clsConstants.FAILED_EDIT_QUESTION;
                else return clsConstants.FAILED_NEW_QUESTION;
            }


        }



        public int SliderInputValidation(clsQuestionSlider Question, int Id = -1)
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

                //check that 100>=order>0
                if (Question.Order <= 0 || Question.Order > 100)
                {
                    return clsConstants.INVALID_QUESTION_ORDER;
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
                //id is the flag to know if we should create new question or update previous
                if (Id == -1)
                {
                    return DatabaseAccess.NewQuestion(Question);

                }


                //to update instead of creating new question
                //if id!=-1 -> only change text and number of smileys -> update not insert
                else
                {

                    return DatabaseAccess.EditQuestion(Question);


                }



            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
                if (Id == -1) return clsConstants.FAILED_EDIT_QUESTION;
                else return clsConstants.FAILED_NEW_QUESTION;
            }


        }

        public int GetSmileyQuestion(clsQuestionSmiley Question)
        {
            return DatabaseAccess.GetSmileyQuestion(Question);
        }

        public int GetStarsQuestion(clsQuestionStar Question)
        {
            return DatabaseAccess.GetStarsQuestion(Question);
        }


        public int GetSliderQuestion(clsQuestionSlider Question)
        {
            return DatabaseAccess.GetSliderQuestion(Question);
        }


        public int DeleteQuestion(int Id)
        {
            return DatabaseAccess.DeleteQuestion(Id);

        }

        public void SetConnectionString(string Server, string Database, string Username, string Password, bool IntegratedSecurity)
        {
            DatabaseAccess.SetConnectionString(Server, Database, Username, Password, IntegratedSecurity);
        }
        public List<clsMergedQuestions> ViewQuestions()
        {
            LastUpdateTime = DateTime.Now.ToString();
            return DatabaseAccess.ViewQuestions();
        }

    }
}

