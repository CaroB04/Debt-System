using BuildingMyFirstAPIOnion.Core.BaseModels;
using BuildingMyFirstAPIOnion.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingMyFirstAPIOnion.Models.Contexts
{
   public class BaseContext : DbContext
    {
        public BaseContext (DbContextOptions<BaseContext> contextOptions) : base (contextOptions)
        {

        }

        public DbSet<LoanEntity> Loans { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<LoanPayment> LoanPayment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<LoanEntity>()
                        .HasOne(loan => loan.Debtor)
                        .WithMany(user => user.LoansGiven)
                        .IsRequired()
                        .HasForeignKey(loan => loan.DebtorId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LoanEntity>()
                        .HasOne(loan => loan.Lender)
                        .WithMany(user => user.LoansTaken)
                        .IsRequired()
                        .HasForeignKey(loan => loan.LenderId)
                        .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);

            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IBaseEntity).IsAssignableFrom(type.ClrType))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }
        }
    }
}
