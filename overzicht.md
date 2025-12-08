# Overzicht van geleende code

---

## Cursus / Agendarepo
1. Alles van de map Models is gebaseerd op de Agendarepo:
   1. de DBContext, inclusief het gebruiken van UserSecrets
   ```csharp
   public class MCRentalDBContext : IdentityDbContext<Gebruiker>{
    public DbSet<Stad> Steden { get; set; }
    public DbSet<Filiaal> Filialen { get; set; }
    public DbSet<Gebruiker> Gebruikers { get; set; }
    public DbSet<Auto> Autos { get; set; }
    public DbSet<Reservatie> Reservaties { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddUserSecrets<MCRentalDBContext>(optional: true)
                    .Build();

                var connStr = config.GetConnectionString("DefaultConnection");
            try
            {
                optionsBuilder.UseSqlServer(connStr);
            }
            catch (Exception ex)
            {
                throw new Exception("Er is een fout opgetreden bij het configureren van de database: " + ex.Message);
            }
        }
        }
        
    

    public static void seeder(MCRentalDBContext context)
    {
        try
        {
            if (true)
            {
                context.Steden.AddRange(Stad.seedingData());
                context.SaveChanges();
                seedSteden(context);
            }
            if (!context.Filialen.Any())
            {
                context.Filialen.AddRange(Filiaal.seedingData());
                context.SaveChanges();
            }
            Gebruiker.Seeder();
            context.SaveChanges();
            if (!context.Autos.Any())
            {
                context.Autos.AddRange(Auto.seedingData());
                context.SaveChanges();
            }
            if (!context.Reservaties.Any())
            {
                context.Reservaties.AddRange(Reservatie.seedingData());
                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Er is een fout opgetreden bij het seeden van de Database: " + ex.Message);
        }
    }
    }
   ```
   2. het aanmaken van de objecten: Stad, Filiaal, Reservatie, Auto. het gebruiken van de juiste Annotations
        ```csharp
        public class Stad {
        public int Id { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Postcode { get; set; }
        public ICollection<Filiaal>? Filialen { get; set; }
        public ICollection<Gebruiker>? Gebruikers { get; set; }
        }
        ```
      1. Object Gebruiker is ook volledig gebaseerd op de voorbeeldapplicatie, vooral de seedingmethode in de klasse
        ```csharp
        public class Gebruiker : IdentityUser {
            //... alle attributen
            public static Gebruiker dummy = new Gebruiker
            {
                Id = "1",
                Voornaam = "Dummy",
                Achternaam = "Dumpfries",
                GeboorteDatum = new DateTime(1999, 1, 1),
                Adres = "Dummystraat 1",
                StadId = 1,
                UserName = "Dummy",
                NormalizedUserName = "DUMMY",
                Email = "Dummy@mcrental.be",
                EmailConfirmed = true,
                PhoneNumber = "0000000000"
            };
            public static async Task Seeder()
            {
                MCRentalDBContext context = new MCRentalDBContext();

                // Voeg de nodige rollen toe
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new List<IdentityRole>
                    {
                        new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
                        new IdentityRole { Id = "Klant", Name = "Klant", NormalizedName = "KLANT" }
                    });
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Add(dummy);
                    context.SaveChanges();
                    Gebruiker hans = new Gebruiker
                    {
                        Voornaam = "Hans",
                        Achternaam = "De Beer",
                        GeboorteDatum = new DateTime(1990, 6, 15),
                        Adres = "Berenstraat 5",
                        StadId = 2,
                        UserName = "hansb",
                        Email = "hans.debeer@mcrental.be",
                        EmailConfirmed = true,
                        PhoneNumber = "0123456789"
                    };
                    Console.WriteLine("Creating users...");
                    Gebruiker admin = new Gebruiker
                    {
                        Voornaam = "Admin",
                        Achternaam = "Istrator",
                        GeboorteDatum = new DateTime(1985, 3, 20),
                        Adres = "Adminlaan 10",
                        StadId = 4,
                        UserName = "admin",
                        Email = "admin@mcrental.be",
                        EmailConfirmed = true,
                        PhoneNumber = "0987654321"
                    };
                    Gebruiker grietje = new Gebruiker
                    {
                        Voornaam = "Grietje",
                        Achternaam = "Peeters",
                        GeboorteDatum = new DateTime(1995, 11, 30),
                        Adres = "Peeterstraat 8",
                        StadId = 3,
                        UserName = "grietjep",
                        Email = "grietjep@hotmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "0112233445"
                    };
                    Gebruiker jans = new Gebruiker
                    {
                        Voornaam = "JanS",
                        Achternaam = "Smet",
                        GeboorteDatum = new DateTime(1978, 9, 5),
                        Adres = "Smetlaan 12",
                        StadId = 2,
                        UserName = "janss",
                        Email = "janss@hotmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "0223344556"
                    };

                    // Seeding halverwege mislukt, niet alle gebruikers aanwezig + geen rollen toegewezen

                    UserManager<Gebruiker> userManager = new UserManager<Gebruiker>(
                        new UserStore<Gebruiker>(context),
                        null, new PasswordHasher<Gebruiker>(),
                        null, null, null, null, null, null);

                    await userManager.CreateAsync(hans, "12345678");
                    await userManager.CreateAsync(admin, "12345678");
                    await userManager.CreateAsync(grietje, "12345678");
                    await userManager.CreateAsync(jans, "12345678");

                    while (context.Users.Count() < 4)
                    {
                        await Task.Delay(100);
                    }

                    await userManager.AddToRoleAsync(hans, "Klant");
                    await userManager.AddToRoleAsync(admin, "Admin");
                    await userManager.AddToRoleAsync(grietje, "Klant");
                    await userManager.AddToRoleAsync(jans, "Klant");
                }

                dummy = context.Users.First(u => u.UserName == "Dummy");
            }
        }
        ```
