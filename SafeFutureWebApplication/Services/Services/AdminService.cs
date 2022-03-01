using SafeFutureWebApplication.Repository;
using SafeFutureWebApplication.Models;
using SafeFutureWebApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Ganss.XSS;
using Microsoft.EntityFrameworkCore;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace SafeFutureWebApplication.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext context;
        private readonly IAuthService authService;

        public AdminService(AppDbContext context, IAuthService authService)
        {
            this.context = context;
            this.authService = authService;
        }

        public IEnumerable<User> GetUsers() => context.Users.AsNoTracking().ToList();

        public User GetUserById(Guid id) => context.Users.AsNoTracking().FirstOrDefault(x => x.UserId == id);

        public byte[] GetReport(DateTime from = default, DateTime to = default)
        {
            IQueryable<Attendance> query = context.Attendances
                .Include(x => x.Recipient)
                .AsQueryable();

            if (from != DateTime.MinValue && to != DateTime.MinValue)
            {
                query = query.Where(x => x.EventDate >= from && x.EventDate <= to);
            }

            IEnumerable<object> result = query.ToList().Select(x => new
            {
                x.AttendanceId,
                x.EventDate,
                x.Recipient.FirstName,
                x.Recipient.MiddleName,
                x.Recipient.LastName,
                x.Recipient.ZipCode,
                x.Recipient.HouseholdSize,
            });
   
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(result);
                ms.Flush();
            }
            return ms.ToArray();
        }

        // COLT TESTING FOR REPORT GENERATION
        public byte[] ColtReportTesting()
        {
            IEnumerable<Recipient> recipients = context.Recipients.ToList();
            IEnumerable<Attendance> attendances = context.Attendances.ToList();
            MemoryStream ms = new MemoryStream();

            using var writer = new StreamWriter(ms);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(attendances);

            ms.Flush();

            return ms.ToArray();
        }

        public bool CreateUser(User user, string requester)
        {
            user.Salt = authService.GetSalt();
            user.Password = authService.HashPassword(user.Password, user.Salt);
            
            try
            {
                context.Add(user);
                context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool UpdateUser(User update)
        {
            User existing = context.Users.FirstOrDefault(x => x.UserId == update.UserId);

            if (existing == null) { return false; }

            existing.Username = SanitizeText(update.Username);
            existing.Role = update.Role;

            try
            {
                context.Update(existing);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(Guid id)
        {
            User user = context.Users.FirstOrDefault(x => x.UserId == id);

            if (user == null) { return false; }

            try
            {
                context.Remove(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Private
        private string SanitizeText(string value)
        {
            HtmlSanitizer sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(value);
        }
        #endregion

    }
}
