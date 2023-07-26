using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ErrorLogger;
using System.Data;
using Utilities;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Configuration;
using System.Text;
using System.Xml;

namespace DatabaseLayer
{
    public class DatabaseAccess
    {
        public string Server { set; get; }
        public string Database { set; get; }
        public string IntegratedSecurity;
        public string Username;
        public string Password;
        public static string CONNECTION = ConfigurationManager.ConnectionStrings["CONNECTION"].ToString();

        //this is to stop the connection loop on GetLastUpdate before the connection form is submitted 
        private bool DatabaseChangeFlag = false;



        public void DataUpdatedInDataLayer()
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {

                        SqlCommand UpdateTime = new SqlCommand(clsConstants.P_LAST_UPDATE, Connection);
                        UpdateTime.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        UpdateTime.Parameters.Add(new SqlParameter("@" + clsConstants.UPDATE_TIME, SqlDbType.VarChar, 30));
                        UpdateTime.Parameters["@" + clsConstants.UPDATE_TIME].Value = DateTime.Now.ToString("M/d/yyyy HH:mm:ss");
                        // Update the LastUpdate to indicate that data is updated
                        UpdateTime.ExecuteNonQuery();


                    }
                    catch (Exception E)
                    {
                        //Logger.WriteLog(E.Message, clsConstants.ERROR);
                    }
                }
            }
            catch (Exception E)
            {
                //Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message); 
            }
        }



        public string GetLastUpdate()
        {
            try
            {
                if (DatabaseChangeFlag)
                {
                    using (SqlConnection Connection = new SqlConnection(CONNECTION))
                    {
                        Connection.Open();
                        try
                        {

                            SqlCommand GetUpdateTime = new SqlCommand(clsConstants.P_GET_LAST_UPDATE, Connection);
                            GetUpdateTime.CommandType = CommandType.StoredProcedure;


                            // Add the output parameter.
                            GetUpdateTime.Parameters.Add(new SqlParameter("@" + clsConstants.UPDATE_TIME, SqlDbType.DateTime));
                            GetUpdateTime.Parameters["@" + clsConstants.UPDATE_TIME].Direction = ParameterDirection.Output;


                            //run previously stored procedure
                            GetUpdateTime.ExecuteNonQuery();
                            // MessageBox.Show( GetUpdateTime.Parameters["@" + clsConstants.UPDATE_TIME].Value.ToString());

                            return GetUpdateTime.Parameters["@" + clsConstants.UPDATE_TIME].Value.ToString();


                        }
                        catch (Exception E)
                        {
                            // Logger.WriteLog(E.Message, clsConstants.ERROR);
                            return "";
                        }
                    }
                }
                else return "";
            }
            catch (Exception E)
            {
                // Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return "";
            }
        }


        public void SetConnectionString(string tServer, string tDatabase, string tUsername, string tPassword, bool tIntegratedSecurity)
        {
            try
            {
                
                Server = tServer;
                Database = tDatabase;
                string Connection;
                if (tIntegratedSecurity)
                {
                    IntegratedSecurity = "True";
                    //Constructing connection string from the inputs
                    Connection = string.Format(clsConstants.SET_CONNECTION_WINDOWS_AUTH, Server, Database, IntegratedSecurity);
                }
                else
                {
                    IntegratedSecurity = "False";
                    Username = tUsername;
                    Password = tPassword;
                    Connection = string.Format(clsConstants.SET_CONNECTION_SQL_AUTH, Server, Database, Username, Password);
                }

                //updating APP.config file
                XmlDocument XmlDoc = new XmlDocument();
                //to refresh connection string each time else it will use  previous connection string
                //Loading the Config file
                XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                foreach (XmlElement xElement in XmlDoc.DocumentElement)
                {
                    if (xElement.Name == "connectionStrings")
                    {
                        //setting the coonection string
                        xElement.FirstChild.Attributes[2].Value = Connection;
                    }
                }
                //writing the connection string in config file
                XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                ConfigurationManager.RefreshSection("connectionStrings");
                CONNECTION = ConfigurationManager.ConnectionStrings["CONNECTION"].ToString();

            }
            catch (Exception E)
            {
                Logger.WriteLog(E.Message, clsConstants.ERROR);
            }


        } 
        public bool CanConnect(string TestConnectionString="")
        {
            try
            {

                SqlConnection Connection;
                if (TestConnectionString == "")
                    Connection = new SqlConnection(CONNECTION);
                else
                     Connection = new SqlConnection(TestConnectionString);
                Connection.Open();
                Connection.Close();
                return true;
            }
            catch (Exception E)
            { 
                Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return false;
            }
            
            

        }

        //3 overriden functions to create new question depending on the question type
        public int NewQuestion(clsQuestionSmiley Question)
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

        public int NewQuestion(clsQuestionStar Question)
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

        public int NewQuestion(clsQuestionSlider Question)
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

        public int EditQuestion(clsQuestionSmiley Question)
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

        public int EditQuestion(clsQuestionStar Question)
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

        public int EditQuestion(clsQuestionSlider Question)
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

        public int DeleteQuestion(int Id)
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

        public int GetSmileyQuestion(clsQuestionSmiley Question)
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

        public int GetStarQuestion(clsQuestionStar Question)
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

        public int GetSliderQuestion(clsQuestionSlider Question)
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


        public List<clsMergedQuestions> ViewQuestions()
        {
            List<clsMergedQuestions> Questions = new List<clsMergedQuestions>();
            try
            {

                DatabaseChangeFlag = true;
                DataUpdatedInDataLayer();
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    try
                    {

                        using (SqlCommand ViewQuestions = new SqlCommand(clsConstants.P_VIEW, Connection))
                        {
                            Connection.Open();
                            using (SqlDataReader Reader = ViewQuestions.ExecuteReader())
                            {
                                while (Reader.Read())
                                {
                                    clsMergedQuestions Question = new clsMergedQuestions
                                    {
                                        Type = Reader[clsConstants.TYPE].ToString(),
                                        Id = (int)Reader[clsConstants.ID],
                                        Text = Reader[clsConstants.TEXT].ToString(),
                                        Order = (int)Reader[clsConstants.ORDER],
                                        Properties = Reader[clsConstants.PROPERTIES].ToString()
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
                Logger.WriteLog(E.Message, clsConstants.ERROR);
                return Questions;
            }
        }



    }
}