2. Ik heb eerst voor alle db tabellen een pagina gemaakt waarin ik alle data per tabel in een DataGrid steek zoals we in de les hebben gezien
   1. in AutoOverzichtPage.xaml
   ```html
   <DataGrid x:Name="dgAutos" Grid.Row="1" Margin="8" AutoGenerateColumns="False">
        <DataGrid.Columns>
            <DataGridTextColumn Header="Merk" Binding="{Binding Merk}" Width="100"/>
            <DataGridTextColumn Header="Model" Binding="{Binding Model}" Width="200"/>
            <DataGridTextColumn Header="Nummerplaat" Binding="{Binding Nummerplaat}" Width="200"/>
            <DataGridTextColumn Header="DagPrijs" Binding="{Binding DagPrijs}" Width="200"/>
            <DataGridTextColumn Header="Beschikbaar" Binding="{Binding Beschikbaar}" Width="200"/>
            <DataGridTextColumn Header="Type" Binding="{Binding type}" Width="200"/>
            <DataGridTextColumn Header="FiliaalId" Binding="{Binding FiliaalId}" Width="200"/>
            <DataGridTemplateColumn Header="Acties" Width="100">
                <DataGridTemplateColumn.CellTemplate>
                    <DataTemplate>
                        <Button Content="Reserveer" Click="ReserveerButton_Click" IsEnabled="True"/>
                    </DataTemplate>
                </DataGridTemplateColumn.CellTemplate>
            </DataGridTemplateColumn>
        </DataGrid.Columns>
    </DataGrid>
   ```
   ```csharp
   autos = (from auto in _context.Autos
                       select new MCRental_Models.Auto
                       {
                           Id = auto.Id,
                           Merk = auto.Merk,
                           Model = auto.Model,
                           Nummerplaat = auto.Nummerplaat,
                           DagPrijs = auto.DagPrijs,
                           Beschikbaar = auto.Beschikbaar,
                           type = auto.type,
                           FiliaalId = auto.FiliaalId
                       })
                       .Where(a => a.FiliaalId == this.filiaal.Id && !gereserveerdeAutoIds.Contains(a.Id))
                       .OrderBy(a => a.DagPrijs)
                       .ThenBy(a => a.Merk)
                       .ThenBy(a => a.Model)
                       .ToList();

    dgAutos.ItemsSource = autos;
   ```
3. het aanpassen van de rollen van de gebruiker
   ```html
        <StackPanel Grid.Row="1" Grid.Column="1" Orientation="Vertical">
            <ListBox x:Name="lbRollen" SelectionChanged="lbRollen_SelectionChanged"/>
        </StackPanel>
   ```
   ```csharp
   List<string> userRoles = (from ur in _context.UserRoles
                          where ur.UserId == _gebruiker.Id
                          select ur.RoleId)
                         .ToList();
    foreach (IdentityRole role in _context.Roles)
    {
        bool isSelected = userRoles.Contains(role.Id);
        roles.Add(new ListBoxItem { Content = role.Id, IsSelected = isSelected });
    }
    lbRollen.ItemsSource = roles;
    lbRollen.Visibility = Visibility.Visible;

    private async void lbRollen_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            foreach (ListBoxItem item in lbRollen.Items)
            {
                string role = item.Content.ToString();
                if (lbRollen.SelectedItems.Contains(item))
                    await _userManager.AddToRoleAsync(_gebruiker, role);
                else
                    await _userManager.RemoveFromRoleAsync(_gebruiker, role);
            }
        } catch(Exception ex)
        {
            MessageBox.Show($"Er is een fout opgetreden bij het bijwerken van de rollen: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
   ```
4. LogIn window en Registreer window volledig
5. de App.xaml.cs ook volledig
   ```csharp
   public partial class App : Application
    {
        static public ServiceProvider ServiceProvider { get; private set; }
        static public Gebruiker Gebruiker { get; set; }
        static public Windows.MainWindow MainWindow { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                var services = new ServiceCollection();

                services.AddDbContext<MCRentalDBContext>();

                services.AddIdentityCore<Gebruiker>(
                    options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 6;
                        options.User.RequireUniqueEmail = true;
                    })
                    .AddEntityFrameworkStores<MCRentalDBContext>();

                services.AddLogging();

                ServiceProvider = services.BuildServiceProvider();

                MCRentalDBContext context = new MCRentalDBContext();
                MCRentalDBContext.seeder(context);

                //App.Gebruiker = Gebruiker.dummy;

                MainWindow = new Windows.MainWindow(ServiceProvider.GetRequiredService<MCRentalDBContext>());
                MainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fout bij opstarten applicatie", MessageBoxButton.OK, MessageBoxImage.Error);
                Current.Shutdown();
            }
        }
    }
```
---

