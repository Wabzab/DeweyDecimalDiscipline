using DeweyDecimalDiscipline.Content;
using DeweyDecimalDiscipline.Data;
using DeweyDecimalDiscipline.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace DeweyDecimalDiscipline.Pages
{
    /// <summary>
    /// Interaction logic for FindingCallNumbersPage.xaml
    /// </summary>
    public partial class FindingCallNumbersPage : Page
    {
        public int level = 1;
        public Tree tree { get; set; }
        public Node goal { get; set; }
        public ObservableCollection<Node> setA { get; set; }
        public ObservableCollection<Node> setB { get; set; }
        public ObservableCollection<Node> setC { get; set; }

        // Time Objects used to keep track of time
        DispatcherTimer timer { get; set; }
        DateTime startTime { get; set; }
        TimeSpan time { get; set; }

        public FindingCallNumbersPage()
        {
            InitializeComponent();

            string tutorial = ("You are presented with the call number description to find at the top of the page (e.g: Wind instruments).\n\n" +
                "There are four options available to pick from on the rest of the page. \n\n" +
                "Only one of these is correct, pick the wrong option and it is game over.\n\n" +
                "The options start at the top level of call number (e.g: 200, 400, 700) and get more precise with each succesfull answer (e.g: 740, 780, 790)\n\n" +
                "Try to find the correct call number as fast as possible! Good luck!");
            var result = MessageBox.Show(tutorial, "Finding Call Numbers", MessageBoxButton.OK);

            tree = CallNumberClassesHandler.GetCallNumberTree();
            tree.ParentChildNodes(tree.Root);
            goal = GetRandomBottomNode(tree.Root);

            setA = new ObservableCollection<Node> { goal };
            foreach (var node in GetRandomSiblings(goal, 3))
            {
                setA.Add(node);
            }

            setB = new ObservableCollection<Node> { goal.Parent! };
            foreach (var node in GetRandomSiblings(goal.Parent!, 3))
            {
                setB.Add(node);
            }

            setC = new ObservableCollection<Node> { goal.Parent!.Parent! };
            foreach (var node in GetRandomSiblings(goal.Parent!.Parent!, 3))
            {
                Debug.WriteLine(node.Number);
                setC.Add(node);
            }

            TargetCallNumber.ItemsSource = new ObservableCollection<Node> { goal };
            CallNumberOptions.ItemsSource = setC.OrderBy(node => node.Number);

            // Start timer
            startTime = DateTime.Now;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += timer_Tick;
            timer.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            time = DateTime.Now.Subtract(startTime);
            lblTime.Content = string.Format("{0}:{1}", (time.Minutes).ToString("D2"), (time.Seconds).ToString("D2"));
        }

        private void GameOver()
        {
            timer.Stop();
            // Record score sheet data
            Finding scoresheet = new Finding();
            scoresheet.Time = time;
            scoresheet.Date = startTime;
            scoresheet.Score = level-1;
            // Correct Call Number Order
            string correctAnswer = string.Format("{0} - {1}\n{2} - {3}\n{4} - {5}", goal.Number, goal.Description, goal.Parent!.Number, goal.Parent!.Description, goal.Parent!.Parent!.Number, goal.Parent!.Parent!.Description);
            // Inform user of result
            string endMessage = string.Format("Game over: {0:0}% in {1}s\n\n", (scoresheet.Score / 3.0 * 100), scoresheet.Time.TotalSeconds);
            MessageBox.Show(endMessage + correctAnswer);
            // Store score sheet
            FindingDAO.Add(scoresheet);
            // Navigate to home page
            LandingPage landingPage = new LandingPage();
            this.NavigationService.Navigate(landingPage);
        }

        private Node GetRandomBottomNode(Node node)
        {
            Random random = new Random();
            Node child = node.Children[random.Next(0, node.Children.Length)];
            if (child.Children.Length > 0)
            {
                return GetRandomBottomNode(child);
            }
            return child;
        }

        private List<Node> GetRandomSiblings(Node node, int count)
        {
            Random random = new Random();
            Node parent = node.Parent!;
            List<Node> siblings = new List<Node>();
            int total = 0;
            if (parent.Children.Length <= count)
            {
                return new List<Node>();
            }
            while (total < count)
            {
                Node sibling = parent.Children[random.Next(0, parent.Children.Length)];
                if (sibling == node || siblings.Contains(sibling))
                {
                    continue;
                }
                siblings.Add(sibling);
                total++;
            }
            return siblings;
        }

        private void onOptionsDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject obj = (DependencyObject)e.OriginalSource;

            while (obj != null && obj != CallNumberOptions)
            {
                if (obj.GetType() == typeof(ListBoxItem))
                {

                    if (CallNumberOptions.SelectedItem != null)
                    {
                        Node? node = CallNumberOptions.SelectedItem as Node;
                        CheckCorrectSelection(node!);
                    }
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
        }

        private void CheckCorrectSelection(Node selectedNode)
        {
            if (level == 1)
            {
                if (selectedNode == goal.Parent!.Parent!)
                {
                    level++;
                    CallNumberOptions.ItemsSource = setB.OrderBy(node => node.Number);
                    return;
                }
                GameOver();
            }

            if (level == 2)
            {
                if (selectedNode == goal.Parent!)
                {
                    level++;
                    CallNumberOptions.ItemsSource = setA.OrderBy(node => node.Number); 
                    return;
                }
                GameOver();
            }

            if (level == 3)
            {
                if (selectedNode == goal)
                {
                    level++;
                    GameOver();
                    return;
                }
                GameOver();
            }
        }
    }
}
