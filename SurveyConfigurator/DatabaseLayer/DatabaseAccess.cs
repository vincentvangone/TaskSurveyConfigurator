using System;
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

       
        //setters and getters for common inputs - text + type
        public static string GetText(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand GetText = new SqlCommand(clsConstants.P_GET_TEXT, Connection);
                    GetText.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetText.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                    GetText.Parameters["@" + clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetText.Parameters.Add(new SqlParameter("@" + clsConstants.TEXT, SqlDbType.VarChar, 1000));
                    GetText.Parameters["@" + clsConstants.TEXT].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetText.ExecuteNonQuery();

                    return GetText.Parameters["@" + clsConstants.TEXT].Value.ToString();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.", clsConstants.ERROR, E.Message);
                return null;
            }

        }
        public static int EditText(int Id, string Text)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {
                        SqlCommand EditText = new SqlCommand(clsConstants.P_EDIT_TEXT, Connection);
                        EditText.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        EditText.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        EditText.Parameters["@" + clsConstants.ID].Value = Id;
                        EditText.Parameters.Add(new SqlParameter("@" + clsConstants.TEXT, SqlDbType.VarChar, 1000));
                        EditText.Parameters["@" + clsConstants.TEXT].Value = Text;

                        //run previously stored procedure
                        if (EditText.ExecuteNonQuery() >= 1)
                        {
                            DataUpdatedInDataLayer();
                            return clsConstants.SUCCESS;
                        }
                        else
                            return clsConstants.FAILED_EDIT_QUESTION;
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_EDIT_QUESTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_EDIT_QUESTION; //failed Update
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_DATABASE_CONNECTION; //failed to connect to database
            }

        }
        public static string GetType(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand GetType = new SqlCommand(clsConstants.P_GET_TYPE, Connection);
                    GetType.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetType.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                    GetType.Parameters["@" + clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetType.Parameters.Add(new SqlParameter("@" + clsConstants.TYPE, SqlDbType.VarChar, 1000));
                    GetType.Parameters["@" + clsConstants.TYPE].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetType.ExecuteNonQuery();

                    return GetType.Parameters["@" + clsConstants.TYPE].Value.ToString();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.", clsConstants.ERROR, E.Message);
                return null;
            }

        }


        public static int EditQuestionSmiley(int Id, int NumberOfSmileys)
        {
            try
            {

                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {
                        SqlCommand EditNumberOfSmileys = new SqlCommand(clsConstants.P_EDIT_QUESTION_SMILEY, Connection);
                        EditNumberOfSmileys.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        EditNumberOfSmileys.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        EditNumberOfSmileys.Parameters["@" + clsConstants.ID].Value = Id;
                        EditNumberOfSmileys.Parameters.Add(new SqlParameter("@" + clsConstants.NUMBER_OF_SMILEYS, SqlDbType.Int));
                        EditNumberOfSmileys.Parameters["@" + clsConstants.NUMBER_OF_SMILEYS].Value = NumberOfSmileys;

                        //run previously stored procedure
                        if (EditNumberOfSmileys.ExecuteNonQuery() > 0)
                        {
                            DataUpdatedInDataLayer();
                            return clsConstants.SUCCESS;
                        }
                        else
                        {
                            return clsConstants.FAILED_EDIT_QUESTION; //failed Update
                        }

                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_EDIT_QUESTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_EDIT_QUESTION; //failed Update
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
        //setters and getters for stars question properties - NumberOfStars
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

        public static int GetNumberOfStarss(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand GetNumberOfStars = new SqlCommand(clsConstants.P_GET_STARS_QUESTION, Connection);
                    GetNumberOfStars.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetNumberOfStars.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                    GetNumberOfStars.Parameters["@" + clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetNumberOfStars.Parameters.Add(new SqlParameter("@" + clsConstants.NUMBER_OF_STARS, SqlDbType.Int));
                    GetNumberOfStars.Parameters["@" + clsConstants.NUMBER_OF_STARS].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetNumberOfStars.ExecuteNonQuery();

                    return int.Parse(GetNumberOfStars.Parameters["@" + clsConstants.NUMBER_OF_STARS].Value.ToString());

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.", clsConstants.ERROR, E.Message);
                return 0;
            }

        }


        public static int EditQuestionStars(int Id, int NumberOfStars)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {
                        SqlCommand EditQuestionStars = new SqlCommand(clsConstants.P_EDIT_QUESTION_STARS, Connection);
                        EditQuestionStars.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        EditQuestionStars.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        EditQuestionStars.Parameters["@" + clsConstants.ID].Value = Id;
                        EditQuestionStars.Parameters.Add(new SqlParameter("@" + clsConstants.NUMBER_OF_STARS, SqlDbType.Int));
                        EditQuestionStars.Parameters["@" + clsConstants.NUMBER_OF_STARS].Value = NumberOfStars;


                        //run previously stored procedure
                        if (EditQuestionStars.ExecuteNonQuery() > 0)
                        {
                            DataUpdatedInDataLayer();
                            return clsConstants.SUCCESS;
                        }
                            
                        else
                            return clsConstants.FAILED_EDIT_QUESTION;
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_EDIT_QUESTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_EDIT_QUESTION; //failed Update
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_DATABASE_CONNECTION; //failed to connect to database
            }
        }

        //setters and getters for stars question properties - Start and End Values + Captions
        public static int GetStartValue(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand GetStartValue = new SqlCommand(clsConstants.P_GET_START_VALUE, Connection);
                    GetStartValue.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetStartValue.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                    GetStartValue.Parameters["@" + clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetStartValue.Parameters.Add(new SqlParameter("@" + clsConstants.START_VALUE, SqlDbType.Int));
                    GetStartValue.Parameters["@" + clsConstants.START_VALUE].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetStartValue.ExecuteNonQuery();

                    return int.Parse(GetStartValue.Parameters["@" + clsConstants.START_VALUE].Value.ToString());

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.", clsConstants.ERROR, E.Message);
                return 0;
            }

        }
        public static int GetEndValue(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand GetEndValue = new SqlCommand(clsConstants.P_GET_END_VALUE, Connection);
                    GetEndValue.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetEndValue.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                    GetEndValue.Parameters["@" + clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetEndValue.Parameters.Add(new SqlParameter("@" + clsConstants.END_VALUE, SqlDbType.Int));
                    GetEndValue.Parameters["@" + clsConstants.END_VALUE].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetEndValue.ExecuteNonQuery();

                    return int.Parse(GetEndValue.Parameters["@" + clsConstants.END_VALUE].Value.ToString());

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.", clsConstants.ERROR, E.Message);
                return 100;
            }
        }
        public static string GetStartCaption(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand GetStartCaption = new SqlCommand(clsConstants.P_GET_START_CAPTION, Connection);
                    GetStartCaption.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetStartCaption.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                    GetStartCaption.Parameters["@" + clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetStartCaption.Parameters.Add(new SqlParameter("@" + clsConstants.START_CAPTION, SqlDbType.VarChar, 20));
                    GetStartCaption.Parameters["@" + clsConstants.START_CAPTION].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetStartCaption.ExecuteNonQuery();

                    return GetStartCaption.Parameters["@" + clsConstants.START_CAPTION].Value.ToString();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.", clsConstants.ERROR, E.Message);
                return "";
            }
        }
        public static string GetEndCaption(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand GetEndCaption = new SqlCommand(clsConstants.P_GET_END_CAPTION, Connection);
                    GetEndCaption.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetEndCaption.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                    GetEndCaption.Parameters["@" + clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetEndCaption.Parameters.Add(new SqlParameter("@" + clsConstants.END_CAPTION, SqlDbType.VarChar, 20));
                    GetEndCaption.Parameters["@" + clsConstants.END_CAPTION].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetEndCaption.ExecuteNonQuery();

                    return GetEndCaption.Parameters["@" + clsConstants.END_CAPTION].Value.ToString();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.", clsConstants.ERROR, E.Message);
                return "";
            }
        }
        public static int EditQuestionSlider(int Id, clsQuestionSlider Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {
                        SqlCommand EditQuestionSlider = new SqlCommand(clsConstants.P_EDIT_SLIDER, Connection);
                        EditQuestionSlider.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        EditQuestionSlider.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        EditQuestionSlider.Parameters["@" + clsConstants.ID].Value = Id;
                        EditQuestionSlider.Parameters.Add(new SqlParameter("@" + clsConstants.START_VALUE, SqlDbType.Int));
                        EditQuestionSlider.Parameters["@" + clsConstants.START_VALUE].Value = Question.StartValue;
                        EditQuestionSlider.Parameters.Add(new SqlParameter("@" + clsConstants.END_VALUE, SqlDbType.Int));
                        EditQuestionSlider.Parameters["@" + clsConstants.END_VALUE].Value = Question.EndValue;
                        EditQuestionSlider.Parameters.Add(new SqlParameter("@" + clsConstants.START_CAPTION, SqlDbType.VarChar, 1000));
                        EditQuestionSlider.Parameters["@" + clsConstants.START_CAPTION].Value = Question.StartCaption;
                        EditQuestionSlider.Parameters.Add(new SqlParameter("@" + clsConstants.END_CAPTION, SqlDbType.VarChar, 1000));
                        EditQuestionSlider.Parameters["@" + clsConstants.END_CAPTION].Value = Question.EndCaption;


                        //run previously stored procedure
                        if (EditQuestionSlider.ExecuteNonQuery() > 0)
                        {
                            DataUpdatedInDataLayer();
                            return clsConstants.SUCCESS;
                        }
                        else
                            return clsConstants.FAILED_EDIT_QUESTION;
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog(clsConstants.FAILED_EDIT_QUESTION_STRING, clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_EDIT_QUESTION; //failed Update
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog(clsConstants.FAILED_DATABASE_CONNECTION_STRING, clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_DATABASE_CONNECTION; //failed to connect to database
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



