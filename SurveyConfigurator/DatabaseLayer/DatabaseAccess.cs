using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ErrorLogger;
using System.Data;
using Utilities;
using System.Transactions;
using System.Runtime.Remoting.Channels;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace DatabaseLayer
{
    public class DatabaseAccess
    {
        public static string Server;
        public static string Database;
        public static string IntegratedSecurity;
        public static string Username;
        public static string Password;
        public static string CONNECTION;

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
                Logger.WriteLog("Failed to connect to database. \nCheck Connection Keys in App.config file.", clsConstants.ERROR, E.Message);
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
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.NUMBER_OF_SMILEYS, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.NUMBER_OF_SMILEYS].Value = Question.NumberOfSmileys;

                        // Add the output parameter.
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.ID].Direction = ParameterDirection.Output;

                        //run previously stored procedure
                        if (NewQuestion.ExecuteNonQuery() > 0)
                        {
                            Logger.WriteLog("Question created successfully.", clsConstants.INFORMATION, "Question ID:" + NewQuestion.Parameters["@" + clsConstants.ID].Value);
                            return clsConstants.SUCCESS; //success code
                        }
                        else
                        {
                            return clsConstants.FAILED_NEW_QUESTION;
                        }
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Failed To Add Question", clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_NEW_QUESTION; //failed insertion
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed To Connect to Database.", clsConstants.ERROR, E.Message);
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
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.NUMBER_OF_STARS, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.NUMBER_OF_STARS].Value = Question.NumberOfStars;

                        // Add the output parameter.
                        NewQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        NewQuestion.Parameters["@" + clsConstants.ID].Direction = ParameterDirection.Output;

                        //run previously stored procedure
                        if (NewQuestion.ExecuteNonQuery() > 0)
                        {
                            Logger.WriteLog("Question created successfully.", clsConstants.INFORMATION, "Question ID:" + NewQuestion.Parameters["@" + clsConstants.ID].Value);
                            return clsConstants.SUCCESS; //success code
                        }
                        else
                        {
                            return clsConstants.FAILED_NEW_QUESTION;
                        }
                    }

                    catch (Exception E)
                    {
                        Logger.WriteLog("Failed To Add Question", clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_NEW_QUESTION; //failed insertion
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed To Connect to Database.", clsConstants.ERROR, E.Message);
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
                        // Add input parameter for the stored procedure and specify what to use as its value.
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
                            Logger.WriteLog("Question created successfully.", clsConstants.INFORMATION, "Question ID:" + NewQuestion.Parameters["@" + clsConstants.ID].Value);
                            return clsConstants.SUCCESS; //success code
                        }
                        else
                        {
                            return clsConstants.FAILED_NEW_QUESTION;
                        }
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Failed To Add Question", clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_NEW_QUESTION; //failed insertion
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed To Connect to Database.", clsConstants.ERROR, E.Message);
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
                            return clsConstants.SUCCESS;
                        else
                            return clsConstants.FAILED_DELETE_QUESTION;
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Failed To Delete Question", clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_DELETE_QUESTION; //failed deletion
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed to Connect to Database.", clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_DATABASE_CONNECTION; //failed to connect to database
            }


        }

        public static DataTable ListQuestions()
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();

                    SqlCommand Delete = new SqlCommand(clsConstants.P_VIEW, Connection);
                    Delete.CommandType = CommandType.StoredProcedure;
                    using (var adapter = new SqlDataAdapter(Delete))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        Logger.WriteLog("", clsConstants.INFORMATION, "Table in main page fetched from database successfully.");
                        return table;
                    }

                }

            }
            catch (Exception E)
            {
                Logger.WriteLog("View Procedure failed.", clsConstants.ERROR, E.Message);
                return null;
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
                        if(EditText.ExecuteNonQuery()>=1)
                            return clsConstants.SUCCESS;
                        else 
                            return clsConstants.FAILED_EDIT_QUESTION;
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Failed To Edit Question", clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_EDIT_QUESTION; //failed Update
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed To Connect to Database.", clsConstants.ERROR, E.Message);
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


        //setters and getters for smiley question properties - NumberOfSmileys
        public static int GetNumberOfSmileys(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand GetNumberOfSmileys = new SqlCommand(clsConstants.P_GET_NUMBER_OF_SMILEYS, Connection);
                    GetNumberOfSmileys.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetNumberOfSmileys.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                    GetNumberOfSmileys.Parameters["@" + clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetNumberOfSmileys.Parameters.Add(new SqlParameter("@" + clsConstants.NUMBER_OF_SMILEYS, SqlDbType.VarChar, 1000));
                    GetNumberOfSmileys.Parameters["@" + clsConstants.NUMBER_OF_SMILEYS].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetNumberOfSmileys.ExecuteNonQuery();

                    return int.Parse(GetNumberOfSmileys.Parameters["@" + clsConstants.NUMBER_OF_SMILEYS].Value.ToString());

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.", clsConstants.ERROR, E.Message);
                return 0;
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
                        if(EditNumberOfSmileys.ExecuteNonQuery()>0)
                            return clsConstants.SUCCESS;
                        else
                        {
                            return clsConstants.FAILED_EDIT_QUESTION; //failed Update
                        }

                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Failed To Update Question", clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_EDIT_QUESTION; //failed Update
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed To Connect to Database.", clsConstants.ERROR, E.Message);
                return clsConstants.FAILED_DATABASE_CONNECTION; //failed to connect to database
            }


        }


        //setters and getters for stars question properties - NumberOfStars
        public static int GetNumberOfStars(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand GetNumberOfStars = new SqlCommand(clsConstants.P_GET_NUMBER_OF_STARS, Connection);
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
                            return clsConstants.SUCCESS;
                        else
                            return clsConstants.FAILED_EDIT_QUESTION;
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Failed To Update Question", clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_EDIT_QUESTION; //failed Update
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed To Connect to Database.", clsConstants.ERROR, E.Message);
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
                            return clsConstants.SUCCESS;
                        else
                            return clsConstants.FAILED_EDIT_QUESTION;
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Failed To Update Question", clsConstants.ERROR, E.Message);
                        return clsConstants.FAILED_EDIT_QUESTION; //failed Update
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed To Connect to Database.", clsConstants.ERROR, E.Message);
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
                                        Type = Reader["Type"].ToString(),
                                        Id = (int)Reader["Id"],
                                        Text = Reader["Text"].ToString(),
                                        Properties =  Reader["Question Properties"].ToString()
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



