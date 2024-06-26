using Microsoft.AspNetCore.Mvc;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("/{controller}/{action}")]
    public class TestController : ControllerBase
    {

        public object Data()
        {
            DateTime? DeletionTime = null; // new DateTime(2024,6,18);
            TimeSpan? BeginTime = new TimeSpan(8, 26,15);
            long? DeletorId = 20240626123456666L;

            var obj = new
            {
                OrderId = 20240626123456123L,
                CreationTime = DateTime.Now,
                DeletionTime = DeletionTime,
                DeletorId = DeletorId,
                BeginTime = BeginTime,
                UserId = Guid.NewGuid()
            };

            return obj;
        }

        public string Time()
        {
            return DateTime.Now.ToString();
        }
    }
}
