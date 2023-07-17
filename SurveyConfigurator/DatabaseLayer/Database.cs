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
namespace DatabaseLayer
{
    public class Database
    {
        public static string  CONNECTION = "Data Source="+ConfigurationManager.AppSettings["Server"]+";Initial Catalog = " + ConfigurationManager.AppSettings["Database"] + "; Integrated Security = True;";
        private int QuestionId;
        public static bool CanConnect()
        {
            SqlConnection Connection = new SqlConnection(CONNECTION);
            try
            {
                Connection.Open();
                return true;
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed to connect to database. \nCheck Connection String in App.config file.", clsConstants.ERROR,E.Message);
                return false;
            }
        }

        public static int StoreQuestion(clsQuestionSmiley Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {

                        SqlCommand InsertQuestion = new SqlCommand(clsConstants.P_INSERT, Connection);
                        InsertQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TEXT, SqlDbType.VarChar, 1000));
                        InsertQuestion.Parameters["@" + clsConstants.TEXT].Value = Question.Text;
                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TYPE, SqlDbType.VarChar, 20));
                        InsertQuestion.Parameters["@" + clsConstants.TYPE].Value = Question.Type;
                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.NUMBER_OF_SMILEYS, SqlDbType.Int));
                        InsertQuestion.Parameters["@" + clsConstants.NUMBER_OF_SMILEYS].Value = Question.NumberOfSmileys;

                        // Add the output parameter.
                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        InsertQuestion.Parameters["@" + clsConstants.ID].Direction = ParameterDirection.Output;

                        //run previously stored procedure
                        InsertQuestion.ExecuteNonQuery();
                        Logger.WriteLog("Question created successfully.", clsConstants.INFORMATION, "Question ID:" + (int)InsertQuestion.Parameters["@" + clsConstants.ID].Value);
                        return 0; //success code
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Failed To Add Question", clsConstants.ERROR, E.Message);
                        return 1; //failed parameter addition
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed To Add Question", clsConstants.ERROR, E.Message);
                return -1; //failed to connect
            }
        }



        public static int StoreQuestion(clsQuestionStar Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {

                        SqlCommand InsertQuestion = new SqlCommand(clsConstants.P_INSERT, Connection);
                        InsertQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TEXT, SqlDbType.VarChar, 1000));
                        InsertQuestion.Parameters["@" + clsConstants.TEXT].Value = Question.Text;
                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TYPE, SqlDbType.VarChar, 20));
                        InsertQuestion.Parameters["@" + clsConstants.TYPE].Value = Question.Type;
                        InsertQuestion.Parameters.Add(new SqlParameter("@"+clsConstants.NUMBER_OF_STARS, SqlDbType.Int));
                        InsertQuestion.Parameters["@" + clsConstants.NUMBER_OF_STARS].Value = Question.NumberOfStars;

                        // Add the output parameter.
                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        InsertQuestion.Parameters["@" + clsConstants.ID].Direction = ParameterDirection.Output;

                        //run previously stored procedure
                        InsertQuestion.ExecuteNonQuery();
                        Logger.WriteLog("Question created successfully.", clsConstants.INFORMATION, "Question ID:" + (int)InsertQuestion.Parameters["@" + clsConstants.ID].Value);
                        return 0; //success code
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Failed To Add Question", clsConstants.ERROR, E.Message);
                        return 1; //failed parameter addition
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed To Add Question", clsConstants.ERROR, E.Message);
                return -1; //failed to connect
            }
        }

        public static int StoreQuestion(clsQuestionSlider Question)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    try
                    {

                        SqlCommand InsertQuestion = new SqlCommand(clsConstants.P_INSERT, Connection);
                        InsertQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TEXT, SqlDbType.VarChar, 1000));
                        InsertQuestion.Parameters["@" + clsConstants.TEXT].Value = Question.Text;
                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.TYPE, SqlDbType.VarChar, 20));
                        InsertQuestion.Parameters["@" + clsConstants.TYPE].Value = Question.Type;
                        // Add input parameter for the stored procedure and specify what to use as its value.
                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.START_VALUE, SqlDbType.Int));
                        InsertQuestion.Parameters["@" + clsConstants.START_VALUE].Value = Question.StartValue;

                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.END_VALUE, SqlDbType.Int));
                        InsertQuestion.Parameters["@" + clsConstants.END_VALUE].Value = Question.EndValue;

                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.START_CAPTION, SqlDbType.VarChar, 50));
                        InsertQuestion.Parameters["@" + clsConstants.START_CAPTION].Value = Question.StartCaption;

                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.END_CAPTION, SqlDbType.VarChar, 50));
                        InsertQuestion.Parameters["@" + clsConstants.END_CAPTION].Value = Question.EndCaption;

                        // Add the output parameter.
                        InsertQuestion.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                        InsertQuestion.Parameters["@" + clsConstants.ID].Direction = ParameterDirection.Output;

                        //run previously stored procedure
                        InsertQuestion.ExecuteNonQuery();
                        Logger.WriteLog("Question created successfully.", clsConstants.INFORMATION, "Question ID:" +  InsertQuestion.Parameters["@" + clsConstants.ID].Value );
                        return 0; //success code
                    }
                    catch (Exception E)
                    {
                        Logger.WriteLog("Failed To Add Question", clsConstants.ERROR, E.Message);
                        return 1; //failed parameter addition
                    }
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed To Add Question", clsConstants.ERROR, E.Message);
                return -1; //failed to connect
            }
        }


        public static void DeleteQuestion(int Id)
        {

            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();

                    SqlCommand Delete = new SqlCommand(clsConstants.P_DELETE, Connection);
                    Delete.CommandType = CommandType.StoredProcedure;

                    Delete.Parameters.Add(new SqlParameter("@" + clsConstants.ID, SqlDbType.Int));
                    Delete.Parameters["@" + clsConstants.ID].Value = Id;

                    //run previously stored delete procedure
                    Delete.ExecuteNonQuery();
                    Logger.WriteLog("Question Deleted successfully.", clsConstants.INFORMATION, "Question ID:" + Id);
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Delete Failed. Please Try Again.", clsConstants.ERROR, E.Message);
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
                        Logger.WriteLog("",clsConstants.INFORMATION,"Table in main page fetched from database successfully.");
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
                    GetText.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    GetText.Parameters["@"+clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetText.Parameters.Add(new SqlParameter("@"+clsConstants.TEXT, SqlDbType.VarChar, 1000));
                    GetText.Parameters["@"+clsConstants.TEXT].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetText.ExecuteNonQuery();
                    
                    return GetText.Parameters["@"+clsConstants.TEXT].Value.ToString();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.", clsConstants.ERROR, E.Message);
                return null;
            }
           
        }
        public static void SetText(int Id,string Text)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand SetText = new SqlCommand(clsConstants.P_SET_TEXT, Connection);
                    SetText.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetText.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    SetText.Parameters["@"+clsConstants.ID].Value = Id;
                    SetText.Parameters.Add(new SqlParameter("@"+clsConstants.TEXT, SqlDbType.VarChar,1000));
                    SetText.Parameters["@"+clsConstants.TEXT].Value = Text;

                    //run previously stored procedure
                    SetText.ExecuteNonQuery();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful Save.", clsConstants.ERROR, E.Message);
                
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
                    GetType.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    GetType.Parameters["@"+clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetType.Parameters.Add(new SqlParameter("@"+clsConstants.TYPE, SqlDbType.VarChar, 1000));
                    GetType.Parameters["@"+clsConstants.TYPE].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetType.ExecuteNonQuery();

                    return GetType.Parameters["@"+clsConstants.TYPE].Value.ToString();

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
                    GetNumberOfSmileys.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    GetNumberOfSmileys.Parameters["@"+clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetNumberOfSmileys.Parameters.Add(new SqlParameter("@"+clsConstants.NUMBER_OF_SMILEYS, SqlDbType.VarChar, 1000));
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
        public static void SetNumberOfSmileys(int Id, int NumberOfSmileys)
        {
            try
            {

                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand SetNumberOfSmileys = new SqlCommand(clsConstants.P_SET_NUMBER_OF_SMILEYS, Connection);
                    SetNumberOfSmileys.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetNumberOfSmileys.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    SetNumberOfSmileys.Parameters["@"+clsConstants.ID].Value = Id;
                    SetNumberOfSmileys.Parameters.Add(new SqlParameter("@" + clsConstants.NUMBER_OF_SMILEYS, SqlDbType.Int));
                    SetNumberOfSmileys.Parameters["@" + clsConstants.NUMBER_OF_SMILEYS].Value = NumberOfSmileys;

                    //run previously stored procedure
                    SetNumberOfSmileys.ExecuteNonQuery();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful Save.", clsConstants.ERROR, E.Message);

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
                    GetNumberOfStars.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    GetNumberOfStars.Parameters["@"+clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetNumberOfStars.Parameters.Add(new SqlParameter("@"+clsConstants.NUMBER_OF_STARS, SqlDbType.Int));
                    GetNumberOfStars.Parameters["@"+clsConstants.NUMBER_OF_STARS].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetNumberOfStars.ExecuteNonQuery();

                    return int.Parse(GetNumberOfStars.Parameters["@"+clsConstants.NUMBER_OF_STARS].Value.ToString());

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.", clsConstants.ERROR, E.Message);
                return 0;
            }

        }
        public static void SetNumberOfStars(int Id, int NumberOfStars)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand SetNumberOfStars = new SqlCommand(clsConstants.P_SET_NUMBER_OF_STARS, Connection);
                    SetNumberOfStars.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetNumberOfStars.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    SetNumberOfStars.Parameters["@"+clsConstants.ID].Value = Id;
                    SetNumberOfStars.Parameters.Add(new SqlParameter("@"+clsConstants.NUMBER_OF_STARS, SqlDbType.Int));
                    SetNumberOfStars.Parameters["@"+clsConstants.NUMBER_OF_STARS].Value =NumberOfStars;


                    //run previously stored procedure
                    SetNumberOfStars.ExecuteNonQuery();

                    
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful Save.", clsConstants.ERROR, E.Message);
               
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
                    GetStartValue.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    GetStartValue.Parameters["@"+clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetStartValue.Parameters.Add(new SqlParameter("@"+clsConstants.START_VALUE, SqlDbType.Int));
                    GetStartValue.Parameters["@"+clsConstants.START_VALUE].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetStartValue.ExecuteNonQuery();

                    return int.Parse(GetStartValue.Parameters["@"+clsConstants.START_VALUE].Value.ToString());

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
                    GetEndValue.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    GetEndValue.Parameters["@"+clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetEndValue.Parameters.Add(new SqlParameter("@"+clsConstants.END_VALUE, SqlDbType.Int));
                    GetEndValue.Parameters["@"+clsConstants.END_VALUE].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetEndValue.ExecuteNonQuery();

                    return int.Parse(GetEndValue.Parameters["@"+clsConstants.END_VALUE].Value.ToString());

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
                    GetStartCaption.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    GetStartCaption.Parameters["@"+clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetStartCaption.Parameters.Add(new SqlParameter("@"+clsConstants.START_CAPTION, SqlDbType.VarChar,20));
                    GetStartCaption.Parameters["@"+clsConstants.START_CAPTION].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetStartCaption.ExecuteNonQuery();

                    return GetStartCaption.Parameters["@"+clsConstants.START_CAPTION].Value.ToString();

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
                    GetEndCaption.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    GetEndCaption.Parameters["@"+clsConstants.ID].Value = Id;


                    // Add the output parameter.
                    GetEndCaption.Parameters.Add(new SqlParameter("@"+clsConstants.END_CAPTION, SqlDbType.VarChar, 20));
                    GetEndCaption.Parameters["@"+clsConstants.END_CAPTION].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetEndCaption.ExecuteNonQuery();

                    return GetEndCaption.Parameters["@"+clsConstants.END_CAPTION].Value.ToString();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.", clsConstants.ERROR, E.Message);
                return "";
            }
        }
        public static void SetStartValue(int Id, int StartValue)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand SetStartValue = new SqlCommand(clsConstants.P_SET_START_VALUE, Connection);
                    SetStartValue.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetStartValue.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    SetStartValue.Parameters["@"+clsConstants.ID].Value = Id;
                    SetStartValue.Parameters.Add(new SqlParameter("@"+clsConstants.START_VALUE, SqlDbType.Int));
                    SetStartValue.Parameters["@"+clsConstants.START_VALUE].Value = StartValue;


                    //run previously stored procedure
                    SetStartValue.ExecuteNonQuery();

                }
            }
            catch(Exception E)
            {
                Logger.WriteLog("Unsuccessful Save.", clsConstants.ERROR, E.Message);

            }
        }
        public static void SetEndValue(int Id, int EndValue)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand SetEndValue = new SqlCommand(clsConstants.P_SET_END_VALUE, Connection);
                    SetEndValue.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetEndValue.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    SetEndValue.Parameters["@"+clsConstants.ID].Value = Id;
                    SetEndValue.Parameters.Add(new SqlParameter("@"+clsConstants.END_VALUE, SqlDbType.Int));
                    SetEndValue.Parameters["@"+clsConstants.END_VALUE].Value = EndValue;


                    //run previously stored procedure
                    SetEndValue.ExecuteNonQuery();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful Save.", clsConstants.ERROR, E.Message);

            }
        }
        public static void SetStartCaption(int Id, string StartCaption)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand SetStartCaption = new SqlCommand(clsConstants.P_SET_START_CAPTION, Connection);
                    SetStartCaption.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetStartCaption.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    SetStartCaption.Parameters["@"+clsConstants.ID].Value = Id;
                    SetStartCaption.Parameters.Add(new SqlParameter("@"+clsConstants.START_CAPTION, SqlDbType.VarChar,1000));
                    SetStartCaption.Parameters["@"+clsConstants.START_CAPTION].Value = StartCaption;


                    //run previously stored procedure
                    SetStartCaption.ExecuteNonQuery();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful Save.", clsConstants.ERROR, E.Message);

            }
        }
        public static void SetEndCaption(int Id, string EndCaption)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(CONNECTION))
                {
                    Connection.Open();
                    SqlCommand SetEndCaption = new SqlCommand(clsConstants.P_SET_END_CAPTION, Connection);
                    SetEndCaption.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetEndCaption.Parameters.Add(new SqlParameter("@"+clsConstants.ID, SqlDbType.Int));
                    SetEndCaption.Parameters["@"+clsConstants.ID].Value = Id;
                    SetEndCaption.Parameters.Add(new SqlParameter("@"+clsConstants.END_CAPTION, SqlDbType.VarChar, 1000));
                    SetEndCaption.Parameters["@"+clsConstants.END_CAPTION].Value = EndCaption;


                    //run previously stored procedure
                    SetEndCaption.ExecuteNonQuery();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful Save.", clsConstants.ERROR, E.Message);

            }
        }
    }
}


                           
