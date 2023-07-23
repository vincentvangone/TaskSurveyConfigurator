﻿using DatabaseLayer;
using ErrorLogger;
using Microsoft.TeamFoundation.Common.Internal;
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
using System.Windows.Forms;
using System.Xml.Serialization;
using Utilities;

namespace BusinessLayer
{
    public static class Logic
    {

        private static string LastUpdateTime;
        

        public static int CheckForUpdates()
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

        
        

        public static bool CanConnect()
        {
            return DatabaseAccess.CanConnect();

        }

        
        //if id is set to -1 = database excutes insert command
        //else -> update command
        public static int SmileyInputValidation(clsQuestionSmiley Question, int Id = -1)
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


                    if (Result == clsConstants.SUCCESS)
                    {
                        return DatabaseAccess.EditQuestionSmiley(Id, Question.NumberOfSmileys);
                        
                        
                    }

                    else return Result;
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
                if (Id == -1) return clsConstants.FAILED_EDIT_QUESTION;
                else return clsConstants.FAILED_NEW_QUESTION;
            }





        }

        public static int StarsInputValidation(clsQuestionStar Question, int Id = -1)
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
                //id is the flag to know if we should create new question or update previous
                if (Id == -1)
                {
                    return DatabaseAccess.NewQuestion(Question);
                    
                   
                }


                //to update instead of creating new question
                //if id!=-1 -> only change text and number of smileys -> update not insert
                else
                {
                    int Result = DatabaseAccess.EditText(Id, Question.Text);


                    if (Result == clsConstants.SUCCESS)
                    {
                        return DatabaseAccess.EditQuestionStars(Id, Question.NumberOfStars);
                        
                    }
                    else  return Result;
                   
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
                if (Id == -1) return clsConstants.FAILED_EDIT_QUESTION;
                else return clsConstants.FAILED_NEW_QUESTION;
            }


        }



        public static int SliderInputValidation(clsQuestionSlider Question, int Id = -1)
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
                //id is the flag to know if we should create new question or update previous
                if (Id == -1)
                {
                    return DatabaseAccess.NewQuestion(Question);
                    
                }


                //to update instead of creating new question
                //if id!=-1 -> only change text and number of smileys -> update not insert
                else
                {
                    int Result = DatabaseAccess.EditText(Id, Question.Text);


                    if (Result == clsConstants.SUCCESS)
                    {
                        return DatabaseAccess.EditQuestionSlider(Id, Question);
                    }
                    else return Result;

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

        public static int GetNumberOfSmileys(int Id)
        {
            return DatabaseAccess.GetNumberOfSmileys(Id);
        }
        public static int GetNumberOfStars(int Id)
        {
            return DatabaseAccess.GetNumberOfStars(Id);
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
            LastUpdateTime = DateTime.Now.ToString();
            return DatabaseAccess.ViewQuestions();
        }

    }
}

