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

namespace PerProg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ViewModel VM { get; set; }
        bool aktualisJatekos = true;
        Point from;
        Point to;
        int clickCount = 0;
        public MainWindow()
        {
            InitializeComponent();
            VM = new ViewModel();
            this.DataContext = VM;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
                if (clickCount < 1)
                {
                    Point p = e.GetPosition((Image)this.Content);
                    int width = (int)(((Image)this.Content).ActualWidth / 8);
                    int height = (int)(((Image)this.Content).ActualHeight / 8);
                    int y = (int)(p.X / width);
                    int x = (int)(p.Y / height);
                    from = new Point(x, y);
                    clickCount++;
                }
                else
                {
                    Point p = e.GetPosition((Image)this.Content);
                    int width = (int)(((Image)this.Content).ActualWidth / 8);
                    int height = (int)(((Image)this.Content).ActualHeight / 8);
                    int y = (int)(p.X / width);
                    int x = (int)(p.Y / height);
                    to = new Point(x, y);
                    bool lepett = VM.Lepes(from, to, aktualisJatekos);
                    if (lepett)
                    {
                        aktualisJatekos = !aktualisJatekos;
                    }
                    clickCount--;
                }
            
            

        }
    }
}
