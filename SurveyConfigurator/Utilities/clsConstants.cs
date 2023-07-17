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
        //CONNECTION STRING
        

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
        public const string P_GET_TYPE = "P_GetType";

        public const string P_GET_TEXT = "P_GetText";
        public const string P_SET_TEXT = "P_SetText";

        public const string P_GET_NUMBER_OF_STARS = "P_GetNumberOfStars";
        public const string P_SET_NUMBER_OF_STARS = "P_SetNumberOfStars";

        public const string P_GET_NUMBER_OF_SMILEYS = "P_GetNumberOfSmileys";
        public const string P_SET_NUMBER_OF_SMILEYS = "P_SetNumberOfSmileys";

        public const string P_GET_START_CAPTION = "P_GetStartCaption";
        public const string P_SET_START_CAPTION = "P_SetStartCaption";
        public const string P_GET_END_CAPTION = "P_GetEndCaption";
        public const string P_SET_END_CAPTION = "P_SetEndCaption";

        public const string P_GET_START_VALUE = "P_GetStartValue";
        public const string P_SET_START_VALUE = "P_SetStartValue";
        public const string P_GET_END_VALUE = "P_GetEndValue";
        public const string P_SET_END_VALUE = "P_SetEndValue";

        //QUESTION ATTRIBUTES
        public const string ID = "Id";
        public const string TEXT = "Text";
        public const string TYPE = "Type";


        //SMILEY
        public const string NUMBER_OF_SMILEYS = "NumberOfSmileys";

        //STAR
        public const string NUMBER_OF_STARS = "NumberOfStars";

        //SLIDER
        public const string START_VALUE = "StartValue";
        public const string END_VALUE = "EndValue";
        public const string START_CAPTION = "StartCaption";
        public const string END_CAPTION = "EndCaption";


        public static string ErrorStrings(int Code)
        {
            if (Code == 1)
            {
                return "Success";
            }
            else if (Code == -2)
            {
                return "Question Type not Selected.";
            }
            else if (Code == -3)
            {
                return "Failed to connect to database.";
            }
            else if (Code == -4)
            {
                return "Question Text can't be empty.";
            }
            else if (Code == -5)
            {
                return "Failed To Add Question";
            }
            else if (Code == -6)
            {
                return "Failed To Delete Question";
            }
            else if (Code == -7)
            {
                return "Failed To Update Question";
            }
            else if (Code == -20)
            {
                return "Invalid Number of Smileys (2-5).";
            }
            else if (Code == -21)
            {
                return "Invalid Number of Stars (1-10).";
            }
            else if (Code == -22)
            {
                return "Invalid Start Value (>0).";
            }
            else if (Code == -23)
            {
                return "Invalid End Value (<100).";
            }
            else if (Code == -24)
            {
                return "Invalid End Value (Should be greater than start value).";
            }
            else if (Code == -25)
            {
                return "Start caption too long.";
            }
            else if (Code == -26)
            {
                return "End caption too long.";
            }
            else return "Not Found.";
        }
    }
}
