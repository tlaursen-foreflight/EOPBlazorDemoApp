using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public class DbService : IDbService
    {
        private DatabaseContext _context;

        public DbService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckDbAccess()
        {
            try
            {
                _ = await _context.Airports.AnyAsync().ConfigureAwait(false);
                return true;
            }
            catch (NpgsqlException e)
            {
                if (e.Message.StartsWith("No password has been provided") || e.Message.StartsWith("28P01: password authentication failed for user"))
                {
                    throw new NpgsqlException("Incorrect username or password.");
                }
            }
            catch (InvalidOperationException e)
            {
                if (e.InnerException.Message == "The operation has timed out.")
                {
                    throw new InvalidOperationException("Error likely due to missing vpn access.");
                }
            }
            catch (SocketException e)
            {
                if (e.Message == "No such host is known.")
                {
                    throw new InvalidOperationException("Error likely due to missing internet access.");
                }
            }

            return false;
        }

        public Task<List<EngineOutProcedure>> GetEops()
        {
            return _context.EngineOutProcedures.Include(eop => eop.Runway.Airport).ToListAsync();
        }

        public Task<List<Airport>> GetAirports()
        {
            return _context.Airports.ToListAsync();
        }

        public Task<List<Runway>> GetRunways()
        {
            return _context.Runways.ToListAsync();
        }
    }
}