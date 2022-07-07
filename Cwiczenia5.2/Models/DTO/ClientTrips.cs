﻿using System;
using System.Collections.Generic;

namespace Cwiczenia5._2.Models.DTO
{
    public class ClientTrips
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }
        public IEnumerable<ClientDTO> Clients { get; set; }
        public IEnumerable<CountryDTO> Countries { get; set; } 
    }
}
