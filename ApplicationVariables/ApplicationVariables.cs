using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationVariables
{
    public class ApplicationVariables
    {
            public ApplicationVariables()
            { }

            public struct CsvPaths
            {
                public static string MoviesCSV = @"C:\Users\joeco_000\Documents\Visual Studio 2017\Projects\NovusMovieProject\WebMovies\ExtendedTestData.csv"; // name of the path to the data used for the project.
                //public static string MoviesCSV = @"C:\Users\Recor\Documents\Visual Studio 2015\Projects\NovusMovieProject\WebMovies\ExtendedTestData.csv";
            }

            public struct SystemSettings
            {
                public struct Cache
                {
                    public static bool UseCache = true;
                    public static string FilmCacheName = @"Cache_Film";
                }
            }

            public struct SystemValues // the different items being shown on the webpage. it holds the button, drop down lists default view and the other dropdown lists, where they get their data from.
            {
                public struct Buttons
                { 
                    public static string BtnResetID_ToLower = "btnreset"; // resets the button.
                }

                public struct DropDownLists //default view of the drop down lists
                {
                    public static string DefaultValue = @"NOT SELECTED"; // no values are shown as nothing has been selected.
                    public static string DefaultText = @"<----- SELECT ----->"; // Text shown in the drop down lists.
                    public static bool UseBlankItem = true; // if the drop down list is blank.

                    public struct Films // the Film drop downlist. the id, the names that display in the field and the ID it has associated with it are held here.
                    {
                        public static string ControlID = @"DropDownListFilms";
                        public static string DataTextField = @"FilmName";
                        public static string DataValueField = @"FilmID";
                    }

                    public struct Directors // The Directors drop downlist. the id, the names that display in the field and the ID it has associated with it are held here.
                {
                        public static string ControlID = @"DropDownListDirectors";
                        public static string DataTextField = @"PersonName";
                        public static string DataValueField = @"PersonID";
                    }

                    public struct Actors // The Actors drop downlist. the id, the names that display in the field and the ID it has associated with it are held here.
                {
                        public static string ControlID = @"DropDownListActors";
                        public static string DataTextField = @"PersonName";
                        public static string DataValueField = @"PersonID";
                    }
                }
            }

            public struct DataIDs  // the Id's given to the diffrent areas of data we have. in the ExtendedTest Data.csv file the ID represents where it is placed in the "table".
            {
                public struct CsvItems_Movies
                {
                    public static int FilmID = 0;
                    public static int FilmName = 1;
                    public static int ImdbRating = 2;
                    public static int FilmYear = 7;
                    public static int DirectorID = 3;
                    public static int DirectorName = 4;
                    public static int ActorID = 5;
                    public static int ActorName = 6;
                }
            }
        }

}
