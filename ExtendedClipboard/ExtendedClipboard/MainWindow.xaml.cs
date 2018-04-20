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
        Image[] imageArray;

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

            imageArray = new Image[5];


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


            Clipboard.Clear();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            if(Clipboard.ContainsData(DataFormats.Text))
            {
                copyText();
            }
            else if(Clipboard.ContainsImage())
            {
                copyImage();
            }
            //TODO: Find a way not to clear clipboard everytime (buffor maybe?)
            Clipboard.Clear();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            buttonClicked(0);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            buttonClicked(1);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            buttonClicked(2);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            buttonClicked(3);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            buttonClicked(4);
        }

        private void copyText()
        {
            rewrite();
            labelList[0].Content = Clipboard.GetText();

            buttonConfig();
        }

        private void copyImage()
        {
            rewrite();
            Image image = new Image();
            image.Source = Clipboard.GetImage();
            labelList[0].Content = image;
            imageArray[0] = image;
            buttonConfig();
        }

        //Makes first Label free when there is a need to add a new content
        private void rewrite()
        {
            for (int i = labelList.Count - 1; i > 0; i--)
            {
                labelList[i].Content = labelList[i - 1].Content;
                buttonList[i].Content = buttonList[i - 1].Content;
                imageArray[i] = imageArray[i - 1];
            }
        }

        //Sets Buttons to Visible after copying next image
        //TODO: Separate function for setting visibility of button if not all buttons are visible
        //EDIT: Tried to do it
        private void buttonConfig()
        {
            if (labelList[0].Content.GetType() == typeof(string))
            {
                buttonList[0].Content = "Copy";
            } else if (labelList[0].Content.GetType() == typeof(Image))
            {
                buttonList[0].Content = "Zoom";
            }

            if (ButtonsVisible < 5)
            {
                buttonList[ButtonsVisible].Visibility = Visibility.Visible;
                ButtonsVisible++;
            }
        }

        private void buttonClicked(int numberOfButton)
        {
            if(((string)buttonList[numberOfButton].Content == "Copy") && (numberOfButton != 0) && (labelList[0].Content != labelList[numberOfButton].Content))
            {
                string temp = (string)labelList[numberOfButton].Content;
                rewrite();
                labelList[0].Content = temp;
                buttonConfig();
            }
            //User want copy content which already is on the top
            else if(((string)buttonList[numberOfButton].Content == "Copy") && ((numberOfButton == 0) || (labelList[0].Content == labelList[numberOfButton].Content)))    
            {
                //TODO: Inform that there is no need to copy
            }
            else if((string)buttonList[numberOfButton].Content == "Zoom")  //zoom
            {
                var window = new ZoomWindow();      //new Window with zoomed image
                Image image = imageArray[numberOfButton];
                window.Image1.Height = image.Height;
                window.Image1.Width = image.Width;
                window.Image1.Source = image.Source;
                window.SizeToContent = SizeToContent.Manual;
                window.Show();
            }


        }

    }
}
