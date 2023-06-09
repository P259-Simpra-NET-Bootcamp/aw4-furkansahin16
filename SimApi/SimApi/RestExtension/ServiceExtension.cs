using SimApi.Operation;
using SimApi.Operation.Category;
using SimApi.Service.CustomService;

namespace SimApi.Service.RestExtension;

public static class ServiceExtension
{
    public static void AddServiceExtension(this IServiceCollection services)
    {
        services.AddScoped<IUserLogService, UserLogService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ITransactionService, TransactionService>();

        services.AddScoped<ITransactionReportService, TransactionReportService>();
        services.AddScoped<IDapperAccountService, DapperAccountService>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<ScopedService>();
        services.AddTransient<TransientService>();
        services.AddSingleton<SingletonService>();
    }
}
