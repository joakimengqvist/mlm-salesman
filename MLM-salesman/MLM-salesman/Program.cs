using MLM_salesman.ServiceConnector;

var builder = WebApplication.CreateBuilder(args);

var localCorsSettingsName = "AllowLocalhost5173";

builder.Services.AddCors(options =>
{
    options.AddPolicy(localCorsSettingsName, policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddSingleton<RecruitmentConnector>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(localCorsSettingsName);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();