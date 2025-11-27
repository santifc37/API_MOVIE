using API_MOVIE.DAL;
using API_MOVIE.mapper;
using API_MOVIE.Repository;
using API_MOVIE.Repository.IRepository;
using API_MOVIE.Services;
using API_MOVIE.Services.IServices;
using Class_Programmation.DAL;
using Class_Programmation.Repository;
using Class_Programmation.Repository.iRepository;
using Class_Programmation.Services;
using Class_Programmation.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionMovie")));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionCategory")));
builder.Services.AddAutoMapper(x => x.AddProfile<Mappers>());


// Dependency injetion for services 
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IMovieService, MovieService>();

// Dependency injetion for repository 
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
