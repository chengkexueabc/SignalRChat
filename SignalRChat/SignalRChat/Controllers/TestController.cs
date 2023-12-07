using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SignalRChat.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        List<SysUser> users = new List<SysUser>()
        {
            new SysUser(1,"王鹤棣","123456"),
            new SysUser(2,"吴磊","123456"),
            new SysUser(3,"赵露思","123456")
        };

        [HttpPost]
        public ActionResult Login(SysUser sysUser)
        {
            var user = users.Where(s => s.userName == sysUser.userName &&
                            s.userPwd == sysUser.userPwd).FirstOrDefault();

            if (user != null)
            {
                return Ok(user.userId);
            }

            return Ok("失败");
        }
    }

    public record SysUser(int? userId, string userName, string userPwd);
}
