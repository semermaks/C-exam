using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam20050222
{
    public static class CroudOutside
    {
        public static List<FootbalFun> funs { get; set; } = new List<FootbalFun>();
        public static CancellationToken cansel = new CancellationToken();

    }
}
