using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam20050222
{
    internal class Stadion
    {
        public Sector SecA { get; set; }
        public Sector SecB { get; set; }
        public Sector SecC { get; set; }
        public Sector SecD { get; set; }
        public Sector SecE { get; set; }
        public Sector SecF { get; set; }
        public Stadion()
        {
            SecA = new Sector(Sectors.A);
            SecB = new Sector(Sectors.B);
            SecC = new Sector(Sectors.C);
            SecD = new Sector(Sectors.D);
            SecE = new Sector(Sectors.E);
            SecF = new Sector(Sectors.F);
        }

        public bool AddFunToSec(FootbalFun fan)
        {
            switch (fan.Sector)
            {
                case Sectors.A:
                    return SecA.AddFun(fan);
                    break;
                case Sectors.B:
                    return SecB.AddFun(fan);
                    break;
                case Sectors.C:
                    return SecC.AddFun(fan);
                    break;
                case Sectors.D:
                    return SecD.AddFun(fan);
                    break;
                case Sectors.E:
                    return SecE.AddFun(fan);
                    break;
                case Sectors.F:
                    return SecF.AddFun(fan);
                    break;
                default:
                    return false;
                    break;
            }
            
        }

        public string GetInfoFunsBySector(Sectors sec)
        {
            switch (sec)
            {
                case Sectors.A:
                    return SecA.getInfoAllFuns();
                    break;
                case Sectors.B:
                    return SecB.getInfoAllFuns();
                    break;
                case Sectors.C:
                    return SecC.getInfoAllFuns();
                    break;
                case Sectors.D:
                    return SecD.getInfoAllFuns();
                    break;
                case Sectors.E:
                    return SecE.getInfoAllFuns();
                    break;
                case Sectors.F:
                    return SecF.getInfoAllFuns();
                    break;
                default:
                    return null;
                    break;
            }
        }

        public int[] GetNumbersFunsBySector(Sectors sec)
        {
            switch (sec)
            {
                case Sectors.A:
                    return SecA.GetPlaces();
                    break;
                case Sectors.B:
                    return SecB.GetPlaces();
                    break;
                case Sectors.C:
                    return SecC.GetPlaces();
                    break;
                case Sectors.D:
                    return SecD.GetPlaces();
                    break;
                case Sectors.E:
                    return SecE.GetPlaces();
                    break;
                case Sectors.F:
                    return SecF.GetPlaces();
                    break;
                default:
                    return null;
                    break;
            }
        }
    }
}
