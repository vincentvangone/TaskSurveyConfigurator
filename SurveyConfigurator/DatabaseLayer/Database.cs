using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ErrorLogger;
using System.Data;
using System.Windows.Forms;
using Utilities;
using System.Transactions;

namespace DatabaseLayer
{
    public class Database
    {
        private int QuestionId;

        public const string SMILEY = "Smiley";
        public const string STAR = "Star";
        public const string SLIDER = "Slider";
        public static bool CanConnect()
        {
            SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            try
            {
                Connection.Open();
                return true;
            }
            catch (Exception E)
            {
                Logger.WriteLog("Failed to connect to database. \nCheck Connection String in App.config file.");
                return false;
            }
        }
        public void StoreSmileyQuestion(clsQuestionSmiley Question)
        {
            try
            {
                //storing a question requires 2 insertions -> use transaction
                //guarantees that both commands can commit or roll back as a single unit of work
                using (TransactionScope Scope = new TransactionScope())
                {
                    using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                    {
                        Connection.Open();
                        SqlCommand InsertQuestion = new SqlCommand("P_InsertQuestion", Connection);
                        InsertQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        InsertQuestion.Parameters.Add(new SqlParameter("@Text", SqlDbType.VarChar, 1000));
                        InsertQuestion.Parameters["@Text"].Value = Question.Text;
                        InsertQuestion.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar, 20));
                        InsertQuestion.Parameters["@Type"].Value = Question.Type;

                        // Add the output parameter.
                        InsertQuestion.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                        InsertQuestion.Parameters["@Id"].Direction = ParameterDirection.Output;

                        //run previously stored procedure
                        InsertQuestion.ExecuteNonQuery();

                        // Question ID is an IDENTITY value from the database.
                        // Save it to execute second insertion
                        this.QuestionId = (int)InsertQuestion.Parameters["@Id"].Value;


                        using (SqlCommand InsertQuestionSmiley = new SqlCommand("P_InsertQuestionSmiley", Connection))
                        {

                            InsertQuestionSmiley.CommandType = CommandType.StoredProcedure;

                            // Add input parameter for the stored procedure and specify what to use as its value.
                            InsertQuestionSmiley.Parameters.Add(new SqlParameter("@NumberOfSmileys", SqlDbType.Int));
                            InsertQuestionSmiley.Parameters["@NumberOfSmileys"].Value = Question.NumberOfSmileys;

                            // Add the @QuestionID input parameter, which was obtained from insertQuestion.
                            InsertQuestionSmiley.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                            InsertQuestionSmiley.Parameters["@Id"].Value = this.QuestionId;

                            //run previously stored procedure
                            InsertQuestionSmiley.ExecuteNonQuery();
                            
                            //display question's number
                            MessageBox.Show("Question number " + this.QuestionId + " has been created.");

                        }
                    }
                    Scope.Complete();
                }

            }
            catch (TransactionAbortedException Exception)
            {
                Logger.WriteLog("Save Failed. Please Try Again.", Exception.Message);
            }

        }

        public void StoreSliderQuestion(clsQuestionSlider Question)
        {
            try
            {
                //storing a question requires 2 insertions -> use transaction
                //guarantees that both commands can commit or roll back as a single unit of work
                using (TransactionScope Scope = new TransactionScope())
                {
                    using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                    {
                        Connection.Open();
                        SqlCommand InsertQuestion = new SqlCommand("P_InsertQuestion", Connection);
                        InsertQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        InsertQuestion.Parameters.Add(new SqlParameter("@Text", SqlDbType.VarChar, 1000));
                        InsertQuestion.Parameters["@Text"].Value = Question.Text;
                        InsertQuestion.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar, 20));
                        InsertQuestion.Parameters["@Type"].Value = Question.Type;

                        // Add the output parameter.
                        InsertQuestion.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                        InsertQuestion.Parameters["@Id"].Direction = ParameterDirection.Output;

                        //run previously stored procedure
                        InsertQuestion.ExecuteNonQuery();

                        // Question ID is an IDENTITY value from the database.
                        // Save it to execute second insertion
                        this.QuestionId = (int)InsertQuestion.Parameters["@Id"].Value;


                        using (SqlCommand InsertQuestionSlider = new SqlCommand("P_InsertQuestionSlider", Connection))
                        {

                            InsertQuestionSlider.CommandType = CommandType.StoredProcedure;

                            // Add input parameter for the stored procedure and specify what to use as its value.
                            InsertQuestionSlider.Parameters.Add(new SqlParameter("@StartValue", SqlDbType.Int));
                            InsertQuestionSlider.Parameters["@StartValue"].Value = Question.StartValue;

                            InsertQuestionSlider.Parameters.Add(new SqlParameter("@EndValue", SqlDbType.Int));
                            InsertQuestionSlider.Parameters["@EndValue"].Value = Question.EndValue;

                            InsertQuestionSlider.Parameters.Add(new SqlParameter("@StartCaption", SqlDbType.VarChar, 50));
                            InsertQuestionSlider.Parameters["@StartCaption"].Value = Question.StartCaption;

                            InsertQuestionSlider.Parameters.Add(new SqlParameter("@EndCaption", SqlDbType.VarChar, 50));
                            InsertQuestionSlider.Parameters["@EndCaption"].Value = Question.EndCaption;

                            // Add the @QuestionID input parameter, which was obtained from insertQuestion.
                            InsertQuestionSlider.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                            InsertQuestionSlider.Parameters["@Id"].Value = this.QuestionId;

                            //run previously stored procedure
                            InsertQuestionSlider.ExecuteNonQuery();
                            //RETURN COUNTER OF THE QUESTION ID
                            //display question's number
                            this.QuestionId = (int)InsertQuestionSlider.Parameters["@Id"].Value;
                            MessageBox.Show("Question number " + this.QuestionId + " has been created.");

                        }
                    }
                    Scope.Complete();
                }

            }
            catch (TransactionAbortedException Exception)
            {
                Logger.WriteLog("Save Failed. Please Try Again.", Exception.Message);
            }

        }


        public void StoreStarQuestion(clsQuestionStar Question)
        {
            try
            {
                //storing a question requires 2 insertions -> use transaction
                //guarantees that both commands can commit or roll back as a single unit of work
                using (TransactionScope Scope = new TransactionScope())
                {
                    using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                    {
                        Connection.Open();
                        SqlCommand InsertQuestion = new SqlCommand("P_InsertQuestion", Connection);
                        InsertQuestion.CommandType = CommandType.StoredProcedure;

                        // Add input parameter for the stored procedure and specify what to use as its value.
                        InsertQuestion.Parameters.Add(new SqlParameter("@Text", SqlDbType.VarChar, 1000));
                        InsertQuestion.Parameters["@Text"].Value = Question.Text;
                        InsertQuestion.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar, 20));
                        InsertQuestion.Parameters["@Type"].Value = Question.Type;

                        // Add the output parameter.
                        InsertQuestion.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                        InsertQuestion.Parameters["@Id"].Direction = ParameterDirection.Output;

                        //run previously stored procedure
                        InsertQuestion.ExecuteNonQuery();

                        // Question ID is an IDENTITY value from the database.
                        // Save it to execute second insertion
                        this.QuestionId = (int)InsertQuestion.Parameters["@Id"].Value;


                        using (SqlCommand InsertQuestionStar = new SqlCommand("P_InsertQuestionStar", Connection))
                        {

                            InsertQuestionStar.CommandType = CommandType.StoredProcedure;

                            // Add input parameter for the stored procedure and specify what to use as its value.
                            InsertQuestionStar.Parameters.Add(new SqlParameter("@NumberOfStars", SqlDbType.Int));
                            InsertQuestionStar.Parameters["@NumberOfStars"].Value = Question.NumberOfStars;

                            // Add the @QuestionID input parameter, which was obtained from insertQuestion.
                            InsertQuestionStar.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                            InsertQuestionStar.Parameters["@Id"].Value = this.QuestionId;

                            //run previously stored procedure
                            InsertQuestionStar.ExecuteNonQuery();
                            //RETURN COUNTER OF THE QUESTION ID
                            //display question's number
                            this.QuestionId = (int)InsertQuestionStar.Parameters["@Id"].Value;
                            MessageBox.Show("Question number " + this.QuestionId + " has been created.");

                        }
                    }
                    Scope.Complete();
                }

            }
            catch (TransactionAbortedException Exception)
            {
                Logger.WriteLog("Save Failed. Please Try Again.", Exception.Message);
            }

        }

        public static void DeleteQuestion(int Id, string Type)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete question " + Id + " ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.OK)
            {

                try
                {
                    //deleting a question requires 2 insertions -> use transaction
                    //guarantees that both commands can commit or roll back as a single unit of work
                    using (TransactionScope Scope = new TransactionScope())
                    {
                        using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                        {
                            Connection.Open();

                            if (Type == SMILEY)
                            {
                                SqlCommand DeleteQuestionSmiley = new SqlCommand("P_DeleteQuestionSmiley", Connection);
                                DeleteQuestionSmiley.CommandType = CommandType.StoredProcedure;

                                DeleteQuestionSmiley.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                                DeleteQuestionSmiley.Parameters["@Id"].Value = Id;

                                //run previously stored procedure
                                DeleteQuestionSmiley.ExecuteNonQuery();
                            }

                            else if (Type == STAR)
                            {
                                SqlCommand DeleteQuestionStar = new SqlCommand("P_DeleteQuestionStars", Connection);
                                DeleteQuestionStar.CommandType = CommandType.StoredProcedure;

                                DeleteQuestionStar.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                                DeleteQuestionStar.Parameters["@Id"].Value = Id;

                                //run previously stored procedure
                                DeleteQuestionStar.ExecuteNonQuery();
                            }

                            else if (Type == SLIDER)
                            {
                                SqlCommand DeleteQuestionSlider = new SqlCommand("P_DeleteQuestionSlider", Connection);
                                DeleteQuestionSlider.CommandType = CommandType.StoredProcedure;

                                DeleteQuestionSlider.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                                DeleteQuestionSlider.Parameters["@Id"].Value = Id;

                                //run previously stored procedure
                                DeleteQuestionSlider.ExecuteNonQuery();
                            }

                            //deleting must be executed in this order because foreign key
                            using (SqlCommand DeleteQuestion = new SqlCommand("P_DeleteQuestion", Connection))
                            {

                                DeleteQuestion.CommandType = CommandType.StoredProcedure;

                                DeleteQuestion.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                                DeleteQuestion.Parameters["@Id"].Value = Id;

                                //run previously stored procedure
                                DeleteQuestion.ExecuteNonQuery();

                                MessageBox.Show("Question number " + Id + " has been deleted.");

                            }
                        }
                        Scope.Complete();
                    }

                }
                catch (TransactionAbortedException Exception)
                {
                    Logger.WriteLog("Delete Failed. Please Try Again.",Exception.Message);
                }

            }
        }


      

        public static DataTable ListQuestions()
        {
            string SelectQuery = "SELECT Question.Id,Text,Type,concat('Number of Smileys: ',NumberOfSmileys) as 'Question Properties' FROM QuestionSmiley LEFT JOIN Question ON QuestionSmiley.Id = Question.Id \r\n\tUNION \r\n    SELECT Question.Id,Text,Type,concat('Number of Stars: ', NumberOfStars) as 'Question Properties' FROM QuestionStars LEFT JOIN Question ON QuestionStars.Id = Question.Id\r\n    UNION \r\n    SELECT Question.Id,Text,Type,concat('Start Value: ',StartValue, '  Start Caption: ', StartCaption,'  End Value: ', EndValue, '  End Caption: ' ,EndCaption) as 'Question Properties' FROM QuestionSlider LEFT JOIN Question ON QuestionSlider.Id = Question.Id";
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    using (var adapter = new SqlDataAdapter(SelectQuery, Connection))
                    {
                        var table = new DataTable();

                        //table.Columns.Add("Select", typeof(bool));
                        adapter.Fill(table);
                        return table;


                    }
                }

            }
            catch (Exception E)
            {
                Logger.WriteLog("View Procedure failed.",E.Message );
                return null;
            }
        }

        //setters and getters for common inputs - text + type
        public static string GetText(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand GetText = new SqlCommand("P_GetText", Connection);
                    GetText.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetText.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    GetText.Parameters["@Id"].Value = Id;


                    // Add the output parameter.
                    GetText.Parameters.Add(new SqlParameter("@Text", SqlDbType.VarChar, 1000));
                    GetText.Parameters["@Text"].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetText.ExecuteNonQuery();
                    
                    return GetText.Parameters["@Text"].Value.ToString();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit." , E.Message);
                return null;
            }
           
        }
        public static void SetText(int Id,string Text)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand SetText = new SqlCommand("P_SetText", Connection);
                    SetText.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetText.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    SetText.Parameters["@Id"].Value = Id;
                    SetText.Parameters.Add(new SqlParameter("@Text", SqlDbType.VarChar,1000));
                    SetText.Parameters["@Text"].Value = Text;

                    //run previously stored procedure
                    SetText.ExecuteNonQuery();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful Save." ,E.Message);
                
            }

        }
        public static string GetType(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand GetType = new SqlCommand("P_GetType", Connection);
                    GetType.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetType.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    GetType.Parameters["@Id"].Value = Id;


                    // Add the output parameter.
                    GetType.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar, 1000));
                    GetType.Parameters["@Type"].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetType.ExecuteNonQuery();

                    return GetType.Parameters["@Type"].Value.ToString();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit." ,E.Message);
                return null;
            }

        }


        //setters and getters for smiley question properties - NumberOfSmileys
        public static int GetNumberOfSmileys(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand GetNumberOfSmileys = new SqlCommand("P_GetNumberOfSmileys", Connection);
                    GetNumberOfSmileys.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetNumberOfSmileys.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    GetNumberOfSmileys.Parameters["@Id"].Value = Id;


                    // Add the output parameter.
                    GetNumberOfSmileys.Parameters.Add(new SqlParameter("@NumberOfSmileys", SqlDbType.VarChar, 1000));
                    GetNumberOfSmileys.Parameters["@NumberOfSmileys"].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetNumberOfSmileys.ExecuteNonQuery();

                    return int.Parse(GetNumberOfSmileys.Parameters["@NumberOfSmileys"].Value.ToString());

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit." , E.Message);
                return 0;
            }

        }
        public static void SetNumberOfSmileys(int Id, int NumberOfSmileys)
        {
            try
            {

                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand SetNumberOfSmileys = new SqlCommand("P_SetNumberOfSmileys", Connection);
                    SetNumberOfSmileys.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetNumberOfSmileys.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    SetNumberOfSmileys.Parameters["@Id"].Value = Id;
                    SetNumberOfSmileys.Parameters.Add(new SqlParameter("@NumberOfSmileys", SqlDbType.Int));
                    SetNumberOfSmileys.Parameters["@NumberOfSmileys"].Value = NumberOfSmileys;

                    //run previously stored procedure
                    SetNumberOfSmileys.ExecuteNonQuery();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful Save." , E.Message);

            }


        }


        //setters and getters for stars question properties - NumberOfStars
        public static int GetNumberOfStars(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand GetNumberOfStars = new SqlCommand("P_GetNumberOfStars", Connection);
                    GetNumberOfStars.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetNumberOfStars.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    GetNumberOfStars.Parameters["@Id"].Value = Id;


                    // Add the output parameter.
                    GetNumberOfStars.Parameters.Add(new SqlParameter("@NumberOfStars", SqlDbType.Int));
                    GetNumberOfStars.Parameters["@NumberOfStars"].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetNumberOfStars.ExecuteNonQuery();

                    return int.Parse(GetNumberOfStars.Parameters["@NumberOfStars"].Value.ToString());

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit." , E.Message);
                return 0;
            }

        }
        public static void SetNumberOfStars(int Id, int NumberOfStars)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand SetNumberOfStars = new SqlCommand("P_GetNumberOfStars", Connection);
                    SetNumberOfStars.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetNumberOfStars.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    SetNumberOfStars.Parameters["@Id"].Value = Id;
                    SetNumberOfStars.Parameters.Add(new SqlParameter("@NumberOfStars", SqlDbType.Int));
                    SetNumberOfStars.Parameters["@NumberOfStars"].Value =NumberOfStars;


                    //run previously stored procedure
                    SetNumberOfStars.ExecuteNonQuery();

                    
                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful Save.",E.Message );
               
            }
        }

        //setters and getters for stars question properties - Start and End Values + Captions
        public static int GetStartValue(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand GetStartValue = new SqlCommand("P_GetStartValue", Connection);
                    GetStartValue.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetStartValue.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    GetStartValue.Parameters["@Id"].Value = Id;


                    // Add the output parameter.
                    GetStartValue.Parameters.Add(new SqlParameter("@StartValue", SqlDbType.Int));
                    GetStartValue.Parameters["@StartValue"].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetStartValue.ExecuteNonQuery();

                    return int.Parse(GetStartValue.Parameters["@StartValue"].Value.ToString());

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.",E.Message );
                return 0;
            }

        }
        public static int GetEndValue(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand GetEndValue = new SqlCommand("P_GetEndValue", Connection);
                    GetEndValue.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetEndValue.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    GetEndValue.Parameters["@Id"].Value = Id;


                    // Add the output parameter.
                    GetEndValue.Parameters.Add(new SqlParameter("@EndValue", SqlDbType.Int));
                    GetEndValue.Parameters["@EndValue"].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetEndValue.ExecuteNonQuery();

                    return int.Parse(GetEndValue.Parameters["@EndValue"].Value.ToString());

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.",E.Message );
                return 100;
            }
        }
        public static string GetStartCaption(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand GetStartCaption = new SqlCommand("P_GetStartCaption", Connection);
                    GetStartCaption.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetStartCaption.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    GetStartCaption.Parameters["@Id"].Value = Id;


                    // Add the output parameter.
                    GetStartCaption.Parameters.Add(new SqlParameter("@StartCaption", SqlDbType.VarChar,20));
                    GetStartCaption.Parameters["@StartCaption"].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetStartCaption.ExecuteNonQuery();

                    return GetStartCaption.Parameters["@StartCaption"].Value.ToString();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit.",E.Message );
                return "";
            }
        }
        public static string GetEndCaption(int Id)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand GetEndCaption = new SqlCommand("P_GetEndCaption", Connection);
                    GetEndCaption.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    GetEndCaption.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    GetEndCaption.Parameters["@Id"].Value = Id;


                    // Add the output parameter.
                    GetEndCaption.Parameters.Add(new SqlParameter("@EndCaption", SqlDbType.VarChar, 20));
                    GetEndCaption.Parameters["@EndCaption"].Direction = ParameterDirection.Output;


                    //run previously stored procedure
                    GetEndCaption.ExecuteNonQuery();

                    return GetEndCaption.Parameters["@EndCaption"].Value.ToString();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful edit." , E.Message);
                return "";
            }
        }
        public static void SetStartValue(int Id, int StartValue)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand SetStartValue = new SqlCommand("P_SetStartValue", Connection);
                    SetStartValue.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetStartValue.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    SetStartValue.Parameters["@Id"].Value = Id;
                    SetStartValue.Parameters.Add(new SqlParameter("@StartValue", SqlDbType.Int));
                    SetStartValue.Parameters["@StartValue"].Value = StartValue;


                    //run previously stored procedure
                    SetStartValue.ExecuteNonQuery();

                }
            }
            catch(Exception E)
            {
                Logger.WriteLog("Unsuccessful Save.",E.Message );

            }
        }
        public static void SetEndValue(int Id, int EndValue)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand SetEndValue = new SqlCommand("P_SetEndValue", Connection);
                    SetEndValue.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetEndValue.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    SetEndValue.Parameters["@Id"].Value = Id;
                    SetEndValue.Parameters.Add(new SqlParameter("@EndValue", SqlDbType.Int));
                    SetEndValue.Parameters["@EndValue"].Value = EndValue;


                    //run previously stored procedure
                    SetEndValue.ExecuteNonQuery();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful Save." , E.Message);

            }
        }
        public static void SetStartCaption(int Id, string StartCaption)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand SetStartCaption = new SqlCommand("P_SetStartCaption", Connection);
                    SetStartCaption.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetStartCaption.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    SetStartCaption.Parameters["@Id"].Value = Id;
                    SetStartCaption.Parameters.Add(new SqlParameter("@StartCaption", SqlDbType.VarChar,1000));
                    SetStartCaption.Parameters["@StartCaption"].Value = StartCaption;


                    //run previously stored procedure
                    SetStartCaption.ExecuteNonQuery();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful Save.",E.Message );

            }
        }
        public static void SetEndCaption(int Id, string EndCaption)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    Connection.Open();
                    SqlCommand SetEndCaption = new SqlCommand("P_SetEndCaption", Connection);
                    SetEndCaption.CommandType = CommandType.StoredProcedure;

                    // Add input parameter for the stored procedure and specify what to use as its value.
                    SetEndCaption.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
                    SetEndCaption.Parameters["@Id"].Value = Id;
                    SetEndCaption.Parameters.Add(new SqlParameter("@EndCaption", SqlDbType.VarChar, 1000));
                    SetEndCaption.Parameters["@EndCaption"].Value = EndCaption;


                    //run previously stored procedure
                    SetEndCaption.ExecuteNonQuery();

                }
            }
            catch (Exception E)
            {
                Logger.WriteLog("Unsuccessful Save." , E.Message);

            }
        }
    }
}


                           
