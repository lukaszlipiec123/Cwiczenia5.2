using System.Collections.Generic;
using System.Threading.Tasks;
using Cwiczenia5._2.Models;
using Cwiczenia5._2.Models.DTO;

namespace Cwiczenia5._2.DataAccessLayer
{
    public interface ITripsDataAccess
    {
        public Task<IEnumerable<ClientTrips>> GetTripsAsync();
        public Task<bool> DeleteClient(int IdClient);
        public Task<bool> AssociateClientToTrip(ClientRequest clientRequest);
    }
}
