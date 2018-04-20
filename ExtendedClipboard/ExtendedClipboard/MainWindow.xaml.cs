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
        List<Button> buttonList;
        int ButtonsVisible = 0;

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


            //Adds buttons to list and hides them
            buttonList = new List<Button>();
            button1.Visibility = Visibility.Hidden;
            buttonList.Add(button1);
            button2.Visibility = Visibility.Hidden;
            buttonList.Add(button2);
            button3.Visibility = Visibility.Hidden;
            buttonList.Add(button3);
            button4.Visibility = Visibility.Hidden;
            buttonList.Add(button4);
            button5.Visibility = Visibility.Hidden;
            buttonList.Add(button5);

            //TODO: Add functionality to buttons

            Clipboard.Clear();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            if(Clipboard.ContainsData(DataFormats.Text))
            {
                for(int i = labelList.Count-1; i > 0; i--)
                {
                    labelList[i].Content = labelList[i - 1].Content;
                    buttonList[i].Content = buttonList[i - 1].Content;
                }
                labelList[0].Content = Clipboard.GetText();
                if (labelList[0].Content.GetType() == typeof(string)) {
                    buttonList[0].Content = "Copy";
                }

                //Sets Buttons to Visible after copying next text
                //TODO: Separate function for setting visibility of button if not all buttons are visible
                if (ButtonsVisible < 5)
                {
                    buttonList[ButtonsVisible].Visibility = Visibility.Visible;
                    ButtonsVisible++;
                }

            }
            else if(Clipboard.ContainsImage())
            {
                for (int i = labelList.Count - 1; i > 0; i--)
                {
                    labelList[i].Content = labelList[i - 1].Content;
                    buttonList[i].Content = buttonList[i - 1].Content;
                }
                Image image = new Image();
                image.Source = Clipboard.GetImage();
                labelList[0].Content = image;
                if (labelList[0].Content.GetType() == typeof(Image))
                {
                    buttonList[0].Content = "Zoom";
                }

                //Sets Buttons to Visible after copying next image
                //TODO: Separate function for setting visibility of button if not all buttons are visible
                if (ButtonsVisible < 5)
                {
                    buttonList[ButtonsVisible].Visibility = Visibility.Visible;
                    ButtonsVisible++;
                }

            }
            //TODO: Find a way not to clear clipboard everytime (buffor maybe?)
            Clipboard.Clear();




        }
    }
}
