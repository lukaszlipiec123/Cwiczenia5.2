using System.Collections.Generic;
using System.Threading.Tasks;
using Cwiczenia5._2.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Cwiczenia5._2.Models.DTO;

namespace Cwiczenia5._2.DataAccessLayer
{
    public class TripsDataAccess : ITripsDataAccess
    {

        private readonly s20785Context _dbContext;

        public TripsDataAccess(s20785Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ClientTrips>> GetTripsAsync()
        {

            var trips = await _dbContext.Trips.Select(x => new ClientTrips
            {
                Name = x.Name,
                Description = x.Description,
                DateFrom = x.DateFrom,
                DateTo = x.DateTo,
                MaxPeople = x.MaxPeople,
                Countries = x.CountryTrips.Select(x => new CountryDTO
                {
                    Name = x.IdCountryNavigation.Name
                }),
                Clients = x.ClientTrips.Select(x => new ClientDTO
                {
                    FirstName = x.IdClientNavigation.FirstName,
                    LastName = x.IdClientNavigation.LastName,
                })
            }).OrderByDescending(x => x.DateFrom).ToListAsync();


            return trips;
        }

        public async Task<bool> DeleteClient(int IdClient)
        {
            var client = await _dbContext.Clients.FindAsync(IdClient);
            
            if (client == null)
            {
                throw new System.Exception("Klient nie znaleziony! nie można usunąć");
            }

            bool related = _dbContext.ClientTrips.Where(i => i.IdClient == IdClient).Any();

            if (related == true)
            {
                throw new System.Exception("Klient jest już powiązany! nie można usunąć");
            }

            _dbContext.Clients.Remove(client);
            _ = _dbContext.SaveChangesAsync();


            return true;

        }

        public async Task<bool> AssociateClientToTrip(ClientRequest clientRequest)
        {
            var client = await _dbContext.Clients.Where(c =>
                c.Pesel.Equals(clientRequest.PESEL)
            ).FirstOrDefaultAsync();


            if (client == null)
            {
                // Client doesn't exist so we create new one
                var newClient = new Client
                {
                    FirstName = clientRequest.FirstName,
                    LastName = clientRequest.LastName,
                    Email = clientRequest.Email,
                    Telephone = clientRequest.Telephone,
                    Pesel = clientRequest.PESEL
                };
                await _dbContext.AddAsync(newClient);
                await _dbContext.SaveChangesAsync();
            }

            var trip = await _dbContext.Trips.FindAsync(clientRequest.IdTrip);

            if (trip == null)
            {
                throw new System.Exception("Wycieczka nie znaleziona! nie można przypisać klienta do nie istniejącej wycieczki");
            }

            var clientTrip = await _dbContext.ClientTrips.Where(c =>
                c.IdTrip == trip.IdTrip
                &
                c.IdClient == client.IdClient
            ).FirstOrDefaultAsync();

            if (clientTrip != null)
            {
                throw new System.Exception("Dana wycieczka jest już przypisana do klienta");
            }

            clientTrip = new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = trip.IdTrip,
                PaymentDate = clientRequest.PaymentDate,
                RegisteredAt = System.DateTime.Now
            };

            await _dbContext.AddAsync(clientTrip);
            await _dbContext.SaveChangesAsync();

            return true;
        }

    }
}
