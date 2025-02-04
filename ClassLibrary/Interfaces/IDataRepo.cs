using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interfaces
{
    public interface IDataRepo
    {
        Task<ISet<Match>> GetAllMatchData(string gender);
        Task<ISet<Match>> GetMatchDataByCountry(string gender, string country);
        Task<ISet<Result>> GetAllNationalTeamData(string gender);
    }
}
