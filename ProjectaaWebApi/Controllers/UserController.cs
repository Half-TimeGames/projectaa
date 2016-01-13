using System.Web.Http;

namespace ProjectaaWebApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        [Route("{user}")]
        public void CreateUser(int id, string firstName, string lastName, string userName)
        {
            
        }
    }
}
