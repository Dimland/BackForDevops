var builder = WebApplication.CreateBuilder(args);

// ��������� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // ��������� ������� � ������
              .AllowAnyMethod()                      // ��������� ����� HTTP-������ (GET, POST, � �.�.)
              .AllowAnyHeader();                     // ��������� ����� ���������
    });
});

// ��������� �����������
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ��������� CORS
app.UseCors("AllowAll");  // �������� CORS �� ���� ������ ���������

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();