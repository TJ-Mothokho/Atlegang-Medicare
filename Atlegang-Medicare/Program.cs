using DataAccesslayer.Repository.Administrator;
using DataAccesslayer.Repository.Consumable_And_Script_Manager;
using DataAccesslayer.Repository.Doctor;
using DataAccesslayer.Repository.Nurse_and_Sister;
using DataAccesslayer.Repository.Ward_Administrator;
using DataAccesslayer.Repository.Ward_Aministrator;
using DataAccesslayer.Services;
using DataAccesslayer.SqlDataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    //IdleTimeout will log the user out after a certain time.
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

//Administrator Services
builder.Services.AddTransient<IAdministratorDashboardCardsRepository, AdministratorDashboardCardsRepository>();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<ISuburbRepository, SuburbRepository>();
builder.Services.AddTransient<ICityRepository, CityRepository>();
builder.Services.AddTransient<IBedRepository, BedRepository>();
builder.Services.AddTransient<IRoomRepository, RoomRepository>();
builder.Services.AddTransient<IWardRepository, WardRepository>();
builder.Services.AddTransient<IBusinessInformationRepository, BusinessInformationRepository>();
//builder.Services.AddTransient<IBankInformationRepository, BankInformationRepository>();


//Doctor Services
builder.Services.AddTransient<IDoctorDashboardCardsRepository, DoctorDashboardCardsRepository>();
builder.Services.AddTransient<IPatientRepository, PatientRepository>();
builder.Services.AddTransient<IVisitRepository, VisitRepository>();

//Consumable Manager Services
builder.Services.AddTransient<IConsumableRepository, ConsumableRepository>();
builder.Services.AddTransient<IMedicationRepository, MedicationRepository>();
builder.Services.AddTransient<IStockRepository, StockRepository>();
builder.Services.AddTransient<ISupplierRepository, SupplierRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();

//Script Services
builder.Services.AddTransient<IScriptDetailRepository, ScriptDetailRepository>();
builder.Services.AddTransient<IScriptRepository, ScriptRepository>();

//Ward Administrator Services
builder.Services.AddTransient<IPatientFileRepository, PatientFileRepository>();
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<IAllergyRepository, AllergyRepository>();
builder.Services.AddTransient<IChronicChonditionRepository, ChronicConditionRepository>();
builder.Services.AddTransient<IPatientAllergyRepository, PatientAllergyRepository>();
builder.Services.AddTransient<IPatientConditionRepository, PatientConditionRepository>();
builder.Services.AddTransient<IPatientMovementRepository, PatientMovementRepository>();
builder.Services.AddTransient<IWardAdministratorDashboardCardsRepository, WardAdministratorDashboardCardsRepository>();
builder.Services.AddTransient<IPatientMedicationRepository, PatientMedicationRepository>();


//Nurse
builder.Services.AddTransient<INurseDashboardCardsRepository, NurseDashboardCardsRepository>();
builder.Services.AddTransient<IVitalRepository, VitalRepository>();
builder.Services.AddTransient<INoteRepository, NoteRepository>();
builder.Services.AddTransient<ITreatmentRepository, TreatmentRepository>();
builder.Services.AddTransient<IPatientVitalRepository, PatientVitalRepository>();
builder.Services.AddTransient<IAdministerMedsRepository, AdministerMedsRepository>();
builder.Services.AddTransient<IPatientTreatmentRepository, PatientTreatmentRepository>();
builder.Services.AddTransient<INurseRepository, NurseRepository>();
builder.Services.AddTransient<IPatientVitalDetailsRepository, PatientVitalDetailsRepository>();


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

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=LandingPage}/{id?}");

app.Run();
