using NetLink.Services;
using NetLink.Session;
using NetLink.Statistics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//netlink builder services
builder.Services
    .AuthenticateWithDevToken("NLz6f8yHWmjwWBeEAmiSOLdEQg1LLcsWEtopKQk1fMBkAr5S1tzaRCLTsMbuIbp7iibxZcGRLJqa6zERP7g")
    .AddEndUsers()
    .AddStatisticsServices()
    .AddSensorServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();