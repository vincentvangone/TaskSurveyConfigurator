using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class clsConstants
    {

        //QUESTION TYPES
        public const string SMILEY = "Smiley";
        public const string STAR = "Star";
        public const string SLIDER = "Slider";


        //LOG TYPES
        public const string ERROR = "Error";
        public const string INFORMATION = "Information";
        public const string WARNING = "Warning";


        //PROCEDURE NAMES
        public const string P_VIEW = "P_View";
        public const string P_DELETE = "P_Delete";
        public const string P_INSERT = "P_Insert";
        public const string P_EDIT = "P_Edit";
        public const string P_GET_TYPE = "P_GetType";

        public const string P_GET_TEXT = "P_GetText";
        public const string P_EDIT_TEXT = "P_EditText";

        public const string P_GET_NUMBER_OF_STARS = "P_GetNumberOfStars";
        public const string P_EDIT_QUESTION_STARS = "P_EditQuestionStar";

        public const string P_GET_NUMBER_OF_SMILEYS = "P_GetNumberOfSmileys";
        public const string P_EDIT_QUESTION_SMILEY = "P_EditQuestionSmiley";
        public const string P_GET_STARS_QUESTION = "P_GetStarsQuestion";
        public const string P_GET_SMILEY_QUESTION = "P_GetSmileyQuestion";
        public const string P_GET_STAR_QUESTION = "P_GetStarQuestion";
        public const string P_GET_SLIDER_QUESTION = "P_GetSliderQuestion";

        public const string P_GET_START_CAPTION = "P_GetStartCaption";
        public const string P_GET_END_CAPTION = "P_GetEndCaption";
        public const string P_GET_START_VALUE = "P_GetStartValue";
        public const string P_GET_END_VALUE = "P_GetEndValue";
        public const string P_EDIT_SLIDER = "P_EditQuestionSlider";

        public const string P_VIEW_QUESTIONS_SMILEY = "P_ViewQuestionsSmiley";

        //QUESTION ATTRIBUTES
        public const string ID = "Id";
        public const string TEXT = "Text";
        public const string TYPE = "Type";
        public const string ORDER = "Order";

        //MERGED
        public const string PROPERTIES = "Properties";


        //SMILEY
        public const string NUMBER_OF_SMILEYS = "NumberOfSmileys";

        //STAR
        public const string NUMBER_OF_STARS = "NumberOfStars";

        //SLIDER
        public const string START_VALUE = "StartValue";
        public const string END_VALUE = "EndValue";
        public const string START_CAPTION = "StartCaption";
        public const string END_CAPTION = "EndCaption";


        //ERROR CODES
        public const int SUCCESS = 1;
        public const int TYPE_NOT_SELECTED = -2;
        public const int FAILED_DATABASE_CONNECTION = -3;
        public const int TEXT_NOT_SPECIFIED = -4;
        public const int FAILED_NEW_QUESTION = -5;
        public const int FAILED_DELETE_QUESTION = -6;
        public const int FAILED_EDIT_QUESTION = -7;
        public const int INVALID_QUESTION_ORDER = -8;

        public const int INVALID_NUMBER_OF_SMILEYS = -20;
        public const int INVALID_NUMBER_OF_STARS = -21;
        public const int INVALID_START_VALUE = -22;
        public const int INVALID_END_VALUE = -23;
        public const int INVALID_END_LESS_THAN_START_VALUE = -24;
        public const int INVALID_START_CAPTION = -25;
        public const int INVALID_END_CAPTION = -26;
        public const int UNKNOWN_ERROR = -27;

        //ERROR MESSAGES
        public const string SUCCESS_STRING = "Success";
        public const string VIEW_FAILED_STRING = "View Question Procedure Failed.";
        public const string TYPE_NOT_SELECTED_STRING = "Question Type not Selected.";
        public const string FAILED_DATABASE_CONNECTION_STRING = "Failed To Connect to Database.";
        public const string TEXT_NOT_SPECIFIED_STRING = "Question Text can't be empty.";
        public const string FAILED_NEW_QUESTION_STRING = "Failed To Add Question";
        public const string FAILED_DELETE_QUESTION_STRING = "Failed To Delete Question";
        public const string FAILED_EDIT_QUESTION_STRING = "Failed To Edit Question";
        public const string INVALID_QUESTION_ORDER_STRING = "Question Order must be positive and greater than zero.";

        public const string INVALID_NUMBER_OF_SMILEYS_STRING = "Invalid Number of Smileys (2-5).";
        public const string INVALID_NUMBER_OF_STARS_STRING = "Invalid Number of Stars (1-10).";
        public const string INVALID_START_VALUE_STRING = "Invalid Start Value (>0).";
        public const string INVALID_END_VALUE_STRING = "Invalid End Value (<100).";
        public const string INVALID_END_LESS_THAN_START_VALUE_STRING = "Invalid End Value (Should be greater than start value).";
        public const string INVALID_START_CAPTION_STRING = "Start caption too long.";
        public const string INVALID_END_CAPTION_STRING = "End caption too long.";

        public const string EMPTY_SERVER_STRING = "SQL Server can't be empty.";
        public const string EMPTY_DATABASE_STRING = "SQL Database can't be empty.";

        public const string DELETE_QUESTION_CONFIRM_STRING="Are you sure you want to delete question ";

    }
}
