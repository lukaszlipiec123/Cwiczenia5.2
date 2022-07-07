using System;

namespace Cwiczenia5._2.Models.DTO
{
    public class ClientRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string PESEL { get; set; }
        public int IdTrip { get; set; }
        public string TripName { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
