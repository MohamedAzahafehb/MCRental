using MCRental_Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;

namespace MCRental_Client
{
    public partial class App : Application
    {
        static public ServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddDbContext<MCRentalDBContext>();

            services.AddIdentityCore<Gebruiker>();

            services.AddLogging();

            ServiceProvider = services.BuildServiceProvider();

            MCRentalDBContext context = new MCRentalDBContext();
            MCRentalDBContext.seeder(context);

            Windows.MainWindow mainWindow = new Windows.MainWindow(ServiceProvider.GetRequiredService<MCRentalDBContext>());
            mainWindow.Show();

            //    var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

            //MCRentalDBContext context = new MCRentalDBContext();
            //services.AddDbContext<MCRentalDBContext>();

            //    services.AddLogging();

            //    //services.AddIdentityCore<Gebruiker>(options =>
            //    //{
            //    //    options.Password.RequireDigit = false;
            //    //    options.Password.RequireLowercase = false;
            //    //    options.Password.RequireNonAlphanumeric = false;
            //    //    options.Password.RequireUppercase = false;
            //    //    options.Password.RequiredLength = 6;
            //    //    options.User.RequireUniqueEmail = true;
            //    //})
            //    //.AddEntityFrameworkStores<MCRentalDBContext>();


            //    serviceProvider = services.BuildServiceProvider();
            //    MainWindow mainWindow = new MainWindow(App.serviceProvider.GetRequiredService<MCRentalDBContext>());
            //mainWindow.Show();
        }
    }
}
