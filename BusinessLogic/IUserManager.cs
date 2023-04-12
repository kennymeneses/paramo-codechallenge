using Models;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IUserManager
    {
        Task<Result> CreateUser(User user);
    }
}
