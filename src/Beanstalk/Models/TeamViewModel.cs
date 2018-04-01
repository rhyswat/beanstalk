using System;
using System.Collections.Generic;
using Beanstalk.Db.Models;

namespace Beanstalk.Models
{
    public class TeamCollectionViewModel
    {
        public DateTime Timestamp { get; set; }
        public string LastUpdated => Timestamp.ToString();

        private List<TeamViewModel> _teams = new List<TeamViewModel>();

        public IEnumerable<TeamViewModel> Teams
        {
            get { return _teams; }
            set
            {
                _teams.Clear();
                if (value != null) _teams.AddRange(value);
            }
        }

    }
    public class TeamViewModel
    {
        public string Name { get; set; }
        public string Stadium { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public int Drawn { get; set; }

        public int Played => Won + Lost + Drawn;
        public int Points => 3 * Won + Drawn;

        public TeamViewModel()
        {
        }

        public TeamViewModel(Team t) : this()
        {
            Name = t.Name;
            Stadium = t.Stadium;
            Won = t.Won;
            Drawn = t.Drawn;
            Lost = t.Lost;
        }
    }
}