using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Exam20050222
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Grid SecA = new Grid();
        Grid SecB = new Grid();
        Grid SecC = new Grid();
        Grid SecD = new Grid();
        Grid SecE = new Grid();
        Grid SecF = new Grid();

        List<FootbalFun> funsIndors = new List<FootbalFun>();
        List<FootbalFun> funsIndorsBack = new List<FootbalFun>();

        PeopleGenerator peopleGenerator = new PeopleGenerator();
        Stadion stadion = new Stadion();

        List<Task> taskAddCroud = new List<Task>();
        List<Task> taskCroudHasBilet = new List<Task>();
        List<Task> taskCroudToStadion = new List<Task>();

        int pause = 0;
        CancelEventArgs cancel = new CancelEventArgs();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            CreateGrids();
        }
        public void CreateGrids()
        {
            SecA.HorizontalAlignment = HorizontalAlignment.Left;
            SecA.VerticalAlignment = VerticalAlignment.Top;

            SecB.HorizontalAlignment = HorizontalAlignment.Left;
            SecB.VerticalAlignment = VerticalAlignment.Bottom;


            SecC.HorizontalAlignment = HorizontalAlignment.Center;
            SecC.VerticalAlignment = VerticalAlignment.Top;

            SecD.HorizontalAlignment = HorizontalAlignment.Center;
            SecD.VerticalAlignment = VerticalAlignment.Bottom;

            SecE.HorizontalAlignment = HorizontalAlignment.Right;
            SecE.VerticalAlignment = VerticalAlignment.Top;


            SecF.HorizontalAlignment = HorizontalAlignment.Right;
            SecF.VerticalAlignment = VerticalAlignment.Bottom;


            Stadium.Children.Add(SecA);
            Stadium.Children.Add(SecB);
            Stadium.Children.Add(SecC);
            Stadium.Children.Add(SecD);
            Stadium.Children.Add(SecE);
            Stadium.Children.Add(SecF);


            foreach (Grid el in Stadium.Children)
            {
                el.Background = Brushes.LightYellow;
                el.Width = 200;
                el.Height = 225;
                for (int i = 0; i < 50; i++)
                {
                    el.RowDefinitions.Add(new RowDefinition());
                }
                for (int i = 0; i < 10; i++)
                {
                    el.ColumnDefinitions.Add(new ColumnDefinition());
                }
                for (int i = 0; i < 50; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        var brush = new SolidColorBrush();
                        brush.Color = Colors.Gray;
                        brush.Opacity = 0.5;
                        Rectangle re = new Rectangle()
                        {
                            Fill = brush
                        };
                        Grid.SetColumn(re, j);
                        Grid.SetRow(re, i);
                        el.Children.Add(re);
                    }
                }
            }

        }

        public void StartCroud()
        {
            taskCroudHasBilet = new List<Task>();
            taskCroudToStadion = new List<Task>();
            Task.Run(peopleGenerator.AddNewFun);
            for (int i = 0; i < 4; i++)
            {
                taskCroudHasBilet.Add(new Task(CroudFilterHasBilet, CroudOutside.cansel));
            }
            foreach (var task in taskCroudHasBilet)
            {
                task.RunSynchronously();
            }

            taskCroudHasBilet[0].Wait();
            for (int i = 0; i < 8; i++)
            {
                taskCroudToStadion.Add(new Task(CroudToStadion, CroudOutside.cansel));
            }
            foreach (var task in taskCroudToStadion)
            {
                task.RunSynchronously();
            }
            foreach (var tsk in taskCroudHasBilet)
            {
                tsk.Wait();
                tsk.Dispose();
            }
            foreach (var tsk in taskCroudToStadion)
            {
                tsk.Wait();
                tsk.Dispose();
            }
        }
        public void StopCroud()
        {
            CroudOutside.cansel.ThrowIfCancellationRequested();
            taskAddCroud[0].Dispose();
            foreach (var task in taskCroudHasBilet)
            {
                task.Dispose();
            }
            foreach (var task in taskCroudToStadion)
            {
                task.Dispose();
            }
        }
        public void UpdateAllGrids()
        {
            UpdateGrid(Sectors.A);
            UpdateGrid(Sectors.B);
            UpdateGrid(Sectors.C);
            UpdateGrid(Sectors.D);
            UpdateGrid(Sectors.E);
            UpdateGrid(Sectors.F);
        }
        public void UpdateGrid(Sectors sect)
        {
            if (stadion.SecA.funs.Length != 0)
            {
                switch (sect)
                {
                    case Sectors.A:
                        SetColorPlaces(Sectors.A, stadion.GetNumbersFunsBySector(Sectors.A));
                        break;
                    case Sectors.B:
                        SetColorPlaces(Sectors.B, stadion.GetNumbersFunsBySector(Sectors.B));
                        break;
                    case Sectors.C:
                        SetColorPlaces(Sectors.C, stadion.GetNumbersFunsBySector(Sectors.C));
                        break;
                    case Sectors.D:
                        SetColorPlaces(Sectors.D, stadion.GetNumbersFunsBySector(Sectors.D));
                        break;
                    case Sectors.E:
                        SetColorPlaces(Sectors.E, stadion.GetNumbersFunsBySector(Sectors.E));
                        break;
                    case Sectors.F:
                        SetColorPlaces(Sectors.F, stadion.GetNumbersFunsBySector(Sectors.F));
                        break;
                    default:
                        break;
                }
            }
        }

        public void CroudFilterHasBilet()
        {
            if (funsIndors.Count != 0 && funsIndorsBack.Count != funsIndors.Count)
            {
                funsIndorsBack.Clear();
                foreach (var item in funsIndors)
                {
                    funsIndorsBack.Add(item);
                }
            }
            try
            {
                foreach (var fun in CroudOutside.funs)
                {
                    if (fun != null)
                    {
                        if (fun.HasBilet && !CroudOutside.cansel.IsCancellationRequested && CroudOutside.funs.Count != 0)
                        {
                            funsIndors.Add(fun);
                            CroudOutside.funs.Remove(fun);
                        }
                        else
                        {
                            CroudOutside.funs.Remove(fun);
                        }
                    }
                    else { }
                }
            }
            catch (System.InvalidOperationException)
            {
            }
        }
        public void CroudToStadion()
        {
            var brush = new SolidColorBrush();
            brush.Color = Colors.Green;
            brush.Opacity = 0.8;
            try
            {
                foreach (var fun in funsIndors)
                {
                    if (!CroudOutside.cansel.IsCancellationRequested && funsIndors.Count != 0)
                    {

                        stadion.AddFunToSec(fun);
                        funsIndors.Remove(fun);
                        //UpdateGrid(fun.Sector);
                        SetColorPlace(fun.Sector, fun.Place, brush);
                    }
                }
            }
            catch (System.InvalidOperationException)
            {

            }
        }

        public void SetColorPlace(Sectors sec, int place, Brush br)
        {
            switch (sec)
            {
                case Sectors.A:
                    this.Dispatcher.BeginInvoke(
            DispatcherPriority.Normal,
            new Action(() => (SecA.Children[place] as Rectangle).Fill = br)
            );
                    break;
                case Sectors.B:
                    this.Dispatcher.BeginInvoke(
            DispatcherPriority.Normal,
            new Action(() => (SecB.Children[place] as Rectangle).Fill = br)
            );
                    break;
                case Sectors.C:
                    this.Dispatcher.BeginInvoke(
            DispatcherPriority.Normal,
            new Action(() => (SecC.Children[place] as Rectangle).Fill = br)
            );
                    break;
                case Sectors.D:
                    this.Dispatcher.BeginInvoke(
            DispatcherPriority.Normal,
            new Action(() => (SecD.Children[place] as Rectangle).Fill = br)
            );
                    break;
                case Sectors.E:
                    this.Dispatcher.BeginInvoke(
            DispatcherPriority.Normal,
            new Action(() => (SecE.Children[place] as Rectangle).Fill = br)
            );
                    break;
                case Sectors.F:
                    this.Dispatcher.BeginInvoke(
            DispatcherPriority.Normal,
            new Action(() => (SecF.Children[place] as Rectangle).Fill = br)
            );
                    break;
                default:
                    break;
            }
        }
        public void TestAdd()
        {
            for (int i = 0; i < 500; i++)
            {
                peopleGenerator.AddNewFun();
            }
        }

        public void tbOutUpdate()
        {
            tbOutside.Text = null;
            foreach (var funout in CroudOutside.funs)
            {
                tbOutside.Text += $"{funout.Name}, Has bilet: {funout.HasBilet}\n";
            }
        }

        public void tbInUpdate()
        {
            tbInside.Text = null;
            foreach (var funIn in funsIndorsBack)
            {
                tbInside.Text += $"{funIn.Name}, Sector: {funIn.Sector}, place: {funIn.Place}\n";

            }
        }
        public void SetColorPlaces(Sectors sec, int[] place)
        {
            var brush = new SolidColorBrush();
            brush.Color = Colors.Gray;
            brush.Opacity = 0.5;

            var brushmain = new SolidColorBrush();
            brush.Color = Colors.Green;
            brush.Opacity = 0.5;
            for (int i = 0; i < 500; i++)
            {
                if (place[i] != -1)
                {
                    switch (sec)
                    {
                        case Sectors.A:
                            this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => (SecA.Children[i] as Rectangle).Fill = brushmain)
                    );
                            break;
                        case Sectors.B:
                            this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => (SecB.Children[i] as Rectangle).Fill = brushmain)
                    );
                            break;
                        case Sectors.C:
                            this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => (SecC.Children[i] as Rectangle).Fill = brushmain)
                    );
                            break;
                        case Sectors.D:
                            this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => (SecD.Children[i] as Rectangle).Fill = brushmain)
                    );
                            break;
                        case Sectors.E:
                            this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => (SecE.Children[i] as Rectangle).Fill = brushmain)
                    );
                            break;
                        case Sectors.F:
                            this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => (SecF.Children[i] as Rectangle).Fill = brushmain)
                    );
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (sec)
                    {
                        case Sectors.A:
                            this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => (SecA.Children[i] as Rectangle).Fill = brush)
                    );
                            break;
                        case Sectors.B:
                            this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => (SecB.Children[i] as Rectangle).Fill = brush)
                    );
                            break;
                        case Sectors.C:
                            this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => (SecC.Children[i] as Rectangle).Fill = brush)
                    );
                            break;
                        case Sectors.D:
                            this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => (SecD.Children[i] as Rectangle).Fill = brush)
                    );
                            break;
                        case Sectors.E:
                            this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => (SecE.Children[i] as Rectangle).Fill = brush)
                    );
                            break;
                        case Sectors.F:
                            this.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() => (SecF.Children[i] as Rectangle).Fill = brush)
                    );
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cancel.Cancel = false;
            for (int i = 0; i < 3000 - pause; i++)
            {
                if (!cancel.Cancel)
                {
                    pause = i;
                    DelayAction(9000, btnStart);
                }
                else break;
            }
            
        }

        public static void DelayAction(int millisecond, Action action)
        {
            var timer = new DispatcherTimer();
            timer.Tick += delegate

            {
                action.Invoke();
                timer.Stop();
            };

            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Start();
        }
        private void btnStart()
        {
            if (!cancel.Cancel)
            {
                StartCroud();
                tbInUpdate();
                tbOutUpdate();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            cancel.Cancel = true;
        }
    }
}
