using MarkEmbling.PostcodesIO;
using Microsoft.AspNetCore.Mvc;
using PostcodeApi;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IPostcodesIOClient, PostcodesIOClient>();
builder.Services.AddSingleton<IPostcodeService, PostcodeService>();
builder.Services.AddOutputCache();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:3001")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseOutputCache();

app.MapGet("/Lookup/{postcode}", async ([FromRoute] string postcode, [FromServices] IPostcodeService service) =>
    {
        var result = await service.LookupAsync(postcode);
        return Results.Ok(result);
    })
.WithName("GetPostCodeInfo")
.WithOpenApi().CacheOutput();

app.MapGet("/AutoComplete/{postcode}", async ([FromRoute] string postcode, [FromServices] IPostcodeService service) =>
    {
        var result = await service.AutocompleteAsync(postcode);
        return Results.Ok(result);
    })
    .WithName("PostCodeAutoComplete")
    .WithOpenApi().CacheOutput();

app.Run();

