using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam20050222
{
    internal class PeopleGenerator
    {
        Random rnd = new Random();
        public int[] SecAPlases { get; set; }
        public int[] SecBPlases { get; set; }
        public int[] SecCPlases { get; set; }
        public int[] SecDPlases { get; set; }
        public int[] SecEPlases { get; set; }
        public int[] SecFPlases { get; set; }
        public string[] Names { get; set; }

        public PeopleGenerator()
        {
            Names = new string[500];
            var tsk = Task.Run(ReadNames);
            this.SecAPlases = new int[500];
            this.SecBPlases = new int[500];
            this.SecCPlases = new int[500];
            this.SecDPlases = new int[500];
            this.SecEPlases = new int[500];
            this.SecFPlases = new int[500];
            for (int i = 0; i < 500; i++)
            {
                SecAPlases[i] = i;
                SecBPlases[i] = i;
                SecCPlases[i] = i;
                SecDPlases[i] = i;
                SecEPlases[i] = i;
                SecFPlases[i] = i;
            }
            tsk.Wait();
            tsk.Dispose();
        }

        private void ReadNames()
        {
            string[] arrS;
            using (StreamReader read = new StreamReader("Names.txt"))
            {
                arrS = read.ReadToEndAsync().Result.Split('\n');
            }
            for (int i = 0; i < 500; i++)
            {
                Names[i] = arrS[i];
            }
        }
        public void AddNewFun()
        {
            for (int j = 0; j < 10; j++)
            {
                int Sect = rnd.Next(1, 8);
                int place = -1;
                int i = rnd.Next(0, 499);
                switch (Sect)
                {
                    case 1:
                        while (place == -1)
                        {
                            if (SecAPlases[i] != -1)
                            {
                                place = SecAPlases[i];
                                SecAPlases[i] = -1;
                                break;
                            }
                            else
                            {
                                if (i != 499) i++;
                                else i = 0;
                            }
                        }
                        CroudOutside.funs.Add(new FootbalFun(Sectors.A, true, place, Names[rnd.Next(0, 499)]));
                        break;
                    case 2:
                        while (place == -1)
                        {
                            if (SecBPlases[i] != -1)
                            {
                                place = SecBPlases[i];
                                SecBPlases[i] = -1;
                                break;
                            }
                            else
                            {
                                if (i != 499) i++;
                                else i = 0;
                            }
                        }
                        CroudOutside.funs.Add(new FootbalFun(Sectors.B, true, place, Names[rnd.Next(0, 499)]));
                        break;
                    case 3:
                        while (place == -1)
                        {
                            if (SecCPlases[i] != -1)
                            {
                                place = SecCPlases[i];
                                SecCPlases[i] = -1;
                                break;
                            }
                            else
                            {
                                if (i != 499) i++;
                                else i = 0;
                            }
                        }
                        CroudOutside.funs.Add(new FootbalFun(Sectors.C, true, place, Names[rnd.Next(0, 499)]));
                        break;
                    case 4:
                        while (place == -1)
                        {
                            if (SecDPlases[i] != -1)
                            {
                                place = SecDPlases[i];
                                SecDPlases[i] = -1;
                                break;
                            }
                            else
                            {
                                if (i != 499) i++;
                                else i = 0;
                            }
                        }
                        CroudOutside.funs.Add(new FootbalFun(Sectors.D, true, place, Names[rnd.Next(0, 499)]));
                        break;
                    case 5:
                        while (place == -1)
                        {
                            if (SecEPlases[i] != -1)
                            {
                                place = SecEPlases[i];
                                SecEPlases[i] = -1;
                                break;
                            }
                            else
                            {
                                if (i != 499) i++;
                                else i = 0;
                            }
                        }
                        CroudOutside.funs.Add(new FootbalFun(Sectors.E, true, place, Names[rnd.Next(0, 499)]));
                        break;
                    case 6:
                        while (place == -1)
                        {
                            if (SecFPlases[i] != -1)
                            {
                                place = SecFPlases[i];
                                SecFPlases[i] = -1;
                                break;
                            }
                            else
                            {
                                if (i != 499) i++;
                                else i = 0;
                            }
                        }
                        CroudOutside.funs.Add(new FootbalFun(Sectors.F, true, place, Names[rnd.Next(0, 499)]));
                        break;
                    case 7:
                        CroudOutside.funs.Add(new FootbalFun(Sectors.B, false, rnd.Next(0, 500), Names[rnd.Next(0, 499)]));
                        break;
                }
            }
        }
    }
}