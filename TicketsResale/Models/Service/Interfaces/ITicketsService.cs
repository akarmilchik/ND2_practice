﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsResale.Business.Models;

namespace TicketsResale.Models.Service
{
    public interface ITicketsService
    {
        Task AddTicketToDb(Ticket item);
        Task<Ticket> GetTicketById(int id);
        Task<List<Ticket>> GetTickets();
        Task<List<Ticket>> GetTicketsByStatusesAndUserName(TicketStatuses ticketStatus, OrderStatuses orderStatus, string userName);
        Task<List<Ticket>> GetTicketsByUserId(string UserId);
        Task UpdTicketToDb(Ticket item);
    }
}
