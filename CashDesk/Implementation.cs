using CashDesk.Data;
using CashDesk.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashDesk
{
    public class DataAccess : IDataAccess
    {
        private CashDeskDbContext context;
        
        public Task InitializeDatabaseAsync()
        {
            if (context == null)
            {
                context = new CashDeskDbContext();
            }
            else
            {
                throw new InvalidOperationException();
            }
            
            return Task.CompletedTask;
        }

        public async Task<int> AddMemberAsync(string firstName, string lastName, DateTime birthday)
        {
            if (context == null)
            {
                throw new InvalidOperationException();
            }
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Null Argument");
            }
            if (context.Members.First(member => member.LastName == lastName).Equals(lastName))
            {
                throw new DuplicateNameException("The lastname already exists!");
            }
            Member member = new Member { FirstName = firstName, LastName = lastName, Birthday = birthday };
            context.Members.Add(member);
            await context.SaveChangesAsync();
            return member.MemberNumber;
        }
            
        public Task DeleteMemberAsync(int memberNumber)
        {
            if (context == null)
            {
                throw new InvalidOperationException();
            }
            context.Members.Remove(context.Members.First(member => member.MemberNumber == memberNumber));
            return Task.CompletedTask;
        }


        public async Task<IMembership> JoinMemberAsync(int memberNumber)
        {
            if (context == null)
            {
                throw new InvalidOperationException();
            }
            Member member = await context.Members.FirstAsync(m => m.MemberNumber == memberNumber);
            Membership membership = new Membership { Member = member, Begin = DateTime.Now };
            context.Memberships.Add(membership);
            await context.SaveChangesAsync();
            return membership;
        }

     
        public async Task<IMembership> CancelMembershipAsync(int memberNumber)
        {
            if (context == null)
            {
                throw new InvalidOperationException();
            }
            Membership membership = context.Memberships.First(membership => membership.Member.MemberNumber == memberNumber);
            membership.End = DateTime.Now;
            context.Memberships.Update(membership);
            await context.SaveChangesAsync();
            return membership;
        }

        public async Task DepositAsync(int memberNumber, decimal amount)
        {
            if (context == null)
            {
                throw new InvalidOperationException();
            }
            if (amount < 0)
            {
                throw new ArgumentException();
            }

            Membership membership = context.Memberships.First(membership => membership.Member.MemberNumber == memberNumber);
            Deposit depo = new Deposit { Amount = amount, Membership = membership };
            context.Deposits.Add(depo);
            await context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public Task<IEnumerable<IDepositStatistics>> GetDepositStatisticsAsync()
        {
            if (context == null)
            {
                throw new InvalidOperationException();
            }
            return null;
        }

        public void Dispose() { }
    }
}
