﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.DTO
{
    public  class TicketDetailsDto: TicketDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
    }
}
