﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsResale.Business.Models
{
    public enum TicketStatuses : byte
    {
        selling,
        waiting,
        sold
    }
}
