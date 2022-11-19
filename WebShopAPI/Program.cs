using LibData;
using LibData.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using WebShopAPI.Mapper;
using WebShopAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppEFContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    options.Stores.MaxLengthForKeys = 128;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<AppEFContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors();

builder.Services.AddAutoMapper(typeof(AppMapProfile));

builder.Services.AddControllers();

//������� ���� ������, ��� ������� ���� ���������. � ���� �� ��������� ����� �������� �� ���� ������
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

var signinKey =
    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtKey")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = signinKey,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
builder.Services.AddSwaggerGen(c =>
{
    var fileDoc = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");
    c.IncludeXmlComments(fileDoc);
});

var app = builder.Build();

app.UseCors(x => x.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");

if (!Directory.Exists(dir))
    Directory.CreateDirectory(dir);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(dir),
    RequestPath = "/images"
});


app.UseAuthentication(); //��������������
app.UseAuthorization();  //�����������

app.MapControllers();

app.SeedData();

app.Run();
