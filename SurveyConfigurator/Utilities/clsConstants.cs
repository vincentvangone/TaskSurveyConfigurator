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
        public const string P_GET_TYPE = "P_GetType";

        public const string P_GET_TEXT = "P_GetText";
        public const string P_EDIT_TEXT = "P_EditText";

        public const string P_GET_NUMBER_OF_STARS = "P_GetNumberOfStars";
        public const string P_EDIT_QUESTION_STARS = "P_EditQuestionStar";

        public const string P_GET_NUMBER_OF_SMILEYS = "P_GetNumberOfSmileys";
        public const string P_EDIT_QUESTION_SMILEY = "P_EditQuestionSmiley";


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

        public const int INVALID_NUMBER_OF_SMILEYS = -20;
        public const int INVALID_NUMBER_OF_STARS = -21;
        public const int INVALID_START_VALUE = -22;
        public const int INVALID_END_VALUE = -23;
        public const int INVALID_END_LESS_THAN_START_VALUE = -24;
        public const int INVALID_START_CAPTION = -25;
        public const int INVALID_END_CAPTION = -26;

    }
}
