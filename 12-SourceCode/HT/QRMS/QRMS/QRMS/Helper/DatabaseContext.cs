using Microsoft.EntityFrameworkCore;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRMS.Helper
{
    public class DatabaseContext : DbContext
    {
        public DbSet<LanguageModel> languageModels { get; set; }
        public DbSet<SettingModel> settingModels { get; set; }
        public DbSet<AccountLogModel> accountLogModels { get; set; }
        public DbSet<CartedContractModelLocal> cartedContractModelLocals { get; set; }

        public DatabaseContext()
        {
            try
            {
                this.Database.EnsureCreated();
            }
            catch(Exception ex)
            {

            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                var dbPath = DependencyService.Get<IDBPath>().GetDBPath();
                var dbcontext = optionsBuilder.UseSqlite($"Filename={dbPath}");

            }
            catch (Exception ex)
            {

            }
        }
    }
}
