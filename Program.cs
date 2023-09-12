using PlayingCardService.CardDeck;
using PlayingCardService.EventFeed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// I have not written C# in 11 years, so I'm not sure if this is the correct way to do this.
// This scrutor plugin feels like overcomplicating simple things or I might be using it wrong.
builder.Services.Scan(
    scan =>
        scan.FromAssemblyOf<ICardDeck>()
            .AddClasses(classes => classes.AssignableTo<ICardDeck>())
            .AsImplementedInterfaces()
);

builder.Services.Scan(
    scan =>
        scan.FromAssemblyOf<IEventStore>()
            .AddClasses(classes => classes.AssignableTo<IEventStore>())
            .AsImplementedInterfaces()
);

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
