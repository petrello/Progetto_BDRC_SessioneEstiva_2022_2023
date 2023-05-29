using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Infrastructure.Authentication
{
    public class CertSettings
    {
        public const string SectionName = "CertSettings";
        public string Location { get; set; } = null!;
    }
}
