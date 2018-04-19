using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ExtendedClipboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        List<Label> labelList;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            labelList = new List<Label>();
            labelList.Add(Label1);
            labelList.Add(Label2);
            labelList.Add(Label3);
            labelList.Add(Label4);
            labelList.Add(Label5);

            Clipboard.Clear();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if(Clipboard.ContainsData(DataFormats.Text))
            {
                for(int i = labelList.Count-1; i > 0; i--)
                {
                    labelList[i].Content = labelList[i - 1].Content;
                }
                labelList[0].Content = Clipboard.GetText();
            }
            else if(Clipboard.ContainsImage())
            {
                for (int i = labelList.Count - 1; i > 0; i--)
                {
                    labelList[i].Content = labelList[i - 1].Content;
                }
                Image image = new Image();
                image.Source = Clipboard.GetImage();
                labelList[0].Content = image;
            }
            Clipboard.Clear();

        }
    }
}
