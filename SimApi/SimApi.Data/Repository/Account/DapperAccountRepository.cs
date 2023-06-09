using SimApi.Data.Context;

namespace SimApi.Data.Repository;

public class DapperAccountRepository : DapperRepository<Account>, IDapperAccountRepository
{

    public DapperAccountRepository(SimDapperDbContext context) : base(context) { }
}
