using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task3.Data;
using Task3.Formatters;
using Task3.Middlewares;
using Task3.Repository.Abstract;
using Task3.Repository.Concrete;
using Task3.Services;
using Task3.Services.Abstract;
using Task3.Services.Concrete;

namespace Task3;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(options =>
        {
            options.OutputFormatters.Add(new BookVCardOutputFormatter());
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connection = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<Task3DbContext>(options => options.UseSqlServer(connection));

        builder.Services.AddAutoMapper(typeof(Program).Assembly);
        builder.Services.AddTransient<ICalculateService, CalculateService>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IBookService, BookService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