## Chatgpt / Copilot
1. bij Auto overzicht
   1. het filteren en sorteren van de datagrid
   ```csharp
   private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        string selectedSort = (cmbSort.SelectedItem as ComboBoxItem).Content as string;
        switch (selectedSort)
        {
            case "Prijs oplopend":
                dgAutos.ItemsSource = autos.OrderBy(a => a.DagPrijs).ThenBy(a => a.Merk).ThenBy(a => a.Model).ToList();
                break;
            case "Prijs aflopend":
                dgAutos.ItemsSource = autos.OrderByDescending(a => a.DagPrijs).ThenBy(a => a.Merk).ThenBy(a => a.Model).ToList();
                break;
            case "Merk A-Z":
                dgAutos.ItemsSource = autos.OrderBy(a => a.Merk).ThenBy(a => a.Model).ThenBy(a => a.DagPrijs).ToList();
                break;
            case "Merk Z-A":
                dgAutos.ItemsSource = autos.OrderByDescending(a => a.Merk).ThenBy(a => a.Model).ThenBy(a => a.DagPrijs).ToList();
                break;
            default:
                dgAutos.ItemsSource = autos;
                break;
        }
    }

    private void TypeCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        var checkBox = sender as CheckBox;
        string type = checkBox.Tag.ToString();

        if (checkBox.IsChecked == true)
        {
            if (!selectedTypes.Contains(type))
                selectedTypes.Add(type);
        }
        else
        {
            selectedTypes.Remove(type);
        }

        // Filter autos
        if (selectedTypes.Count == 0)
        {
            dgAutos.ItemsSource = autos;
            cmbTypes.Text = "Alle types";
        }
        else
        {
            dgAutos.ItemsSource = autos
                .Where(a => selectedTypes.Contains(a.type))
                .ToList();

            cmbTypes.Text = $"{selectedTypes.Count} geselecteerd";
        }
    }
   ```
2. validatie met een if-statement
   ```csharp
    if (string.IsNullOrWhiteSpace(txtMerk.Text) ||
            string.IsNullOrWhiteSpace(txtModel.Text) ||
            string.IsNullOrWhiteSpace(txtNummerplaat.Text) ||
            string.IsNullOrWhiteSpace(txtDagprijs.Text) ||
            string.IsNullOrWhiteSpace(txtType.Text))
        {
            throw new Exception("Vul alle verplichte velden in.");
        }
   ```
3. een window closen vanuit een pagina in AutoOverzichtPage
   ```csharp
    Window.GetWindow(this).Close();
   ```
4. de geklikte object in een datagrid ophalen en meegeven met een nieuw window
   ```csharp
   Filiaal filiaal = (sender as Button).DataContext as Filiaal;
    new FilaalDetailWin(filiaal, _context).ShowDialog();
   ```
5. heel de Pagina ProefielPagina
6. bij het openen van een window, direct de gewenste pagina weergeven
   ```csharp
   var window = new AutoDetailWin();
    window.Show();
    window.frmMain.Navigate(new AutoReservatiePage(reservatie.Auto, _context, reservatie, this));
    ```
7. 
---

## Externe bronnen

1. API-reqest uitvoeren voor het seeden van de tabel Stad
   ```csharp
   private static async void seedSteden(MCRentalDBContext context)
    {
        var client = new HttpClient();
        int limit = 100;
        int offset = 0;
        bool nogData = true;
        List<Stad> steden = new List<Stad>();

        while (nogData)
        {
            string url = $"https://www.odwb.be/api/explore/v2.1/catalog/datasets/postal-codes-belgium/records?select=post_code%2C%20sub_municipality_name_dutch%2C%20sub_municipality_name_french&where=sub_municipality_name_dutch%20is%20not%20null%20or%20sub_municipality_name_french%20is%20not%20null&order_by=post_code&limit={limit}&offset={offset}";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(json);
            var records = doc.RootElement.GetProperty("results");

            if (records.GetArrayLength() == 0)
            {
                nogData = false;
                break;
            }

            foreach (var record in records.EnumerateArray())
            {
                string dutch = record.GetProperty("sub_municipality_name_dutch").GetString();
                string french = record.GetProperty("sub_municipality_name_french").GetString();
                string postC = record.GetProperty("post_code").ToString();

                string cityName = !string.IsNullOrEmpty(dutch) ? dutch :
                        !string.IsNullOrEmpty(french) ? french : null;

                if (!string.IsNullOrEmpty(cityName))
                {
                    var stad = new Stad
                    {
                        Naam = cityName,
                        Postcode = postC
                    };
                    context.Steden.Add(stad);
                }
            }
            context.SaveChanges();
            offset += limit;
        }
    }
```

---

##