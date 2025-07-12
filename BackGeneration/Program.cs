var builder = WebApplication.CreateBuilder(args);

// Добавляем CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // Разрешаем запросы с фронта
              .AllowAnyMethod()                      // Разрешаем любые HTTP-методы (GET, POST, и т.д.)
              .AllowAnyHeader();                     // Разрешаем любые заголовки
    });
});

// Добавляем контроллеры
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Применяем CORS
app.UseCors("AllowAll");  // Включаем CORS до всех других мидлваров

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();