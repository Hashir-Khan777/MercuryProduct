using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class AuditLog
    {
        public List<AuditLogModel> logs = new List<AuditLogModel>();

        [Inject]
        public LogService LogService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            GetLogs();
        }

        public void GetLogs()
        {
            logs = LogService.GetLogs().ToList();
        }
    }
}
