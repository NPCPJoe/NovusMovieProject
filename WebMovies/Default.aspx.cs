using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using av = ApplicationVariables.ApplicationVariables; // short hand for application variables
using cache = ApplicationVariables.ApplicationVariables.SystemSettings.Cache;
using ddl = ApplicationVariables.ApplicationVariables.SystemValues.DropDownLists;
using mbl = MovieBusinessLayer.MovieBusinessLayer;
using mcl = MovieClassLayer.MovieClasses;


namespace WebMovies
{
    public partial class Default : SharedBase // inherits the sharedbase class which holds populating drop down lists and blanks drop down lists. 
    {
        protected void Page_Load(object sender, EventArgs e) // this class loads the page.
        {
            //var tmp = Page.Request.Params["__EVENTTARGET"];

            if (this.IsPostBack && isFilteredPageLoad()) // checks if it is a postback (certain credentials of the page are to be checked against some sources)
            {
                using (mbl bl1 = new mbl()) // calling the movieBuisness layer.
                {
                    string filmID = (DropDownListFilms.SelectedValue == av.SystemValues.DropDownLists.DefaultValue ? null : DropDownListFilms.SelectedValue);// asking if the dropdownlist selected value is equal to the default value. if it is null then it will select the selected value. //populate the dropdown with the film selected??
                    string directorID = (DropDownListDirectors.SelectedValue == av.SystemValues.DropDownLists.DefaultValue ? null : DropDownListDirectors.SelectedValue);
                    string actorID = (DropDownListActors.SelectedValue == av.SystemValues.DropDownLists.DefaultValue ? null : DropDownListActors.SelectedValue);

                    populateDropDownsWithFilteredData(filmID, directorID, actorID);
                }
            }
            else
            {
                populateDropDownsWithOriginalData();
            }
        }

        private bool isFilteredPageLoad()
        { 
            return (Page.Request.Params["__EVENTTARGET"].ToLower() != av.SystemValues.Buttons.BtnResetID_ToLower);
        }

        private void populateDropDowns(bool addBlankItem, List<mcl.SimplisticFilm> sFilms
                                                        , List<mcl.Director> directors
                                                        , List<mcl.Actor> actors)
        {
             populateDropDownList(true, ddl.Films.ControlID // pulls the information from the application Variable class so the dropdown can be populated.
                                                        , sFilms
                                                        , ddl.Films.DataTextField
                                                        , ddl.Films.DataValueField);
            populateDropDownList(true, ddl.Directors.ControlID // pulls the information from the application Variable class so the dropdown can be populated.
                                                        , directors
                                                        , ddl.Directors.DataTextField
                                                        , ddl.Directors.DataValueField);
            populateDropDownList(true, ddl.Actors.ControlID // pulls the information from the application Variable class so the dropdown can be populated.
                                                        , actors
                                                        , ddl.Actors.DataTextField
                                                        , ddl.Actors.DataValueField);
        }

        private mcl.Films getFilms() //
        {
            mcl.Films films = new mcl.Films();

            if ((cache.UseCache) && (Cache[cache.FilmCacheName] != null))
            {
                films = Cache[cache.FilmCacheName] as mcl.Films;
            }
            else
            {
                using (mbl bl1 = new mbl())
                {
                    films = bl1.GetFilms(av.CsvPaths.MoviesCSV);
                    if (cache.UseCache) Cache[cache.FilmCacheName] = films;
                }
            }

            return films;
        }

        private void populateDropDownsWithOriginalData() // returns the original data selected.
        {
            using (mbl bl1 = new mbl())
            {
                mcl.Films films = getFilms();

                List<mcl.Director> directors = bl1.GetDistinctDirectorsFromFilms(films);
                List<mcl.Actor> actors = bl1.GetDistinctActorsFromFilms(films);
                List<mcl.SimplisticFilm> sFilms = bl1.GetDistinctSimplisticFilmsFromFilms(films);

                populateDropDowns(ddl.UseBlankItem, sFilms, directors, actors);
            }
        }

        private void populateDropDownsWithFilteredData(string filmID, string directorID, string actorID) //populates the drop down list with the filtered data.
        {
            mcl.Films films = getFilms();
            using (mbl bl1 = new mbl())
            {
                mcl.Films tmp = bl1.GetFilmsSubset(filmID, directorID, actorID, films);

                List<mcl.Actor> actors = (actorID == null) ? bl1.GetDistinctActorsFromFilms(tmp) : bl1.GetDistinctActor(tmp, actorID);
                List<mcl.Director> directors = (directorID == null) ? bl1.GetDistinctDirectorsFromFilms(tmp) : bl1.GetDistinctDirector(tmp, directorID);
                List<mcl.SimplisticFilm> sFilms = (filmID == null) ? bl1.GetDistinctSimplisticFilmsFromFilms(tmp) : tmp.GetDistinctSimplisticFilm(filmID);

                populateDropDowns(ddl.UseBlankItem, sFilms, directors, actors);
            }
        }

        private bool isSelectionComplete()
        {
            return false;
        }

        private void selectionComplete(mcl.SimplisticFilm simplisticFilm)
        {
            //-- TODO: if directors/actors/sFilms all have count of 1 then show details
        }

        //--------------------------------------------------------------------- EVENTS

        protected void btnReset_Click(object sender, EventArgs e) // when the button called reset is clicked it will populate the data with the original data which is select. 
        {
            populateDropDownsWithOriginalData();
        }




    }
}