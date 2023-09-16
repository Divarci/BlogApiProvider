using CoreLayer.Response;
using RepositoryLayer.Extension;
using ServiceLayer.Exceptions.Filters;
using ServiceLayer.Exceptions.Middleware;
using ServiceLayer.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new ValidationFilterAttribute());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.LoadRepositoryLayerExtensions(builder.Configuration);
builder.Services.LoadServiceLayerExtensions(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStatusCodePages(async statusCodeContext =>
{
    switch (statusCodeContext.HttpContext.Response.StatusCode)
    {
        case 401:
            statusCodeContext.HttpContext.Response.StatusCode = 401;
            await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(new CustomResponseDto<NoContentDto> { StatusCode = 401, Errors = new List<string> { "Unauthorized Access" } });
            break;
        case 403:
            statusCodeContext.HttpContext.Response.StatusCode = 403;
            await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(new CustomResponseDto<NoContentDto> { StatusCode = 403, Errors = new List<string> { "Permission Needed" } });
            break;
        case 404:
            statusCodeContext.HttpContext.Response.StatusCode = 404;
            await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(new CustomResponseDto<NoContentDto> { StatusCode = 404, Errors = new List<string> { "Page Not Found" } });
            break;
        default:
            statusCodeContext.HttpContext.Response.StatusCode = 500;
            await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(new CustomResponseDto<NoContentDto> { StatusCode = 500, Errors = new List<string> { "Unexpected system error. Consult your engineer." } });
            break;

    }
});


app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
