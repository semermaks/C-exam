using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam20050222
{
    internal class Sector
    {
        public Sectors SectorLetter { get; }
        public FootbalFun[] funs { get; private set; }
        public Sector(Sectors sector)
        {
            this.SectorLetter = sector;
            funs = new FootbalFun[500];
        }

        public bool DellFun(int place)
        {
            foreach (var fun in funs)
            {
                if (fun.Place == place)
                {
                    if (funs[place] != null)
                    {
                        funs[place] = null;
                        return true;
                    }
                    else return false;
                }
            }
            return false;
        }

        public bool AddFun(FootbalFun fun)
        {
            if (fun.Place != -1)
            {
                funs[fun.Place] = fun;
                return true;
            }
            else return false;
        }

        public string getInfoAllFuns()
        {
            string str = "";
            foreach (var fun in funs)
            {
                if (fun != null)
                {
                    str += $"{fun.Name}, {fun.Place}\n";
                }
            }
            return str;
        }

        public bool IsNull(int place)
        {
            foreach (var fun in funs)
            {
                if (fun != null) return true;

            }
            return false;
        }

        public int[] GetPlaces()
        {
            int[] arr = new int[500];
            for (int i = 0; i < 500; i++)
            {
                if (funs[i] != null) arr[i] = funs[i].Place;
                else arr[i] = -1;
            }
            return arr;
        }
    }
}
