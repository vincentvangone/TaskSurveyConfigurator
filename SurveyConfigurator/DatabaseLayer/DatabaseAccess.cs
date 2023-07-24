﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ErrorLogger;
using System.Data;
using Utilities;
//using System.Windows.Forms;
using System.IO;

namespace DatabaseLayer
{
    public class DatabaseAccess
    {
        public static string Server { set; get; }
        public static string Database { set; get; }
        public static string IntegratedSecurity;
        public static string Username;
        public static string Password;
        public static string CONNECTION;


        // File path to the shared data file to note updates
        public const string LastUpdate = ".\\LastUpdate.txt";
        public static void DataUpdatedInDataLayer()
        {
            try
            {
                // Update the shared storage (file) to indicate that data is updated
                File.WriteAllText(LastUpdate, DateTime.Now.ToString());
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }
        }

        public static void SetConnectionString(string tServer, string tDatabase, string tUsername, string tPassword, bool tIntegratedSecurity)
        {
            try
            {
                Server = tServer;
                Database = tDatabase;
                if (tIntegratedSecurity)
                {
                    IntegratedSecurity = "True";
                    CONNECTION = String.Format("Data Source={0};Initial Catalog ={1}; Integrated Security = {2}", Server, Database, IntegratedSecurity);
                }
                else
                {
                    IntegratedSecurity = "False";
                    Username = tUsername;
                    Password = tPassword;
                    CONNECTION = String.Format("Data Source={0};Initial Catalog ={1};User Id={3}; Password={4}", Server, Database, Username, Password);
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }


        }

        public static bool CanConnect()
        {
            try
            {
                SqlConnection Connection = new SqlConnection(CONNECTION);
                Connection.Open();
                return true;
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return false;
            }
        }

        //3 overriden functions to create new question depending on the question type
        public static int NewQuestion(clsQuestionSmiley Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {

                        SqlCommand NewQuestion = new SqlCommand(clsConstants.P_INSERT, Connection);
                        NewQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TEXT, SqlDbType.VarChar, 1000));
                        NewQuestion.Parameters["@" + clsConstants.TEXT].Value = Question.Text;
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TYPE, SqlDbType.VarChar, 20));
                        NewQuestion.Parameters["@" + clsConstants.TYPE].Value = Question.Type;
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ORDER, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.ORDER].Value = Question.Order;
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.NUMBER_OF_SMILEYS, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.NUMBER_OF_SMILEYS].Value = Question.NumberOfSmileys;

                        // Add the output parameter.
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.ID].Direction = ParameterDirection.Output;

                        //run previously stored procedure
                        if (NewQuestion.ExecuteNonQuery() > 0)
                        {
                            Logger.WriteLog(clsConstants.SUCCESS_STRING, clsConstants.INFORMATION, clsConstants.ID + NewQuestion.Parameters["@" + clsConstants.ID].Value);
                            DataUpdatedInDataLayer();
                            return clsConstants.SUCCESS; //success code
                        }
                        else
                        {
                            return clsConstants.FAILED_NEW_QUESTION;
                        }
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_NEW_QUESTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_NEW_QUESTION; //failed insertion
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_DATABASE_CONNECTION; //failed to connect to database
            }
        }

        public static int NewQuestion(clsQuestionStar Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {

                        SqlCommand NewQuestion = new SqlCommand(clsConstants.P_INSERT, Connection);
                        NewQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TEXT, SqlDbType.VarChar, 1000));
                        NewQuestion.Parameters["@" + clsConstants.TEXT].Value = Question.Text;
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TYPE, SqlDbType.VarChar, 20));
                        NewQuestion.Parameters["@" + clsConstants.TYPE].Value = Question.Type;
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ORDER, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.ORDER].Value = Question.Order;
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.NUMBER_OF_STARS, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.NUMBER_OF_STARS].Value = Question.NumberOfStars;

                        // Add the output parameter.
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.ID].Direction = ParameterDirection.Output;

                        //run previously stored procedure
                        if (NewQuestion.ExecuteNonQuery() > 0)
                        {
                            Logger.WriteLog(clsConstants.SUCCESS_STRING, clsConstants.INFORMATION, clsConstants.ID + NewQuestion.Parameters["@" + clsConstants.ID].Value);
                            DataUpdatedInDataLayer();
                            return clsConstants.SUCCESS; //success code
                        }
                        else
                        {
                            return clsConstants.FAILED_NEW_QUESTION;
                        }
                    }

                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_NEW_QUESTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_NEW_QUESTION; //failed insertion
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_DATABASE_CONNECTION; //failed to connect to database
            }
        }

        public static int NewQuestion(clsQuestionSlider Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {

                        SqlCommand NewQuestion = new SqlCommand(clsConstants.P_INSERT, Connection);
                        NewQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TEXT, SqlDbType.VarChar, 1000));
                        NewQuestion.Parameters["@" + clsConstants.TEXT].Value = Question.Text;

                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TYPE, SqlDbType.VarChar, 20));
                        NewQuestion.Parameters["@" + clsConstants.TYPE].Value = Question.Type;

                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ORDER, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.ORDER].Value = Question.Order;

                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.START_VALUE, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.START_VALUE].Value = Question.StartValue;

                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.END_VALUE, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.END_VALUE].Value = Question.EndValue;

                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.START_CAPTION, SqlDbType.VarChar, 50));
                        NewQuestion.Parameters["@" + clsConstants.START_CAPTION].Value = Question.StartCaption;

                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.END_CAPTION, SqlDbType.VarChar, 50));
                        NewQuestion.Parameters["@" + clsConstants.END_CAPTION].Value = Question.EndCaption;

                        // Add the output parameter.
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.ID].Direction = ParameterDirection.Output;

                        //run previously stored procedure
                        if (NewQuestion.ExecuteNonQuery() > 0)
                        {
                            Logger.WriteLog(clsConstants.SUCCESS_STRING, clsConstants.INFORMATION, clsConstants.ID + NewQuestion.Parameters["@" + clsConstants.ID].Value);
                            DataUpdatedInDataLayer();
                            return clsConstants.SUCCESS; //success code
                        }
                        else
                        {
                            return clsConstants.FAILED_NEW_QUESTION;
                        }
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_NEW_QUESTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_NEW_QUESTION; //failed insertion
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_DATABASE_CONNECTION; //failed to connect to database
            }
        }



        //3 overriden functions to edit previously created questions depending on the question type

        public static int EditQuestion(clsQuestionSmiley Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {

                        SqlCommand EditQuestion = new SqlCommand(clsConstants.P_EDIT, Connection);
                        EditQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        EditQuestion.Parameters["@" + clsConstants.ID].Value = Question.Id;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TEXT, SqlDbType.VarChar, 1000));
                        EditQuestion.Parameters["@" + clsConstants.TEXT].Value = Question.Text;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TYPE, SqlDbType.VarChar, 20));
                        EditQuestion.Parameters["@" + clsConstants.TYPE].Value = Question.Type;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ORDER, SqlDbType.Int));
                        EditQuestion.Parameters["@" + clsConstants.ORDER].Value = Question.Order;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.NUMBER_OF_SMILEYS, SqlDbType.Int));
                        EditQuestion.Parameters["@" + clsConstants.NUMBER_OF_SMILEYS].Value = Question.NumberOfSmileys;

                        //run previously stored procedure
                        if (EditQuestion.ExecuteNonQuery() > 0)
                        {
                            Logger.WriteLog(clsConstants.SUCCESS_STRING, clsConstants.INFORMATION, clsConstants.ID + Question.Id);
                            DataUpdatedInDataLayer();
                            return clsConstants.SUCCESS; //success code
                        }
                        else
                        {

                            return clsConstants.FAILED_EDIT_QUESTION;
                        }
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_EDIT_QUESTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_EDIT_QUESTION; //failed insertion
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_DATABASE_CONNECTION; //failed to connect to database
            }
        }

        public static int EditQuestion(clsQuestionStar Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {

                        SqlCommand EditQuestion = new SqlCommand(clsConstants.P_EDIT, Connection);
                        EditQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        EditQuestion.Parameters["@" + clsConstants.ID].Value = Question.Id;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TEXT, SqlDbType.VarChar, 1000));
                        EditQuestion.Parameters["@" + clsConstants.TEXT].Value = Question.Text;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TYPE, SqlDbType.VarChar, 20));
                        EditQuestion.Parameters["@" + clsConstants.TYPE].Value = Question.Type;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ORDER, SqlDbType.Int));
                        EditQuestion.Parameters["@" + clsConstants.ORDER].Value = Question.Order;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.NUMBER_OF_STARS, SqlDbType.Int));
                        EditQuestion.Parameters["@" + clsConstants.NUMBER_OF_STARS].Value = Question.NumberOfStars;

                        //run previously stored procedure
                        if (EditQuestion.ExecuteNonQuery() > 0)
                        {
                            Logger.WriteLog(clsConstants.SUCCESS_STRING, clsConstants.INFORMATION, clsConstants.ID + Question.Id);
                            DataUpdatedInDataLayer();
                            return clsConstants.SUCCESS; //success code
                        }
                        else
                        {

                            return clsConstants.FAILED_EDIT_QUESTION;
                        }
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_EDIT_QUESTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_EDIT_QUESTION; //failed insertion
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_DATABASE_CONNECTION; //failed to connect to database
            }
        }

        public static int EditQuestion(clsQuestionSlider Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {

                        SqlCommand EditQuestion = new SqlCommand(clsConstants.P_EDIT, Connection);
                        EditQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        EditQuestion.Parameters["@" + clsConstants.ID].Value = Question.Id;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TEXT, SqlDbType.VarChar, 1000));
                        EditQuestion.Parameters["@" + clsConstants.TEXT].Value = Question.Text;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TYPE, SqlDbType.VarChar, 20));
                        EditQuestion.Parameters["@" + clsConstants.TYPE].Value = Question.Type;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ORDER, SqlDbType.Int));
                        EditQuestion.Parameters["@" + clsConstants.ORDER].Value = Question.Order;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.START_VALUE, SqlDbType.Int));
                        EditQuestion.Parameters["@" + clsConstants.START_VALUE].Value = Question.StartValue;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.END_VALUE, SqlDbType.Int));
                        EditQuestion.Parameters["@" + clsConstants.END_VALUE].Value = Question.EndValue;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.START_CAPTION, SqlDbType.VarChar, 50));
                        EditQuestion.Parameters["@" + clsConstants.START_CAPTION].Value = Question.StartCaption;

                        EditQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.END_CAPTION, SqlDbType.VarChar, 50));
                        EditQuestion.Parameters["@" + clsConstants.END_CAPTION].Value = Question.EndCaption;

                       
                        //run previously stored procedure
                        if (EditQuestion.ExecuteNonQuery() > 0)
                        {
                            Logger.WriteLog(clsConstants.SUCCESS_STRING, clsConstants.INFORMATION, clsConstants.ID + Question.Id);
                            DataUpdatedInDataLayer();
                            return clsConstants.SUCCESS; //success code
                        }
                        else
                        {
                            
                            return clsConstants.FAILED_EDIT_QUESTION;
                        }
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_EDIT_QUESTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_EDIT_QUESTION; //failed insertion
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_DATABASE_CONNECTION; //failed to connect to database
            }
        }

        public static int DeleteQuestion(int Id)
        {

            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {
                        SqlCommand Delete = new SqlCommand(clsConstants.P_DELETE, Connection);
                        Delete.CommandType = CommandType.StoredProcedure;

                        Delete.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        Delete.Parameters["@" + clsConstants.ID].Value = Id;

                        //run previously stored delete procedure
                        if (Delete.ExecuteNonQuery() > 0)
                        {
                            DataUpdatedInDataLayer();
                            return clsConstants.SUCCESS;
                        }
                        else
                            return clsConstants.FAILED_DELETE_QUESTION;
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_DELETE_QUESTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_DELETE_QUESTION; //failed deletion
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_DATABASE_CONNECTION; //failed to connect to database
            }


        }

        public static int GetSmileyQuestion(clsQuestionSmiley Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    try
                    {
                        Connection.Open();
                        SqlCommand GetQuestion = new SqlCommand(clsConstants.P_GET_SMILEY_QUESTION, Connection);
                        GetQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        GetQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        GetQuestion.Parameters["@" + clsConstants.ID].Value = Question.Id;

                        using (SqlDataReader Reader = GetQuestion.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                Question.Type = Reader[clsConstants.TYPE].ToString();
                                Question.Id = (int)Reader[clsConstants.ID];
                                Question.Text = Reader[clsConstants.TEXT].ToString();
                                Question.Order = (int)Reader[clsConstants.ORDER];
                                Question.NumberOfSmileys = (int)Reader[clsConstants.NUMBER_OF_SMILEYS];


                            }
                        }
                        return clsConstants.SUCCESS;
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_DATABASE_CONNECTION;
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_EDIT_QUESTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_EDIT_QUESTION;
            }

        }

        public static int GetStarsQuestion(clsQuestionStar Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    try
                    {
                        Connection.Open();
                        SqlCommand GetQuestion = new SqlCommand(clsConstants.P_GET_STAR_QUESTION, Connection);
                        GetQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        GetQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        GetQuestion.Parameters["@" + clsConstants.ID].Value = Question.Id;

                        using (SqlDataReader Reader = GetQuestion.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                Question.Type = Reader[clsConstants.TYPE].ToString();
                                Question.Id = (int)Reader[clsConstants.ID];
                                Question.Text = Reader[clsConstants.TEXT].ToString();
                                Question.Order = (int)Reader[clsConstants.ORDER];
                                Question.NumberOfStars = (int)Reader[clsConstants.NUMBER_OF_STARS];


                            }
                        }
                        return clsConstants.SUCCESS;
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_DATABASE_CONNECTION;
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_EDIT_QUESTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_EDIT_QUESTION;
            }

        }

        public static int GetSliderQuestion(clsQuestionSlider Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    try
                    {
                        Connection.Open();
                        SqlCommand GetQuestion = new SqlCommand(clsConstants.P_GET_SLIDER_QUESTION, Connection);
                        GetQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        GetQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        GetQuestion.Parameters["@" + clsConstants.ID].Value = Question.Id;

                        using (SqlDataReader Reader = GetQuestion.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                Question.Type = Reader[clsConstants.TYPE].ToString();
                                Question.Id = (int)Reader[clsConstants.ID];
                                Question.Text = Reader[clsConstants.TEXT].ToString();
                                Question.Order = (int)Reader[clsConstants.ORDER];
                                Question.StartValue = (int)Reader[clsConstants.START_VALUE];
                                Question.EndValue = (int)Reader[clsConstants.END_VALUE];
                                Question.StartCaption = Reader[clsConstants.START_CAPTION].ToString();
                                Question.EndCaption = Reader[clsConstants.END_CAPTION].ToString();



                            }
                        }
                        return clsConstants.SUCCESS;
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_DATABASE_CONNECTION;
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_EDIT_QUESTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_EDIT_QUESTION;
            }

        }

        
        public static List<clsMergedQuestions> ViewQuestions()
        {
            List<clsMergedQuestions> Questions = new List<clsMergedQuestions>();
            try
            {

                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    try
                    {
                        
                        using (SqlCommand ViewQuestions = new SqlCommand(clsConstants.P_VIEW, Connection))
                        {
                            Connection.Open();
                            using (SqlDataReader Reader = ViewQuestions.ExecuteReader())
                            {
                                while(Reader.Read())
                                {
                                    clsMergedQuestions Question = new clsMergedQuestions
                                    {
                                        Type = Reader[clsConstants.TYPE].ToString(),
                                        Id = (int)Reader[clsConstants.ID],
                                        Text = Reader[clsConstants.TEXT].ToString(),
                                        Order = (int)Reader[clsConstants.ORDER],
                                        Properties =  Reader[clsConstants.PROPERTIES].ToString()
                                    };
                                    Questions.Add(Question);

                                }
                            }
                        }
                        Connection.Close();
                        return Questions;
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(E.Message, clsConstants.ERROR);
                        return Questions;
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message,clsConstants.ERROR);
                return Questions;
            }
        }



    }
}



