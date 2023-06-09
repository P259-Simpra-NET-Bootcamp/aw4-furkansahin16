using AutoMapper;
using Serilog;
using SimApi.Base;
using SimApi.Data;
using SimApi.Data.Uow;
using SimApi.Schema;

namespace SimApi.Operation;

public class TransactionReportService : ITransactionReportService
{

    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public TransactionReportService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public ApiResponse<List<TransactionViewResponse>> GetAll()
    {
        try
        {
            var transactions = unitOfWork.Repository<TransactionView>().GetAll();
            var mapped = mapper.Map<List<TransactionView>, List<TransactionViewResponse>>(transactions);
            return new ApiResponse<List<TransactionViewResponse>>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetAll Exception");
            return new ApiResponse<List<TransactionViewResponse>>(ex.Message);
        }
    }

    public ApiResponse<List<TransactionViewResponse>> GetByAccountId(int accountId)
    {
        try
        {
            var transactions = unitOfWork.Repository<TransactionView>().GetAll().Where(x => x.AccountId == accountId).ToList();
            var mapped = mapper.Map<List<TransactionView>, List<TransactionViewResponse>>(transactions);
            return new ApiResponse<List<TransactionViewResponse>>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetAll Exception");
            return new ApiResponse<List<TransactionViewResponse>>(ex.Message);
        }
    }

    public ApiResponse<List<TransactionViewResponse>> GetByCustomerId(int customerId)
    {
        try
        {
            var transactions = unitOfWork.Repository<TransactionView>().Where(x=>x.CustomerId== customerId).ToList();
            var mapped = mapper.Map<List<TransactionView>, List<TransactionViewResponse>>(transactions);
            return new ApiResponse<List<TransactionViewResponse>>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetAll Exception");
            return new ApiResponse<List<TransactionViewResponse>>(ex.Message);
        }
    }

    public ApiResponse<TransactionViewResponse> GetById(int id)
    {
        try
        {
            var transaction = unitOfWork.Repository<TransactionView>().GetById(id);
            var mapped = mapper.Map<TransactionView, TransactionViewResponse>(transaction);
            return new ApiResponse<TransactionViewResponse>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetAll Exception");
            return new ApiResponse<TransactionViewResponse>(ex.Message);
        }
    }

    public ApiResponse<List<TransactionViewResponse>> GetByReferenceNumber(string referenceNumber)
    {

        try
        {
            var transactions = unitOfWork.Repository<TransactionView>().Where(x=>x.ReferenceNumber==referenceNumber).ToList();
            var mapped = mapper.Map<List<TransactionView>, List<TransactionViewResponse>>(transactions);
            return new ApiResponse<List<TransactionViewResponse>>(mapped);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "GetAll Exception");
            return new ApiResponse<List<TransactionViewResponse>>(ex.Message);
        }
    }
}
