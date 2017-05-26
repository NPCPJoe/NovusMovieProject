using LumenWorks.Framework.IO.Csv;
using System;
using System.IO;
using System.Linq;
using csvMovies = ApplicationVariables.ApplicationVariables.DataIDs.CsvItems_Movies;
using mcl = MovieClassLayer.MovieClasses;

namespace MovieDataLayer
{
    public class MovieDataLayer : IDisposable
    {
        public void Dispose() // Disposes anything that has been used and is no longer needed. it frees up memory and processing power for us.
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }

        //--------------------------------------------------------------------- CSV
        public mcl.Films GetCsvData(string CsvPath)
        {
            mcl.Films films = new mcl.Films();

            //-- FilmID = csv[0];
            //-- FilmName = csv[1];
            //-- ImdbRating = csv[2];
            //-- FilmYear = csv[7];
            //-- DirectorID = csv[3];
            //-- DirectorName = csv[4];
            //-- ActorID = csv[5];
            //-- ActorName = csv[6];

            using (CsvReader csv = new CsvReader(new StreamReader(CsvPath), true)) // looks up where the data is being held.
            {
                int fieldCount = csv.FieldCount;

                string[] headers = csv.GetFieldHeaders();
                while (csv.ReadNextRecord()) // while reading the data. This if else statement is checking if any of the dropdown fields have been selected. if one has been selected it pulls the data from that csv file and populates the drop down.
                {
                    if (films.Any(item => item.FilmID == csv[csvMovies.FilmID]))
                    {
                        mcl.Film tmpFilm = films.Find(item => item.FilmID == csv[csvMovies.FilmID]);
                        if (tmpFilm.Directors.Any(item => item.PersonID == csv[csvMovies.DirectorID]))
                        { }
                        else
                        {
                            mcl.Director director = getDirectorFromCSV(csv);
                            tmpFilm.Directors.Add(director);
                        }
                        if (tmpFilm.Actors.Any(item => item.PersonID == csv[csvMovies.ActorID]))
                        { }
                        else
                        {
                            mcl.Actor actor = getActorFromCSV(csv);
                            tmpFilm.Actors.Add(actor);
                        }
                    }
                    else
                    {
                        mcl.Film film = getFilmFromCSV(csv);
                        films.Add(film);
                    }
                }
            }
            return films; // returns the results of the if, else statements above.
        }

        private mcl.Director getDirectorFromCSV(CsvReader csv) // gets information from the director csv. it is used in the while loop if a director has been selected.
        {
            mcl.Director director = new mcl.Director(csv[csvMovies.DirectorID]
                                                    , csv[csvMovies.DirectorName]);
            return director;
        }

        private mcl.Actor getActorFromCSV(CsvReader csv) // gets information from the Actor csv. it is used in the while loop if a director has been selected.
        {
            mcl.Actor actor = new mcl.Actor(csv[csvMovies.ActorID]
                                            , csv[csvMovies.ActorName]);
            return actor;
        }

        private mcl.Film getFilmFromCSV(CsvReader csv) // gets information from the film csv. it is used in the while loop if a director has been selected.
        {
            mcl.Director director = getDirectorFromCSV(csv);
            mcl.Actor actor = getActorFromCSV(csv);
            mcl.Film film = new mcl.Film(csv[csvMovies.FilmID]
                                        , csv[csvMovies.FilmName]
                                        , csv[csvMovies.ImdbRating]
                                        , csv[csvMovies.FilmYear]);
            film.Directors.Add(director); // if a film has been selected it also adds a director and an actor associated with that film.
            film.Actors.Add(actor);
            return film;
        }
    }
}
