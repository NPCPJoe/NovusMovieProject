using System;
using System.Linq;
using System.Collections.Generic;

namespace MovieClassLayer
{
    public class MovieClasses
    {
        // ------------------------------------------------------------------------------------------------------------------- FILMS
        public class Films : List<Film>  // inheriting from the List film 
        {
            //--------------------------------------------------------------------- CONSTRUCTORS
            public Films() { }

            public Films(List<Film> films)
            {
                this.AddRange(films);
            }

            //--------------------------------------------------------------------- METHODS

            //----------------------------------------------------------- FILMS
            public Films GetFilmsFilteredSubset(string filmID, string directorID, string actorID) //LINQ filtering  of ID
            {
                var tmpFilms = this.Where(f => f.FilmID == ((filmID == null) ? f.FilmID : filmID)) // called tmpFilms when it is called.
                                    .Where(f => f.Directors.Any(p => p.PersonID == ((directorID == null) ? p.PersonID : directorID))) // these three Where statements is where the films information is pulled from.
                                    .Where(f => f.Actors.Any(p => p.PersonID == ((actorID == null) ? p.PersonID : actorID )))
                                    .OrderBy(f => f.FilmName) // the films are ordered by name alphabetically.
                                    .ThenBy(f => f.FilmID) //  then it is ordered by the ID if the names are similar or the same. it is the second most important ordering.
                                    .ToList(); // puts the data into a list format to be put in the drop down list.
                return new Films(tmpFilms);
            }

            public List<SimplisticFilm> ToListSimplisticFilm() 
            {
                //return this.Select(f => f.GetSimplisticFilm()).OrderBy(f => f.FilmName)
                //                                                .ThenBy(f => f.FilmID)
                //                                                .ToList();

                return this.ToList<SimplisticFilm>().OrderBy(s => s.FilmName)
                                                    .ThenBy(s => s.FilmID)
                                                    .ToList();
            }

            public List<SimplisticFilm> GetDistinctSimplisticFilm(string filmID) // pulls filmid.
            {
                return this.Select(f => f.GetSimplisticFilm()).Where(f => f.FilmID == filmID)
                                                                .ToList();
            }

            //----------------------------------------------------------- DIRECTORS
            public List<Director> ToListDistinctDirector()
            {
                return this.SelectMany(p => p.Directors, (parent, child) => (Director)(child.GetPerson())).GroupBy(p => p.PersonID)
                                                                                                            .Select(grp => grp.First())
                                                                                                            .OrderBy(p => p.PersonName)
                                                                                                            .ThenBy(p => p.PersonID)
                                                                                                            .ToList();
            }

            public List<Director> GetDistinctDirector(string directorID)
            {
                return this.SelectMany(p => p.Directors, (parent, child) => (Director)(child.GetPerson())).Where(p => p.PersonID == directorID)
                                                                                                            .GroupBy(p => p.PersonID)
                                                                                                            .Select(grp => grp.First())
                                                                                                            .ToList();            
            }

            //----------------------------------------------------------- ACTORS
            public List<Actor> ToListDistinctActor()
            {
                return this.SelectMany(p => p.Actors, (parent, child) => (Actor)(child.GetPerson())).GroupBy(p => p.PersonID)
                                                                                                        .Select(grp => grp.First())
                                                                                                        .OrderBy(p => p.PersonName)
                                                                                                        .ThenBy(p => p.PersonID)
                                                                                                        .ToList();
            }

            public List<Actor> GetDistinctActor(string actorID)
            {
                return this.SelectMany(p => p.Actors, (parent, child) => (Actor)(child.GetPerson())).Where(p => p.PersonID == actorID)
                                                                                                        .GroupBy(p => p.PersonID)
                                                                                                        .Select(grp => grp.First())
                                                                                                        .ToList();
            }
        }

        // ------------------------------------------------------------------------------------------------------------------- FILM
        public class Film : SimplisticFilm // film inherits the two bits of data which is film name and film id. Film has more infromation in it. it gets information from the Imdb rating, film year and from the lists director and actor.
        {
            public string ImdbRating { get; set; }
            public string FilmYear { get; set; }
            public List<Director> Directors { get; set; }
            public List<Actor> Actors { get; set; }

            public Film()
            {
                this.Directors = new List<Director>();
                this.Actors = new List<Actor>();
            }

            //------------------------------------------ CONSTRUCTORS
            public Film(string filmID, string filmName, string imdbRating, string filmYear) : base(filmID, filmName) // same as the one below without actor and direction? Could be removed?
            {
                this.ImdbRating = imdbRating;
                this.FilmYear = filmYear;
                this.Directors = new List<Director>();
                this.Actors = new List<Actor>();
            }

            public Film(string filmID, string filmName, string imdbRating, List<Director> directors, List<Actor> actors, string filmYear) : base(filmID, filmName)
            { // constructor above declares it takes data from the areas above.
                this.ImdbRating = imdbRating;
                this.FilmYear = filmYear;
                this.Directors = directors;
                this.Actors = actors;
            }

            //------------------------------------------ METHODS

            public SimplisticFilm GetSimplisticFilm() // simply returns the inforamtion from Simplistic Film.
            {
                return (SimplisticFilm)this;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------- SIMPLISTIC FILM
        public class SimplisticFilm // simplisticFilm holds two bits of information about the film its name and ID.
        {
            public string FilmID { get; set; }
            public string FilmName { get; set; }

            //------------------------------------------ CONSTRUCTORS
            public SimplisticFilm() { }
            public SimplisticFilm(string filmID, string filmName) // the constructor holds the film id and filmname so they can be called when they need to.
            {
                this.FilmID = filmID;
                this.FilmName = filmName;
            }

            //------------------------------------------ METHODS

            public bool IsValid() // checks if film name and film id are null will return an expection if it is not valid.
            {
                //-- valid if has ID and Name
                return (!(string.IsNullOrEmpty(this.FilmID)) && !(string.IsNullOrEmpty(this.FilmName)));
            }
        }

        // ------------------------------------------------------------------------------------------------------------------- DIRECTOR
        public class Director : Person // inherits person which has basic person information and is able to return that info.
        {
            //------------------------------------------ CONSTRUCTORS
            public Director() { }
            public Director(string personID, string personName) : base(personID, personName)
            { }
        }

        // ------------------------------------------------------------------------------------------------------------------- ACTOR
        public class Actor : Person // inherits person which has basic person information and is able to return that info.
        {
            //------------------------------------------ CONSTRUCTORS
            public Actor() { }
            public Actor(string personID, string personName) : base(personID, personName)
            { }
        }

        // ------------------------------------------------------------------------------------------------------------------- PERSON
        public class Person // holds basic inforamtion of all types of people their name and ID
        { 
            public string PersonID { get; set; }
            public string PersonName { get; set; }

            //------------------------------------------ CONSTRUCTORS
            public Person() { } // decalres that person() hold personID and person Name.
            public Person(string personID, string personName)
            {
                this.PersonID = personID;
                this.PersonName = personName;
            }

            //------------------------------------------ METHODS
            public Person GetPerson() // returns the persons information.
            {
                return (Person)this;
            }
        }
    }
}
