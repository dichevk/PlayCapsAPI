using Microsoft.EntityFrameworkCore;
using PlayCapsViewer;
using PlayCapsViewer.Data;
using PlayCapsViewer.Interfaces;
using PlayCapsViewer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
//wire up automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//register the Dependency Injection
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IPlayCapService, PlayCapService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewerService, ReviewerService>();

//add the db seed 
builder.Services.AddTransient<DbSeed>();

//bring over the db specified in the DataContext.cs file
builder.Services.AddDbContext<DataContext>(options =>
{
    // connect to the db via the connection string 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}); 

var app = builder.Build();

//call seeding
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

//seed app connection
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DbSeed>();
        service.SeedDataContext();
    }
}

// Configure the HTTP request pipeline. Middleware section that includes authorization, https redirection and mapping 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
