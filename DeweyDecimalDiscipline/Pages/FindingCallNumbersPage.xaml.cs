using DeweyDecimalDiscipline.Content;
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

        public FindingCallNumbersPage()
        {
            InitializeComponent();
            
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
                    CallNumberOptions.ItemsSource = setB.OrderBy(node => node.Number);
                    level = 2;
                    return;
                }
                Debug.WriteLine("Defeat!");
            }

            if (level == 2)
            {
                if (selectedNode == goal.Parent!)
                {
                    CallNumberOptions.ItemsSource = setA.OrderBy(node => node.Number); 
                    level = 3;
                    return;
                }
                Debug.WriteLine("Defeat!");
            }

            if (level == 3)
            {
                if (selectedNode == goal)
                {
                    Debug.WriteLine("Victory!");
                    return;
                }
                Debug.WriteLine("Defeat!");
            }
        }
    }
}
